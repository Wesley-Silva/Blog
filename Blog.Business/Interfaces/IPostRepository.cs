using Blog.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Business.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> ObterPosts();
        IEnumerable<Post> ObterQtdeComentariosPorPost();

        IEnumerable<Post> ObterPostMaisComentados();
    }
}
