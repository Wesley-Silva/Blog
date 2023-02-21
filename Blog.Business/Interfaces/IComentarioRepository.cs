using Blog.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Business.Interfaces
{
    public interface IComentarioRepository : IRepository<Comentario>
    {
        Task<IEnumerable<Comentario>> ObterComentarios();

        Task<IEnumerable<Comentario>> ObterComentarioPorId(Guid id);
    }
}
