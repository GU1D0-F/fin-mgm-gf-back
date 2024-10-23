using System.Linq.Expressions;

namespace FinManagerGf.Application.Repositories
{
    public interface IBaseRepository<BaseEntity>
    {
        Task<IEnumerable<BaseEntity>> GetAllAsync();
        Task<BaseEntity> GetAsync(Guid id);
        Task<BaseEntity?> GetAsync(Expression<Func<BaseEntity, bool>> predicate);
        Task<BaseEntity> CreateAsync(BaseEntity entity);
        Task<BaseEntity> UpdateAsync(BaseEntity entity);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<BaseEntity>> FindAsync(Expression<Func<BaseEntity, bool>> predicate);
        Task<bool> HasAny(Expression<Func<BaseEntity, bool>> predicate);
    }
}
