using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Poc.BoletoSre2.Modelos;
using System.Net;
using System.Net.Http.Headers;

namespace Poc.BoletoSre2.Gateways {

    /// <inheritdoc cref="ISegurancaGateway"/>
    public class SegurancaRestGateway : IDisposable, ISegurancaGateway {

        #region Campos...

        private bool disposed = false;
        private readonly ILogger<SegurancaRestGateway> _log;
        protected string _urlBase = null;

        #endregion

        #region Construtor...

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        public SegurancaRestGateway(ILogger<SegurancaRestGateway> logService) {

            _log = logService;
            _log.LogTrace("SegurancaRestGateway.Construtor chamado...");
            _urlBase = "https://testeappsre/ServicoSeguranca/";
        }

        #endregion

        #region Métodos principais...

        /// <inheritdoc/>
        public virtual async Task<TicketAutenticacao> RealizarLoginAsync(string login, string senha, CancellationToken cancellationToken = default) {

            string metodo = $"api/Seguranca/Login";

            try {
                using (var client = CriarHttpClient()) {

                    var SolicitacaoLogin = new { login = login, senha = senha };

                    var retorno = await RealizarPostAsync<TicketAutenticacao, SegurancaSessaoFaultContract>(SolicitacaoLogin, metodo, cancellationToken, client).ConfigureAwait(false);
                    if (retorno.Status.Codigo == "OK") {
                        return retorno.Data;
                    }
                    _log.LogError("Erro de segurança. Erro: {@retorno}", retorno);
                }
            }
            catch(Exception xabu) {
                _log.LogError(xabu, "Erro ao realizar login.");
            }

            return null;
        }

        #endregion

        #region Métodos de apoio...

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlBase"></param>
        /// <returns></returns>
        private HttpClient CriarHttpClient() {

            var httpClient = new HttpClient();
            if (!string.IsNullOrEmpty(_urlBase)) {
                httpClient.BaseAddress = new Uri(_urlBase);
            }
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conteudo"></param>
        /// <param name="url"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async Task<RetornoOperacao<T, E>> RealizarPostAsync<T, E>(object conteudo, string url = null, CancellationToken cancellationToken = default, HttpClient client = null) {

            RetornoOperacao<T, E> retorno = new RetornoOperacao<T, E>();

            JsonSerializerSettings settings = new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore,
            };

            string json = JsonConvert.SerializeObject(conteudo, settings);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType.MediaType = "application/json";

            if (client == null) {
                using (HttpClient httpClient = CriarHttpClient()) {
                    var responseMessage = await httpClient.PostAsync(url, httpContent, cancellationToken);
                    await ProcessarResponseMessageAsync(retorno, responseMessage, cancellationToken).ConfigureAwait(false);
                }
            }
            else {
                var responseMessage = await client.PostAsync(url, httpContent, cancellationToken);
                await ProcessarResponseMessageAsync(retorno, responseMessage, cancellationToken).ConfigureAwait(false);
            }

            return retorno;
        }

        protected async Task ProcessarResponseMessageAsync<T, E>(RetornoOperacao<T, E> retorno, HttpResponseMessage responseMessage, CancellationToken cancellationToken) {

            string mensagemErro = responseMessage.ReasonPhrase;

            string jsonResult = await responseMessage.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            if (responseMessage.StatusCode == HttpStatusCode.OK || responseMessage.StatusCode == HttpStatusCode.Created) {
                retorno.Status.Codigo = "OK";
                if (typeof(T) == typeof(string)) {
                    // Remover aspas no começo e fim do texto...
                    string texto = (jsonResult?.Length > 2) ? jsonResult.Substring(1, jsonResult.Length - 2) : "";
                    retorno.Data = (T)(object)texto;
                }
                else {
                    retorno.Data = JsonConvert.DeserializeObject<T>(jsonResult);
                }
            }
            else {
                var resumo = new {
                    StatusCode = Convert.ToInt32(responseMessage.StatusCode),
                    ReasonPhrase = responseMessage.ReasonPhrase,
                    Metodo = responseMessage.RequestMessage.Method.ToString(),
                    Url = responseMessage.RequestMessage.RequestUri.ToString(),
                    RetornoJson = jsonResult
                };
                _log.LogError("SegurancaRestGateway: Erro na requisição: {@resumo}", resumo);

                // Tentar deserializar o erro para o tipo de erro/generics informado...
                try {
                    retorno.Erro = JsonConvert.DeserializeObject<E>(jsonResult);
                }
                catch (Exception xabu) {
                    _log.LogError(xabu, "SegurancaRestGateway: Falha ao recuperar erro retornado. Retorno json não serializavel para tipo informado.");
                }

                // Genericamente faz um cast de retorno.Erro para problemDetails, que deverá ser um cenário geral.
                // Se o cast falhar, problemDetails estará sempre como um null e não será utilizada a seguir...
                ProblemDetails problemDetails = retorno.Erro as ProblemDetails;

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden || responseMessage.StatusCode == HttpStatusCode.Unauthorized) {
                    // Erro de segurança...
                    retorno.Status = new StatusRetorno() { Codigo = "ERRO_SEGURANCA", Mensagem = "Erro de segurança durante comunicação com integração." };
                }
                else if (responseMessage.StatusCode == HttpStatusCode.BadRequest) {
                    // Erro de validação...
                    retorno.Status = new StatusRetorno() { Codigo = "ERRO_VALIDACAO", Mensagem = "Erro de validação durante comunicação com integração." };
                    retorno.Mensagens.Add(new OperacaoInfo() { Codigo = "ERRO_VALIDACAO", Mensagem = problemDetails?.Detail ?? mensagemErro });
                }
                else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError) {
                    // Erro interno na integração (do lado do servidor)...
                    retorno.Status = new StatusRetorno() { Codigo = "ERRO_INTEGRACAO", Mensagem = "Erro interno na integração." };
                }
                else if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable) {
                    // Serviço desligado e/ou indisponível...
                    retorno.Status = new StatusRetorno() { Codigo = "ERRO_INTEGRACAO", Mensagem = "Ocorreu um erro na comunicação com o serviço." };
                    retorno.Mensagens.Add(new OperacaoInfo() { Codigo = "ERRO_INTEGRACAO", Mensagem = "O serviço se encontra indisponível no momento. Favor tentar dentro de alguns minutos." });
                }
                else {
                    // Demais erros...
                    retorno.Status = new StatusRetorno() { Codigo = "ERRO_INTERNO", Mensagem = "Ocorreu um erro na comunicação com o serviço." };
                }
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected void Dispose(bool disposing) {

            if (disposed) { return; }
            _log.LogTrace("SegurancaRestGateway.Dispose chamado...");
            disposed = true;
        }

        #endregion
    }
}
