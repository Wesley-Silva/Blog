using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.App.ViewModels
{
    public class ComentarioViewModel
    {
        [Key]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Post")]
        public Guid PostId { get; set; }
      

        [DisplayName("Corpo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(5000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres ", MinimumLength = 5)]
        public string Corpo { get; set; }

        public Guid UsuarioId { get; set; }

        public DateTime Criado { get; set; } 
    }
}
