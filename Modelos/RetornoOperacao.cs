using Newtonsoft.Json;
using System.Text;

namespace Poc.BoletoSre2.Modelos {

    /// <summary>
    /// Representação do status de retorno de uma operação...
    /// </summary>
    public class StatusRetorno {

        #region Propriedades...

        /// <summary>
        /// Código do status...
        /// </summary>
        [JsonProperty("codigo")]
        public string Codigo { get; set; }

        /// <summary>
        /// Mensagem de status...
        /// </summary>
        [JsonProperty("mensagem")]
        public string Mensagem { get; set; }

        #endregion

        #region Métodos de suporte...

        /// <summary>
        /// Método ToString() customizado...
        /// </summary>
        /// <returns>Uma string contendo os valores de código e mensagem no formato {codigo} - {mensagem}.</returns>
        public override string ToString() {

            return $"{Codigo} - {Mensagem}";
        }

        #endregion
    }

    /// <summary>
    /// Representação de uma informação genérica associada a uma operação...
    /// </summary>
    public class OperacaoInfo {

        #region Propriedades...

        /// <summary>
        /// Código de uma informação de operação...
        /// </summary>
        [JsonProperty("codigo", NullValueHandling = NullValueHandling.Ignore)]
        public string Codigo { get; set; }

        /// <summary>
        /// Mensagem de uma informação de operação...
        /// </summary>
        [JsonProperty("mensagem", NullValueHandling = NullValueHandling.Ignore)]
        public string Mensagem { get; set; }

        /// <summary>
        /// Nome de campo associado a mensagem...
        /// </summary>
        [JsonProperty("nomeCampo", NullValueHandling = NullValueHandling.Ignore)]
        public string NomeCampo { get; set; }

        #endregion

        #region Métodos de suporte...

        /// <summary>
        /// Método ToString() customizado...
        /// </summary>
        /// <returns>Uma string contendo os valores de código e mensagem no formato {codigo} - {mensagem}.</returns>
        public override string ToString() {

            return string.Format("{0} - {1}", Codigo, Mensagem);
        }

        #endregion
    }

    /// <summary>
    /// Representação de um retorno de operação...
    /// </summary>
    public partial class RetornoOperacao {

        #region Properties...

        /// <summary>
        /// Status de retorno de uma operação...
        /// </summary>
        public StatusRetorno Status { get; set; }

        /// <summary>
        /// Listagem de mensagens retornadas pela operação...
        /// </summary>
        public List<OperacaoInfo> Mensagens { get; set; }

        #endregion

        #region Construtores...

        /// <summary>
        /// Construtor básico da classe...
        /// </summary>
        public RetornoOperacao() {

            Status = new StatusRetorno();
            Mensagens = new List<OperacaoInfo>();
        }

        #endregion

        #region Methods...

        /// <summary>
        /// Método ToString() customizado.
        /// </summary>
        /// <returns>String contendo o conteúdo de Codigo and Mensagem no formado: "{codigo} - {mensagem}".</returns>
        public override string ToString() {

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0} - {1}", Status.Codigo, Status.Mensagem);

            if (Mensagens != null && Mensagens.Any()) {
                sb.Append(" Detalhes: ");
                Mensagens.ToList().ForEach(item => sb.AppendFormat("{0} - {1} ; ", item.Codigo, item.Mensagem));
            }

            return sb.ToString();
        }

        #endregion
    }

    /// <summary>
    /// Representação de um retorno de operação (Edição Generics)...
    /// </summary>
    public class RetornoOperacao<T> : RetornoOperacao {

        #region Properties...

        /// <summary>
        /// Informação transportada nesta instância como um dado retornado...
        /// </summary>
        public T Data { get; set; }

        #endregion

        #region Construtores...

        /// <summary>
        /// Construtor básico da classe...
        /// </summary>
        public RetornoOperacao() : base() {

            if (typeof(T).Equals(typeof(string)) || typeof(T).IsInterface || typeof(T).IsAbstract) return;
            Data = Activator.CreateInstance<T>();
        }

        #endregion
    }

    /// <summary>
    /// Representação de um retorno de operação (Edição Generics) com suporte a tipagem de erro...
    /// </summary>
    public class RetornoOperacao<T, E> : RetornoOperacao {

        #region Properties...

        /// <summary>
        /// Informação transportada nesta instância como um dado retornado...
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Erro ocorrido em operação...
        /// </summary>
        public E Erro { get; set; } = default(E);

        #endregion

        #region Construtores...

        /// <summary>
        /// Construtor básico da classe...
        /// </summary>
        public RetornoOperacao() : base() {

            if (typeof(T).Equals(typeof(string)) || typeof(T).IsInterface || typeof(T).IsAbstract) return;
            Data = Activator.CreateInstance<T>();
        }

        #endregion
    }
}
