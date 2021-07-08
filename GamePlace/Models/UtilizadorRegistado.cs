using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GamePlace.Models
{
    public class UtilizadorRegistado
    {

        public UtilizadorRegistado()
        {
            // inicializar a lista de Compras do Jogo
            ListaCompras = new HashSet<Compras>();

        }

        /// <summary>
        /// identificador do utilizador
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome do utilizador
        /// </summary>
        [Required(ErrorMessage = "O Nome é de preenchimento obrigatório")]
        [StringLength(60, ErrorMessage = "O {0} não pode ter mais de {1} caracteres.")]
        public string Nome { get; set; }

        /// <summary>
        /// Foto do utilizador
        /// </summary>
        public string FotoUtilizador { get; set; }

        /// <summary>
        /// Morada
        /// </summary>
        [Required(ErrorMessage = "A Morada é de preenchimento obrigatório")]
        [StringLength(60, ErrorMessage = "A {0} não pode ter mais de {1} caracteres.")]
        public string Morada { get; set; }

        /// <summary>
        /// Código Postal
        /// </summary>
        [Required(ErrorMessage = "Deve escrever o {0}")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres.")]
        [Display(Name = "Código Postal")]
        public string CodPostal { get; set; }

        /// <summary>
        /// Telemóvel
        /// </summary>
        [StringLength(14, MinimumLength = 9, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres.")]
        [RegularExpression("(00)?([0-9]{2,3})?[1-9][0-9]{8}", ErrorMessage = "Escreva, por favor, um nº Telemóvel com 9 algarismos. Se quiser, pode acrescentar o indicativo nacional e o internacional. ")]
        [Display(Name = "Telemóvel")]
        public string Telemovel { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Deve escrever um email valido")]
        public string Email { get; set; }

        //###########################################################################
        // FK para a tabela de Autenticação
        //###########################################################################
        /// <summary>
        /// Chave de ligação entre a Autenticação e os Criadores 
        /// Consegue-se, por exemplo, filtrar os dados dos criadores qd se autenticam
        /// </summary>
        public string UserNameId { get; set; }
        //###########################################################################

        /// <summary>
        /// lista de Compras associados ao Jogo
        /// </summary>
        public ICollection<Compras> ListaCompras { get; set; }






    }
}
