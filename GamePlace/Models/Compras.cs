using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlace.Models
{
    public class Compras
    {

        /// <summary>
        /// PK para a tabela do relacionamento entre Jogo e Utilizadores
        /// </summary>
        [Key]
        public int IdCompra { get; set; }

        /// <summary>
        /// Data de compra do Jogo
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Chave de ativação do Jogo
        /// </summary>
        public string ChaveAtivacao { get; set; }

        //*************************************************************

        /// <summary>
        /// FK para o id do Jogo
        /// </summary>
        [ForeignKey(nameof(Jogo))]
        public int JogoFK { get; set; }
        public Jogos Jogo { get; set; }


        /// <summary>
        /// FK para o Utilizador Registado
        /// </summary>
        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFK { get; set; }
        public UtilizadorRegistado Utilizador { get; set; }



    }
}
