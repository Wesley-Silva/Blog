using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(BlogDbContext context)
            : base(context)
        {

        }        

        public async Task<IEnumerable<Categoria>> ObterCategorias()
        {
            return await Db.Categorias.AsNoTracking().ToListAsync();
        }

        public async Task<Categoria> ObterCategoriaPorId(Guid id)
        {
            return await Db.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
