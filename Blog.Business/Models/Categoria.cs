using System.Collections.Generic;

namespace Blog.Business.Models
{
    public class Categoria : Entity
    {
        public string Nome { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
