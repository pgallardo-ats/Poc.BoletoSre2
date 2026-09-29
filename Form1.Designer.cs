namespace Poc.BoletoSre2 {

    partial class Form1 {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent() {
            progressBar1 = new ProgressBar();
            txtBoxMensagens = new TextBox();
            tableLayoutPanelCentro = new TableLayoutPanel();
            groupBoxGerarBoleto = new GroupBox();
            tableLayoutPanelGerar = new TableLayoutPanel();
            label1 = new Label();
            txtBoxLogin = new TextBox();
            label2 = new Label();
            txtBoxSenha = new TextBox();
            btnGerarBoleto = new Button();
            groupBoxExibirBoleto = new GroupBox();
            tableLayoutPanelExibir = new TableLayoutPanel();
            labelBoletoId = new Label();
            txtBoletoID = new TextBox();
            btnExibirBoleto = new Button();
            tableLayoutPanelCentro.SuspendLayout();
            groupBoxGerarBoleto.SuspendLayout();
            tableLayoutPanelGerar.SuspendLayout();
            groupBoxExibirBoleto.SuspendLayout();
            tableLayoutPanelExibir.SuspendLayout();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.Dock = DockStyle.Top;
            progressBar1.Location = new Point(0, 0);
            progressBar1.Margin = new Padding(0);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(900, 18);
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.TabIndex = 0;
            progressBar1.Visible = false;
            // 
            // txtBoxMensagens
            // 
            txtBoxMensagens.BackColor = Color.White;
            txtBoxMensagens.Dock = DockStyle.Bottom;
            txtBoxMensagens.Font = new Font("Courier New", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBoxMensagens.Location = new Point(0, 335);
            txtBoxMensagens.Multiline = true;
            txtBoxMensagens.Name = "txtBoxMensagens";
            txtBoxMensagens.ReadOnly = true;
            txtBoxMensagens.ScrollBars = ScrollBars.Both;
            txtBoxMensagens.Size = new Size(900, 129);
            txtBoxMensagens.TabIndex = 1;
            // 
            // tableLayoutPanelCentro
            // 
            tableLayoutPanelCentro.ColumnCount = 2;
            tableLayoutPanelCentro.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelCentro.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelCentro.Controls.Add(groupBoxGerarBoleto, 0, 0);
            tableLayoutPanelCentro.Controls.Add(groupBoxExibirBoleto, 1, 0);
            tableLayoutPanelCentro.Dock = DockStyle.Fill;
            tableLayoutPanelCentro.Location = new Point(0, 18);
            tableLayoutPanelCentro.Name = "tableLayoutPanelCentro";
            tableLayoutPanelCentro.Padding = new Padding(12);
            tableLayoutPanelCentro.RowCount = 1;
            tableLayoutPanelCentro.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelCentro.Size = new Size(900, 317);
            tableLayoutPanelCentro.TabIndex = 2;
            // 
            // groupBoxGerarBoleto
            // 
            groupBoxGerarBoleto.Controls.Add(tableLayoutPanelGerar);
            groupBoxGerarBoleto.Dock = DockStyle.Fill;
            groupBoxGerarBoleto.Location = new Point(18, 18);
            groupBoxGerarBoleto.Margin = new Padding(6);
            groupBoxGerarBoleto.Name = "groupBoxGerarBoleto";
            groupBoxGerarBoleto.Padding = new Padding(12);
            groupBoxGerarBoleto.Size = new Size(426, 281);
            groupBoxGerarBoleto.TabIndex = 0;
            groupBoxGerarBoleto.TabStop = false;
            groupBoxGerarBoleto.Text = "Gerar Boleto";
            // 
            // tableLayoutPanelGerar
            // 
            tableLayoutPanelGerar.ColumnCount = 1;
            tableLayoutPanelGerar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelGerar.Controls.Add(label1, 0, 0);
            tableLayoutPanelGerar.Controls.Add(txtBoxLogin, 0, 1);
            tableLayoutPanelGerar.Controls.Add(label2, 0, 2);
            tableLayoutPanelGerar.Controls.Add(txtBoxSenha, 0, 3);
            tableLayoutPanelGerar.Controls.Add(btnGerarBoleto, 0, 5);
            tableLayoutPanelGerar.Dock = DockStyle.Fill;
            tableLayoutPanelGerar.Location = new Point(12, 28);
            tableLayoutPanelGerar.Name = "tableLayoutPanelGerar";
            tableLayoutPanelGerar.RowCount = 6;
            tableLayoutPanelGerar.RowStyles.Add(new RowStyle());
            tableLayoutPanelGerar.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanelGerar.RowStyles.Add(new RowStyle());
            tableLayoutPanelGerar.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanelGerar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelGerar.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tableLayoutPanelGerar.Size = new Size(402, 241);
            tableLayoutPanelGerar.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(0, 0);
            label1.Margin = new Padding(0, 0, 0, 4);
            label1.Name = "label1";
            label1.Size = new Size(402, 15);
            label1.TabIndex = 0;
            label1.Text = "Login:";
            // 
            // txtBoxLogin
            // 
            txtBoxLogin.Dock = DockStyle.Fill;
            txtBoxLogin.Font = new Font("Courier New", 11F, FontStyle.Bold);
            txtBoxLogin.Location = new Point(0, 19);
            txtBoxLogin.Margin = new Padding(0, 0, 0, 8);
            txtBoxLogin.Name = "txtBoxLogin";
            txtBoxLogin.PlaceholderText = "Login de usuário da JUCERJA";
            txtBoxLogin.Size = new Size(402, 24);
            txtBoxLogin.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(0, 51);
            label2.Margin = new Padding(0, 0, 0, 4);
            label2.Name = "label2";
            label2.Size = new Size(402, 15);
            label2.TabIndex = 2;
            label2.Text = "Senha:";
            // 
            // txtBoxSenha
            // 
            txtBoxSenha.Dock = DockStyle.Fill;
            txtBoxSenha.Font = new Font("Courier New", 10F, FontStyle.Bold);
            txtBoxSenha.Location = new Point(0, 70);
            txtBoxSenha.Margin = new Padding(0);
            txtBoxSenha.Name = "txtBoxSenha";
            txtBoxSenha.PasswordChar = '*';
            txtBoxSenha.PlaceholderText = "Senha de usuário da JUCERJA";
            txtBoxSenha.Size = new Size(402, 23);
            txtBoxSenha.TabIndex = 3;
            txtBoxSenha.UseSystemPasswordChar = true;
            // 
            // btnGerarBoleto
            // 
            btnGerarBoleto.Dock = DockStyle.Fill;
            btnGerarBoleto.Location = new Point(0, 197);
            btnGerarBoleto.Margin = new Padding(0);
            btnGerarBoleto.Name = "btnGerarBoleto";
            btnGerarBoleto.Size = new Size(402, 44);
            btnGerarBoleto.TabIndex = 4;
            btnGerarBoleto.Text = "Gerar Boleto";
            btnGerarBoleto.UseVisualStyleBackColor = true;
            btnGerarBoleto.Click += btnGerarBoleto_Click;
            // 
            // groupBoxExibirBoleto
            // 
            groupBoxExibirBoleto.Controls.Add(tableLayoutPanelExibir);
            groupBoxExibirBoleto.Dock = DockStyle.Fill;
            groupBoxExibirBoleto.Location = new Point(456, 18);
            groupBoxExibirBoleto.Margin = new Padding(6);
            groupBoxExibirBoleto.Name = "groupBoxExibirBoleto";
            groupBoxExibirBoleto.Padding = new Padding(12);
            groupBoxExibirBoleto.Size = new Size(426, 281);
            groupBoxExibirBoleto.TabIndex = 1;
            groupBoxExibirBoleto.TabStop = false;
            groupBoxExibirBoleto.Text = "Exibir Boleto";
            // 
            // tableLayoutPanelExibir
            // 
            tableLayoutPanelExibir.ColumnCount = 1;
            tableLayoutPanelExibir.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelExibir.Controls.Add(labelBoletoId, 0, 0);
            tableLayoutPanelExibir.Controls.Add(txtBoletoID, 0, 1);
            tableLayoutPanelExibir.Controls.Add(btnExibirBoleto, 0, 3);
            tableLayoutPanelExibir.Dock = DockStyle.Fill;
            tableLayoutPanelExibir.Location = new Point(12, 28);
            tableLayoutPanelExibir.Name = "tableLayoutPanelExibir";
            tableLayoutPanelExibir.RowCount = 4;
            tableLayoutPanelExibir.RowStyles.Add(new RowStyle());
            tableLayoutPanelExibir.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanelExibir.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelExibir.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tableLayoutPanelExibir.Size = new Size(402, 241);
            tableLayoutPanelExibir.TabIndex = 0;
            // 
            // labelBoletoId
            // 
            labelBoletoId.AutoSize = true;
            labelBoletoId.Dock = DockStyle.Fill;
            labelBoletoId.Location = new Point(0, 0);
            labelBoletoId.Margin = new Padding(0, 0, 0, 4);
            labelBoletoId.Name = "labelBoletoId";
            labelBoletoId.Size = new Size(402, 15);
            labelBoletoId.TabIndex = 0;
            labelBoletoId.Text = "Boleto ID:";
            // 
            // txtBoletoID
            // 
            txtBoletoID.Dock = DockStyle.Fill;
            txtBoletoID.Font = new Font("Courier New", 11F, FontStyle.Bold);
            txtBoletoID.Location = new Point(0, 19);
            txtBoletoID.Margin = new Padding(0);
            txtBoletoID.Name = "txtBoletoID";
            txtBoletoID.PlaceholderText = "Informe o Boleto ID";
            txtBoletoID.Size = new Size(402, 24);
            txtBoletoID.TabIndex = 1;
            // 
            // btnExibirBoleto
            // 
            btnExibirBoleto.Dock = DockStyle.Fill;
            btnExibirBoleto.Location = new Point(0, 197);
            btnExibirBoleto.Margin = new Padding(0);
            btnExibirBoleto.Name = "btnExibirBoleto";
            btnExibirBoleto.Size = new Size(402, 44);
            btnExibirBoleto.TabIndex = 2;
            btnExibirBoleto.Text = "Exibir Boleto Bancário";
            btnExibirBoleto.UseVisualStyleBackColor = true;
            btnExibirBoleto.Click += btnExibirBoleto_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 464);
            Controls.Add(tableLayoutPanelCentro);
            Controls.Add(txtBoxMensagens);
            Controls.Add(progressBar1);
            MinimumSize = new Size(720, 480);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Boleto SRE ";
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            tableLayoutPanelCentro.ResumeLayout(false);
            groupBoxGerarBoleto.ResumeLayout(false);
            tableLayoutPanelGerar.ResumeLayout(false);
            tableLayoutPanelGerar.PerformLayout();
            groupBoxExibirBoleto.ResumeLayout(false);
            tableLayoutPanelExibir.ResumeLayout(false);
            tableLayoutPanelExibir.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar progressBar1;
        private TextBox txtBoxMensagens;
        private TableLayoutPanel tableLayoutPanelCentro;
        private GroupBox groupBoxGerarBoleto;
        private TableLayoutPanel tableLayoutPanelGerar;
        private Label label1;
        private Label label2;
        private GroupBox groupBoxExibirBoleto;
        private TableLayoutPanel tableLayoutPanelExibir;
        private Label labelBoletoId;
        private TextBox txtBoletoID;
        private Button btnExibirBoleto;
        private TextBox txtBoxLogin;
        private TextBox txtBoxSenha;
        private Button btnGerarBoleto;
    }
}
