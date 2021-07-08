using System;
using System.Collections.Generic;

namespace GamePlace.Models
{

    /// <summary>
    /// view model para transportar os dados dos jogos
    /// para a API
    /// </summary>
    public class ListaJogosApiViewModel
    {

        /// <summary>
        /// id do Jogo
        /// </summary>
        public int IdJogo { get; set; }

        /// <summary>
        /// nome do Jogo
        /// </summary>
        public string NomeJogo { get; set; }
    }

    /// <summary>
    /// view model para transportar os dados pesquisados, dentro da API
    /// </summary>
    public class ListaRecursosApiViewModel
    {

        /// <summary>
        /// identificador
        /// </summary>
        public int IdRecurso { get; set; }

        /// <summary>
        /// nome dos recursos
        /// </summary>
        public string NomeRecurso { get; set; }


        /// <summary>
        /// nome do Jogo
        /// </summary>
        public string NomeJogo { get; set; }
    }



    /// <summary>
    /// classe usada para transportar os dados necessários 
    /// à correta visualização dos recursos na respetiva interface
    /// </summary>
    public class ListarRecursosViewModel
    {

        /// <summary>
        /// lista de recursos
        /// </summary>
        public ICollection<Recursos> ListaRecursos { get; set; }

        /// <summary>
        /// Lista dos IDs dos jogos da pessoa que está autenticada
        /// </summary>
        public ICollection<int> ListaJogos { get; set; }
    }
}
