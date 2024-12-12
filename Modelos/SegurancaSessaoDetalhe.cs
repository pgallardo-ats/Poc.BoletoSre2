namespace Poc.BoletoSre2.Modelos {

    /// <summary>
    /// 
    /// </summary>
    public class SegurancaSessaoDetalhe {

        #region Propriedades...

        public bool CredencialInvalida { get; set; }

        public bool Conflito { get; set; }

        public bool ImpossivelVerificarCertificado { get; set; }

        public bool SessaoExpirada { get; set; }

        public string Detalhe { get; set; }

        #endregion
    }
}
