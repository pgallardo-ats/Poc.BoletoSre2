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

                    var retorno = await RealizarPostAsync(SolicitacaoLogin, metodo, client, cancellationToken).ConfigureAwait(false);
                    if (retorno.sucesso) {
                        return retorno.resp;
                    }
                    _log.LogError("Erro de segurança. Erro: {@retorno}", retorno.erro);
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
        protected async Task<(bool sucesso, TicketAutenticacao resp, SegurancaSessaoFaultContract erro)> RealizarPostAsync(object conteudo, string url = null, HttpClient client = null, CancellationToken cancellationToken = default) {

            (bool sucesso, TicketAutenticacao resp, SegurancaSessaoFaultContract erro) retorno = new(false, null, null);

            JsonSerializerSettings settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(conteudo, settings));
            httpContent.Headers.ContentType.MediaType = "application/json";

            var responseMessage = await client.PostAsync(url, httpContent, cancellationToken);
            retorno = await ProcessarResponseMessageAsync<TicketAutenticacao, SegurancaSessaoFaultContract>(responseMessage, cancellationToken).ConfigureAwait(false);

            return retorno;
        }

        protected async Task<(bool sucesso, R, E)> ProcessarResponseMessageAsync<R,E>(HttpResponseMessage responseMessage, CancellationToken cancellationToken) {

            (bool sucesso,R resp, E erro) retorno = new(false, default, default);

            string jsonResult = await responseMessage.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            if (responseMessage.StatusCode == HttpStatusCode.OK || responseMessage.StatusCode == HttpStatusCode.Created) {
                retorno.sucesso = true;
                retorno.resp = JsonConvert.DeserializeObject<R>(jsonResult);
            }
            else {
                retorno.sucesso = false;
                var resumo = new {
                    StatusCode = Convert.ToInt32(responseMessage.StatusCode),
                    ReasonPhrase = responseMessage.ReasonPhrase,
                    Metodo = responseMessage.RequestMessage.Method.ToString(),
                    Url = responseMessage.RequestMessage.RequestUri.ToString(),
                    RetornoJson = jsonResult
                };
                _log.LogError("SegurancaRestGateway: Erro na requisição: {@resumo}", resumo);

                try {
                    retorno.erro = JsonConvert.DeserializeObject<E>(jsonResult);
                }
                catch (Exception xabu) {
                    _log.LogError(xabu, "SegurancaRestGateway: Falha ao recuperar erro retornado. Retorno json não serializavel para tipo experdo.");
                }
            }

            return retorno;
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
