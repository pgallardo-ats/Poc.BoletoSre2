using Microsoft.Extensions.Logging;
using Poc.BoletoSre2.Gateways;
using Poc.BoletoSre2.Helpers;
using Poc.BoletoSre2.Modelos;
using System.Globalization;

namespace Poc.BoletoSre2 {

    public partial class Form1 : Form {

        #region Campos...

        private readonly ILogger<Form1> _log;
        private readonly ISegurancaGateway _segurancaGateway;
        private readonly IIntegracaoBancariaGateway _integracaoBancariaGateway;
        private bool _ocupado = false;
        private CancellationTokenSource _cts = null;

        #endregion

        #region Construtor...

        public Form1(ILogger<Form1> logger, ISegurancaGateway segurancaGateway, IIntegracaoBancariaGateway integracaoBancariaGateway) {

            _log = logger;
            _segurancaGateway = segurancaGateway;
            _integracaoBancariaGateway = integracaoBancariaGateway;
            InitializeComponent();
        }

        #endregion

        #region Handlers do formul�rio...

        private void Form1_Load(object sender, EventArgs e) {

            string msg = "Formul�rio carregado...";
            _log.LogInformation(msg);
            Logar(msg);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {

            if (_cts != null) {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }

        private async void btnGerarBoleto_Click(object sender, EventArgs e) {

            SetOcupado(true);
            await GerarBoletoAsync();
            SetOcupado(false);
        }

        #endregion

        #region M�todos principais...

        public async Task GerarBoletoAsync() {

            string login = txtBoxLogin.Text;
            string senha = txtBoxSenha.Text;

            _cts = new CancellationTokenSource();

            var ticket = await _segurancaGateway.RealizarLoginAsync(login, senha, _cts.Token).ConfigureAwait(false);
            if (ticket == null) {
                Logar("ERRO: Login n�o teve sucesso, confira o log...");
                return;
            }
            else {
                Logar($"LOGIN: Login com sucesso para login: \"{login}\", UsuarioID: {ticket.UsuarioId}");
            }

            Logar("Gerando boleto banc�rio...");
            DadosBoletoBancarioCriacao dadosBoleto = new DadosBoletoBancarioCriacao() {
                AtoId = 169, // 860	- Emiss�o de certificado digital
                EventoId = 311, // 861 - Emiss�o de certificado digital e-CPF A1
                AtoEventoId = 529, // AtoID: 169 X EventoID: 311
                CpfCnpjSolicitante = UtilHelper.GerarCpf(),
                NomeSolicitante = $"Fulano {UtilHelper.ObterStringAleatoria(10)} da Silva Testes",
                ValorAtoEvento = 1234,
                ValorBoleto = 1234,
            };
            var boletoBancarioAvulso = await _integracaoBancariaGateway.CriarBoletoAvulsoAsync(dadosBoleto, ticket, "127.0.0.1", _cts.Token).ConfigureAwait(false);
            if (boletoBancarioAvulso == null) {
                Logar("ERRO: Erro ao gerar boleto banc�rio, confira o log..");
                return;
            }
            else {
                Logar($"Gerado boleto banc�rio...\r\nBoletoID : {boletoBancarioAvulso.BoletoId}\r\nNumeroBoleto : {boletoBancarioAvulso.Boleto.NumeroBoleto}");
            }

            // Buscar informa��es do boleto com base no BoletoID...
            if (boletoBancarioAvulso != null) {
                await BuscarBoletoPorBoletoIdAsync(boletoBancarioAvulso.BoletoId, ticket, _cts.Token).ConfigureAwait(false);
                await BuscarBoletoPorNumeroAsync(boletoBancarioAvulso.Boleto.NumeroBoleto, ticket, _cts.Token).ConfigureAwait(false);
            }
        }

        public async Task BuscarBoletoPorBoletoIdAsync(int boletoId, TicketAutenticacao ticket, CancellationToken cancellationToken = default) {

            Logar($"\r\nBuscar boleto por id, BoletoID: {boletoId}");
            var boletoBancarioAvulso = await _integracaoBancariaGateway.ObterBoletoBancarioAvulsoPorIdAsync(boletoId, ticket, "127.0.0.1", cancellationToken).ConfigureAwait(false);
            if (boletoBancarioAvulso == null) {
                Logar($"ERRO: Ocorreu erro ao tentar buscar BoletoID: {boletoId}, confira o log...");
            }
            else {
                CultureInfo culture = new CultureInfo("pt-BR");
                Logar("Boleto encontrado...");
                Logar($"BoletoID: {boletoId}");
                Logar($"NumeroBoleto : {boletoBancarioAvulso.Boleto.NumeroBoleto}");
                Logar($"NumeroDocumento : {boletoBancarioAvulso.Boleto.NumeroDocumento}");
                Logar($"DataEmissao : {boletoBancarioAvulso.Boleto.DataEmissao.ToString("dd/MM/yyyy HH:mm")}");
                Logar($"DataVencimento : {boletoBancarioAvulso.Boleto.DataVencimento.ToString("dd/MM/yyyy HH:mm")}");
                Logar($"ValorBoleto : {boletoBancarioAvulso.Boleto.ValorBoleto.ToString("C", culture)}");
                Logar($"CpfCnpjSolicitante : {boletoBancarioAvulso.CpfCnpjSolicitante}");
                Logar($"NomeSolicitante : {boletoBancarioAvulso.NomeSolicitante}");
            }
        }

        public async Task BuscarBoletoPorNumeroAsync(string numeroBoleto, TicketAutenticacao ticket, CancellationToken cancellationToken = default) {

            Logar($"\r\nBuscar boleto por NumeroBoleto, Numero: {numeroBoleto}");
            var boletoBancarioAvulso = await _integracaoBancariaGateway.ObterBoletoBancarioPorNumeroAsync(numeroBoleto, ticket, "127.0.0.1", cancellationToken).ConfigureAwait(false);
            if (boletoBancarioAvulso == null) {
                Logar($"ERRO: Ocorreu erro ao tentar buscar boleto n�mero: {numeroBoleto}, confira o log...");
            }
            else {
                CultureInfo culture = new CultureInfo("pt-BR");
                Logar("Boleto encontrado...");
                Logar($"NumeroBoleto : {boletoBancarioAvulso.Boleto.NumeroBoleto}");
                Logar($"BoletoID: {boletoBancarioAvulso.BoletoId}");
                Logar($"NumeroDocumento : {boletoBancarioAvulso.Boleto.NumeroDocumento}");
                Logar($"DataEmissao : {boletoBancarioAvulso.Boleto.DataEmissao.ToString("dd/MM/yyyy HH:mm")}");
                Logar($"DataVencimento : {boletoBancarioAvulso.Boleto.DataVencimento.ToString("dd/MM/yyyy HH:mm")}");
                Logar($"ValorBoleto : {boletoBancarioAvulso.Boleto.ValorBoleto.ToString("C", culture)}");
                Logar($"CpfCnpjSolicitante : {boletoBancarioAvulso.CpfCnpjSolicitante}");
                Logar($"NomeSolicitante : {boletoBancarioAvulso.NomeSolicitante}");
            }
        }

        #endregion

        #region M�todos de apoio...

        private void SetOcupado(bool ocupado) {

            _ocupado = ocupado;
            progressBar1.Visible = ocupado;
            btnGerarBoleto.Enabled = !ocupado;
        }

        private void Logar(string mensagem) {

            txtBoxMensagens.BeginInvoke(new Action(() => { 
            
                txtBoxMensagens.AppendText(mensagem + "\r\n");
            }));
        }

        #endregion
    }
}
