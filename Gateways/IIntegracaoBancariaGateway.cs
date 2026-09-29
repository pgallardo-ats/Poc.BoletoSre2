using Poc.BoletoSre2.Modelos;
using SreIntBancClient;

namespace Poc.BoletoSre2.Gateways {
    
    /// <summary>
    /// Integração para a integração bancária do JUCERJA-SRE...
    /// </summary>
    public interface IIntegracaoBancariaGateway {

        /// <summary>
        /// Cria um novo boleto avulso...
        /// </summary>
        /// <param name="dadosBoleto">Dados básicos do boleto.</param>
        /// <param name="ticket">Instância de <see cref="TicketAutenticacao"/> obtido de um login do sistema de segurança (JUCERJA-SSEG).</param>
        /// <param name="ipChamador">Texto com o IPv4 do chamador da operação.</param>
        /// <param name="cancellationToken">Token de cancelamento da operação.</param>
        /// <returns>Instância de <see cref="BoletoBancarioAvulso"/> com os dados do boleto criado.</returns>
        Task<BoletoBancarioAvulso> CriarBoletoAvulsoAsync(DadosBoletoBancarioCriacao dadosBoleto, TicketAutenticacao ticket, string ipChamador, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtem um boleto avulso com base no BoletoId...
        /// </summary>
        /// <param name="boletoId">Identificador de instância de um boleto bancário (boleto avulso).</param>
        /// <param name="ticket">Instância de <see cref="TicketAutenticacao"/> obtido de um login do sistema de segurança (JUCERJA-SSEG).</param>
        /// <param name="ipChamador">Texto com o IPv4 do chamador da operação.</param>
        /// <param name="cancellationToken">Token de cancelamento da operação.</param>
        /// <returns>Instância de <see cref="BoletoBancarioAvulso"/> com os dados do boleto encontrado.</returns>
        Task<BoletoBancarioAvulso> ObterBoletoBancarioAvulsoPorIdAsync(int boletoId, TicketAutenticacao ticket, string ipChamador, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtem um boleto avulso com base no número do boleto...
        /// </summary>
        /// <param name="numeroBoleto">Numero do boleto a ser obtido.</param>
        /// <param name="ticket">Instância de <see cref="TicketAutenticacao"/> obtido de um login do sistema de segurança (JUCERJA-SSEG).</param>
        /// <param name="ipChamador">Texto com o IPv4 do chamador da operação.</param>
        /// <param name="cancellationToken">Token de cancelamento da operação.</param>
        /// <returns>Instância de <see cref="BoletoBancarioAvulso"/> com os dados do boleto encontrado.</returns>
        Task<BoletoBancarioAvulso> ObterBoletoBancarioPorNumeroAsync(string numeroBoleto, TicketAutenticacao ticket, string ipChamador, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obter o arquivo (PDF) do boleto bancário solicitado...
        /// </summary>
        /// <param name="boletoId">Identificador de instância de um boleto bancário .</param>
        /// <param name="caminhoArquivo">Caminho onde o arquivo PDF será salvo.</param>
        /// <param name="nomeArquivo">Nome do arquivo PDF. (nome arquivo + extensão)</param>
        /// <param name="ticket">Instância de <see cref="TicketAutenticacao"/> obtido de um login do sistema de segurança (JUCERJA-SSEG).</param>
        /// <param name="ipChamador">Texto com o IPv4 do chamador da operação.</param>
        /// <param name="cancellationToken">Token de cancelamento da operação.</param>
        /// <returns>Instância de <see cref="ArquivoInterno"/> com os dados do arquivo PDF do boleto.</returns>
        Task<ArquivoInterno> ObterBoletoBancarioArquivoAsync(int boletoId, string caminhoArquivo, string nomeArquivo, TicketAutenticacao ticket, string ipChamador, CancellationToken cancellationToken = default);
    }
}