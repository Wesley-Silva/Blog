using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.App.ViewModels
{
    public class CategoriaViewModel
    {
        [Key]
        [Display(Name = "Código")]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres ", MinimumLength = 2)]
        public string Nome { get; set; }


        public IEnumerable<PostViewModel> Posts { get; set; }
    }
}
