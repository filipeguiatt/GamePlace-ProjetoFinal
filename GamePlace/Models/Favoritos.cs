using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlace.Models
{
    public class Favoritos
    {


        /// <summary>
        /// Identificador de cada Recurso
        /// </summary>
        [Key]
        public int IdFavorito { get; set; }

        /// <summary>
        /// Nome do Recurso
        /// </summary>
        public string JogoFavorito { get; set; }


        /// <summary>
        /// FK para o id do Jogo
        /// </summary>
        [ForeignKey(nameof(JogoFav))]
        public int JogoFK { get; set; }
        public Jogos JogoFav { get; set; }


        /// <summary>
        /// FK para o Utilizador Registado
        /// </summary>
        [ForeignKey(nameof(User))]
        public int UtilizadorFK { get; set; }
        public UtilizadorRegistado User { get; set; }






    }
}