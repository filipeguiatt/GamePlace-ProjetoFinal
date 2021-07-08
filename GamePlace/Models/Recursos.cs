using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlace.Models
{
    public class Recursos
    {

        /// <summary>
        /// Identificador de cada Recurso
        /// </summary>
        [Key]
        public int IdRecursos { get; set; }

        /// <summary>
        /// Nome do Recurso
        /// </summary>
        public string NomeRecurso { get; set; }

        /// <summary>
        /// FK para os recursos do Jogo
        /// </summary>
        [ForeignKey(nameof(Jogo))]
        public int JogoFK { get; set; }
        public Jogos Jogo { get; set; }







    }
}
