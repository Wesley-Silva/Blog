using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    public class ComentarioRepository : Repository<Comentario>, IComentarioRepository
    {
        public ComentarioRepository(BlogDbContext context, IHttpContextAccessor accessor)
            : base(context)
        {
        }

        public async Task<IEnumerable<Comentario>> ObterComentarios()
        {
            return await Db.Comentarios.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Comentario>> ObterComentarioPorId(Guid id)
        {
            return await Db.Comentarios.Where(c => c.PostId == id).ToListAsync();
        }  
    }
}
