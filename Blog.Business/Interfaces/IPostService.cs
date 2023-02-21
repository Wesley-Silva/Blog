using Blog.Business.Models;
using System;
using System.Threading.Tasks;

namespace Blog.Business.Interfaces
{
    public interface IPostService : IDisposable
    {
        Task Adicionar(Post post);
        Task Atualizar(Post post);
        Task Remover(Guid id);
    }
}
