﻿using Blog.Business.Models;
using System;
using System.Threading.Tasks;

namespace Blog.Business.Interfaces
{
    public interface ICategoriaService : IDisposable
    {
        Task Adicionar(Categoria categoria);

        Task Atualizar(Categoria categoria);

        Task Remover(Guid id);
    }
}
