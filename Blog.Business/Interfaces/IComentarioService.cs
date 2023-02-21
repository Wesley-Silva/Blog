using Blog.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Interfaces
{
    public interface IComentarioService : IDisposable
    {
        Task Adicionar(Comentario comentario);
    }
}
