using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlace.Models
{
    public class Favoritos
    {
        public Favoritos()
        {
            // inicializar a lista de Compras do Jogo
            ListaJogos = new HashSet<Jogos>();

        }


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
        [ForeignKey(nameof(JogoFavorit))]
        public int JogoFK { get; set; }
        public Jogos JogoFavorit { get; set; }


        /// <summary>
        /// FK para o Utilizador Registado
        /// </summary>
        [ForeignKey(nameof(User))]
        public int UtilizadorFK { get; set; }
        public UtilizadorRegistado User { get; set; }

        /// <summary>
        /// lista de jogos
        /// </summary>
        public ICollection<Jogos> ListaJogos { get; set; }





    }
}
