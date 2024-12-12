namespace Poc.BoletoSre2.Modelos {

    /// <summary>
    /// 
    /// </summary>
    public class SegurancaSessaoFaultContract : ProblemDetails {

        #region Propriedades...

        /// <summary>
        /// 
        /// </summary>
        public bool? FaultException { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TipoDetalhe { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SegurancaSessaoDetalhe Detalhe { get; set; }

        #endregion
    }
}
