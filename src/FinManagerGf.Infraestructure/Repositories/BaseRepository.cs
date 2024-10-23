using FinManagerGf.Application.Repositories;
using FinManagerGf.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinManagerGf.Infraestructure.Repositories
{
    public class BaseRepository<TEntity>(CoreDbContext context) : IBaseRepository<TEntity> where TEntity : IdentityUser
    {
        protected readonly CoreDbContext context = context;
        protected readonly DbSet<TEntity> dbSet = context.Set<TEntity>();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await dbSet.ToListAsync();

        //TODO: Transformar em exception personalizada
        public virtual async Task<TEntity> GetAsync(Guid id) => await dbSet.FindAsync(id) ?? throw new Exception("Not Found");

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            TEntity entity = await GetAsync(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) =>
            await dbSet.Where(predicate).ToListAsync();

        public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate) =>
            await dbSet
                .Where(predicate)
                .FirstOrDefaultAsync();

        public virtual async Task<bool> HasAny(Expression<Func<TEntity, bool>> predicate) =>
            await dbSet.Where(predicate).AnyAsync();

    }
}
