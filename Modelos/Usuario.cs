namespace Poc.BoletoSre2.Modelos {

    /// <summary>
    /// Representação de um usuário da JUCERJA perante a integração com o JUCERJA-SSEG...
    /// </summary>
    public class Usuario {

        #region Propriedades...

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ModoPersistencia { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Cpf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HashSenha { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Bloqueado { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Excluido { get; set; }

        /// <summary>
        /// Indica o tipo de usuário, podendo ser: 1- JUCERJA; 2- Delegacia; 4- Associação; 8- Internet; 16- Extranet; 32- Gestor Extranet; 64; Sistema. 
        /// Um usuário pode possuir mais de um tipo, por exemplo, ser da JUCERJA e Internet, com isso, seu tipo será a soma do 1 com o 8 e seu tipo será 9.
        /// </summary>
        public int TipoUsuario { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? UsuarioInternetId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IntegracaoGovBr { get; set; }

        #endregion
    }
}
