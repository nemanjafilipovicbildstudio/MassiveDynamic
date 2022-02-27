using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassiveDynamic.Data.Models;

namespace MassiveDynamic.Data.Repositories
{
    public interface IRepository<TEntity, in TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(TKey id);
        IQueryable<TEntity> GetQueryable();
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);
    }
}
