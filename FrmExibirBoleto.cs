using Microsoft.Web.WebView2.WinForms;

namespace Poc.BoletoSre2 {

    public class FrmExibirBoleto : Form {

        private readonly string _caminhoArquivo;
        private readonly WebView2 _webView;

        public FrmExibirBoleto(string caminhoArquivo) {

            _caminhoArquivo = caminhoArquivo;
            _webView = new WebView2 { Dock = DockStyle.Fill };
            Controls.Add(_webView);

            Text = "Boleto bancário";
            FormBorderStyle = FormBorderStyle.Sizable;
            StartPosition = FormStartPosition.Manual;
            Width = 900;

            var area = Screen.FromPoint(Cursor.Position).WorkingArea;
            Top = area.Top;
            Height = area.Height;
            Left = area.Left + (area.Width - Width) / 2;

            Load += FrmExibirBoleto_Load;
        }

        private async void FrmExibirBoleto_Load(object sender, EventArgs e) {

            try {
                await _webView.EnsureCoreWebView2Async();
                _webView.CoreWebView2.Navigate(new Uri(_caminhoArquivo).AbsoluteUri);
            }
            catch (Exception ex) {
                MessageBox.Show(this, "Não foi possível exibir o PDF: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void Dispose(bool disposing) {

            if (disposing) {
                _webView?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
