using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Data.Context;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {        
        private IConfiguration _config;
        public PostRepository(BlogDbContext context, IHttpContextAccessor accessor, IConfiguration configuration)
            : base(context)
        {            
            _config = configuration;
        }

        public async Task<IEnumerable<Post>> ObterPosts()
        {
            return await Db.Posts.AsNoTracking()
                .Include(c => c.Categoria)
                .Include(p => p.Comentarios)
                .OrderBy(p => p.Titulo).ToListAsync();
        }

        public IEnumerable<Post> ObterQtdeComentariosPorPost()
        {
            var query = "select count(posts.Id), posts.Titulo as posts, comentarios.PostId " +
                      "  from posts " +
                       " INNER JOIN comentarios on posts.id = comentarios.PostId " +
                       " group by posts.Titulo, comentarios.PostId ";
            IEnumerable<Post> post;
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                conexao.Open();
                post = conexao.Query<Post>(query);
                conexao.Close();
            }
            return post;
        }

        public IEnumerable<Post> ObterPostMaisComentados()
        {
            var query = "select top(5) count(posts.Id) as Contador, posts.Titulo, comentarios.PostId " + 
                        "from posts INNER JOIN comentarios on posts.id = comentarios.PostId " +
                        "group by posts.Titulo, comentarios.PostId " +
                        "order by posts.Titulo";
            IEnumerable<Post> post;
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                conexao.Open();
                post = conexao.Query<Post>(query);
                conexao.Close();
            }
            return post;
        }
    }
}
