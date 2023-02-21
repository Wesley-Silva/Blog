using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    // so pode ser herdada(abstract)nao pode ser instanciada
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        // protected aqui pq tanto o repository quantoquem herdar de pode utilizar o Db
        protected readonly BlogDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(BlogDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            var entity = new TEntity { Id = id };
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            // retorna as resposta da leitura do banco sem Tracking isso garante + perform
            // await espera esse resultado acontecer -> DbSet.AsNoTracking().Where(predicate).ToListAsync();
            // se nao espera(await) ele retorna uma task e eu nao quero task eu quero resultado do banco
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            // (Db?) se existir faça o dispose senao nao faça
            Db?.Dispose();
        }
    }
}
