using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Business.Models
{
    public class Post : Entity
    {
        public Guid CategoriaId { get; set; }

        public string Titulo { get; set; }

        public string Corpo { get; set; }

        public DateTime CriadoEm { get; set; }

        public DateTime AtualizadoEm { get; set; }

        public string Imagem { get; set; }

        public string Email { get; set; }

        [NotMapped]
        public int Contador { get; set; }

        //relacionamento
        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }
    }
}
