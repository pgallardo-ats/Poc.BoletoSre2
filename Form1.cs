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

        #region Handlers do formulário...

        private void Form1_Load(object sender, EventArgs e) {

            string msg = "Formulário carregado...";
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
            await Task.Delay(3000);
            SetOcupado(false);
        }

        private async void btnExibirBoleto_Click(object sender, EventArgs e) {

            SetOcupado(true);
            await ExibirBoletoBancarioAsync();
            await Task.Delay(3000);
            SetOcupado(false);
        }

        #endregion

        #region Métodos principais...

        public async Task GerarBoletoAsync() {

            string login = txtBoxLogin.Text;
            string senha = txtBoxSenha.Text;

            _cts = new CancellationTokenSource();

            var ticket = await _segurancaGateway.RealizarLoginAsync(login, senha, _cts.Token).ConfigureAwait(false);
            if (ticket == null) {
                Logar("ERRO: Login não teve sucesso, confira o log...");
                return;
            }
            else {
                Logar($"LOGIN: Login com sucesso para login: \"{login}\", UsuarioID: {ticket.UsuarioId}");
            }

            Logar("Gerando boleto bancário...");
            DadosBoletoBancarioCriacao dadosBoleto = new DadosBoletoBancarioCriacao() {
                AtoId = 169, // 860	- Emissão de certificado digital
                EventoId = 311, // 861 - Emissão de certificado digital e-CPF A1
                AtoEventoId = 529, // AtoID: 169 X EventoID: 311
                CpfCnpjSolicitante = UtilHelper.GerarCpf(),
                NomeSolicitante = $"Fulano {UtilHelper.ObterStringAleatoria(10)} da Silva Testes",
                ValorAtoEvento = 1234,
                ValorBoleto = 1234,
            };
            var boletoBancarioAvulso = await _integracaoBancariaGateway.CriarBoletoAvulsoAsync(dadosBoleto, ticket, "127.0.0.1", _cts.Token).ConfigureAwait(false);
            if (boletoBancarioAvulso == null) {
                Logar("ERRO: Erro ao gerar boleto bancário, confira o log..");
                return;
            }
            else {
                Logar($"Gerado boleto bancário...\r\nBoletoID : {boletoBancarioAvulso.BoletoId}\r\nNumeroBoleto : {boletoBancarioAvulso.Boleto.NumeroBoleto}");
            }

            // Buscar informações do boleto com base no BoletoID...
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
                Logar($"ERRO: Ocorreu erro ao tentar buscar boleto número: {numeroBoleto}, confira o log...");
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

        public async Task ExibirBoletoBancarioAsync() {

            // OBS:
            // Até o momento desta implementação, o provedor de serviços bancários de boleto usado pela JUCERJA tem
            // o problema técnico de não disponibilizar um ambiente de testes, apenas de produção. Por isso boletos gerados
            // no ambiente de testes da JUCERJA sofrem as seguintes alterações ao serem gerados no ambiente de produção do provedor:
            // - Nome do solicitante do boleto é alterado para um funcionário pré-definido da JUCERJA.
            // - Endereço do solicitante é alterado para o endereço da JUCERJA.
            // - Valor do boleto é alterado para R$ 0,05.
            // Isto irá se reflitir nos PDFs dos boletos gerados no ambiente de testes da JUCERJA. No ambiente de produção
            // todos os valores enviados para a API da integração bancária do SRE são preservados.

            bool boletoInformado = int.TryParse(txtBoletoID.Text, new CultureInfo("pt-BR"), out int boletoId);
            if (!boletoInformado) {
                Logar("ERRO: Informe um BoletoID válido para exibir o boleto bancário...");
                return;
            }

            Logar($"\r\nExibir boleto bancário, BoletoID: {boletoId}");
            _cts = new CancellationTokenSource();

            var ticket = await _segurancaGateway.RealizarLoginAsync(txtBoxLogin.Text, txtBoxSenha.Text, _cts.Token).ConfigureAwait(false);
            if (ticket == null) {
                Logar("ERRO: Login não teve sucesso, confira o log...");
                return;
            }
            else {
                Logar($"LOGIN: Login com sucesso para: \"{txtBoxLogin.Text}\", UsuarioID: {ticket.UsuarioId}");
            }

            string nomeArquivo = $"boleto-{boletoId}-{Ulid.NewUlid()}.pdf";
            string caminho = AppDomain.CurrentDomain.BaseDirectory;

            // Obter o arquivo PDF do boleto bancário...
            var arquivo = await _integracaoBancariaGateway.ObterBoletoBancarioArquivoAsync(boletoId, caminho, nomeArquivo, ticket, "", _cts.Token).ConfigureAwait(false);
            if (arquivo == null) {
                Logar($"ERRO: Ocorreu erro ao tentar obter o arquivo do boleto bancário, BoletoID: {boletoId}, confira o log...");
                return;
            }

            if (File.Exists(arquivo.Caminho)) {
                Logar("Boleto bancário gerado em: " + arquivo.Caminho);

                string caminhoArquivo = arquivo.Caminho;
                BeginInvoke(new Action(() => {
                    var frm = new FrmExibirBoleto(caminhoArquivo);
                    frm.Show(this);
                }));

            }
        }

        private void SetOcupado(bool ocupado) {

            _ocupado = ocupado;
            progressBar1.Visible = _ocupado;
            btnGerarBoleto.Enabled = !_ocupado;
            btnExibirBoleto.Enabled = !_ocupado;
        }

        private void Logar(string mensagem) {

            txtBoxMensagens.BeginInvoke(new Action(() => {

                txtBoxMensagens.AppendText(mensagem + "\r\n");
            }));
        }

        #endregion
    }
}
