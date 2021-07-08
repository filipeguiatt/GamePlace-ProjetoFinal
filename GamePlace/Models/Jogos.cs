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
        public string Nome { get; set; }


        /// <summary>
        /// Capa do Jogo
        /// </summary>
        public string FotoCapa { get; set; }

        /// <summary>
        /// Descricao do Jogo
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Tamanho do Jogo
        /// </summary>
        public string Tamanho { get; set; }

        /// <summary>
        /// Data de Lançamento do Jogo
        /// </summary>
        public string AnoLancamento { get; set; }

        /// <summary>
        /// Genero do Jogo
        /// </summary>
        public string Genero { get; set; }

        /// <summary>
        /// Preco do Jogo
        /// </summary>
        public string Preco { get; set; }

        /// <summary>
        /// Classificação do Jogo
        /// </summary>
        public string Classificacao { get; set; }

        /// <summary>
        /// Faixa Etária do Jogo
        /// </summary>
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
