using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamePlace.Models
{
    public class APIJogosViewModel
    {
        /// <summary>
        /// Id do Jogo
        /// </summary>
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

        



    }
}
