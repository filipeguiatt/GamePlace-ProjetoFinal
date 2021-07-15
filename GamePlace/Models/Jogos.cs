using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GamePlace.Models
{
    public class Jogos
    {

        public Jogos()
        {
            // inicializar a lista de Recursos do Jogo
            ListaRecursos = new HashSet<Recursos>();

            // inicializar a lista de Compras do Jogo
            ListaCompras = new HashSet<Compras>();
        }

        /// <summary>
        /// Identificador de cada Jogo
        /// </summary>
        [Key]
        public int IdJogo { get; set; }

        /// <summary>
        /// Nome do Jogo
        /// </summary>
        [Required(ErrorMessage = "O Nome do Jogo é de preenchimento obrigatório")]
        [StringLength(20, ErrorMessage = "O {0} não pode ter mais de {1} caracteres.")]
        public string Nome { get; set; }

        /// <summary>
        /// Capa do Jogo
        /// </summary>
        public string FotoCapa { get; set; }

        /// <summary>
        /// Descricao do Jogo
        /// </summary>

        [Required(ErrorMessage = "A Descrição do Jogo é de preenchimento obrigatório")]
        [StringLength(400, ErrorMessage = "O {0} não pode ter mais de {1} caracteres.")]
        public string Descricao { get; set; }

        /// <summary>
        /// Tamanho do Jogo
        /// </summary>
        [Required(ErrorMessage = "O Tamanho do Jogo é de preenchimento obrigatório")]
        [StringLength(5, ErrorMessage = "O {0} não pode ter mais de {1} caracteres.")]
        [RegularExpression("([0-9])Gb", ErrorMessage = "Só são aceites Gb como medida de tamanho.")]
        public string Tamanho { get; set; }

        /// <summary>
        /// Data de Lançamento do Jogo
        /// </summary>
        [Required(ErrorMessage = "O Ano de Lançamento é de preenchimento obrigatório")]
        [StringLength(4, ErrorMessage = "O {0} não pode ter mais de {1} caracteres.")]
        [RegularExpression("([0-9])", ErrorMessage = "Só são aceites algarismos.")]
        public string AnoLancamento { get; set; }

        /// <summary>
        /// Genero do Jogo
        /// </summary>
        [Required(ErrorMessage = "O Genero é de preenchimento obrigatório")]
        [StringLength(15, ErrorMessage = "O {0} não pode ter mais de {1} caracteres.")]
        [RegularExpression("([a-z][A-Z])", ErrorMessage = "Só são aceites caracteres.")]
        public string Genero { get; set; }

        /// <summary>
        /// Preco do Jogo
        /// </summary>
        [Required(ErrorMessage = "O Preço é de preenchimento obrigatório")]
        [StringLength(6, ErrorMessage = "O {0} não pode ter mais de {1} caracteres.")]
        [RegularExpression("(([0-9],)€)", ErrorMessage = "Só são aceites algarismos e € como moeda de pagamento.")]
        public string Preco { get; set; }

        /// <summary>
        /// Classificação do Jogo
        /// </summary>
        [Required(ErrorMessage = "A Classificacao é de preenchimento obrigatório")]
        [StringLength(3, ErrorMessage = "O {0} não pode ter mais de {1} caracteres.")]
        [RegularExpression("([0-9],)", ErrorMessage = "Só são aceites algarismos.")]
        public string Classificacao { get; set; }

        /// <summary>
        /// Faixa Etária do Jogo
        /// </summary>
        [Required(ErrorMessage = "A Classificacao é de preenchimento obrigatório")]
        [StringLength(3, ErrorMessage = "O {0} não pode ter mais de {1} caracteres.")]
        [RegularExpression("(([0-9])+)", ErrorMessage = "Só são aceites algarismos.")]
        public string FaixaEtaria { get; set; }

        /// <summary>
        /// lista de Recursos associados ao Jogo
        /// </summary>
        public ICollection<Recursos> ListaRecursos { get; set; }

        /// <summary>
        /// lista de Compras associados ao Jogo
        /// </summary>
        public ICollection<Compras> ListaCompras { get; set; }
    }
}