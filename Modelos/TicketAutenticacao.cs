namespace Poc.BoletoSre2.Modelos {

    /// <summary>
    /// Representação de um Ticket de autenticação de um Usuário JUCERJA...
    /// </summary>
    public class TicketAutenticacao {

        #region Propriedades...

        /// <summary>
        /// Login do Usuário...
        /// </summary>
        public string LoginUsuario { get; set; }

        /// <summary>
        /// Nome do Usuário...
        /// </summary>
        public string NomeUsuario { get; set; }

        /// <summary>
        /// Identificador de instância do usuário...
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Data/Hora da autenticação...
        /// </summary>
        public DateTime MomentoAutenticacao { get; set; }

        /// <summary>
        /// Data/hora da expiração do ticket...
        /// </summary>
        public DateTime ExpiraAutenticacao { get; set; }

        /// <summary>
        /// Endereço IP da origem de chamada de login...
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Sequencia...
        /// </summary>
        public string Sequencia { get; set; }

        #endregion
    }
}
