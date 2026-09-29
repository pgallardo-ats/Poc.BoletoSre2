namespace Poc.BoletoSre2.Modelos {

    /// <summary>
    /// Representação básica de um arquivo de uso interno do sistema...
    /// </summary>
    public class ArquivoInterno {

        #region Propriedades...

        /// <summary>
        /// Nome do arquivo (nome + extensão), para fins de apresentação...
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Caminho completo até o arquivo (path + nome.arquivo + extensão)...
        /// </summary>
        public string Caminho { get; set; }

        #endregion
    }
}
