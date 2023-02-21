using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.App.ViewModels
{
    public class PostViewModel
    {
        [Key]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Categoria")]
        public Guid CategoriaId { get; set; }


        [DisplayName("Titulo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres ", MinimumLength = 2)]
        public string Titulo { get; set; }


        [DisplayName("Corpo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(5000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres ", MinimumLength = 2)]
        public string Corpo { get; set; }


        public DateTime CriadoEm { get; set; }


        public DateTime AtualizadoEm { get; set; }


        [DisplayName("Imagem")]

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public IFormFile ImagemUpload { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Imagem { get; set; }


        public string Email { get; set; }

        
        public int Contador { get; set; }

        public CategoriaViewModel Categoria { get; set; }
        public IEnumerable<CategoriaViewModel> Categorias { get; set; }

        public ComentarioViewModel Comentario { get; set; }
        public IEnumerable<ComentarioViewModel> Comentarios { get; set; }

    }
}
