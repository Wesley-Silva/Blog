using Blog.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Business.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<IEnumerable<Categoria>> ObterCategorias();

        Task<Categoria> ObterCategoriaPorId(Guid id);
    }
}
