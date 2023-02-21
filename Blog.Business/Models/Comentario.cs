using System;

namespace Blog.Business.Models
{
    public class Comentario : Entity
    {        
        public string Corpo { get; set; }
        public DateTime Criado { get; set; }
        public Guid PostId { get; set; }
        public Guid UsuarioId { get; set; }
        public virtual Post Posts { get; set; }
    }
}
