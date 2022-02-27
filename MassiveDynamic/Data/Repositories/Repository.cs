using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MassiveDynamic.Data.Models;
using System.Linq;
using MassiveDynamic.Data.ModelConfigs;

namespace MassiveDynamic.Data.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly ApplicationDbContext context;
        private DbSet<TEntity> entities;

        public Repository(ApplicationDbContext context) {
            this.context = context;
            entities = context.Set<TEntity>();
        }
       
        public Task<TEntity> GetAsync(TKey id)
        {
            return entities.SingleOrDefaultAsync(s => s.Id.Equals(id));
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return entities;
        }

        public async Task InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if(typeof(TEntity) == typeof(Company))
            {
                var length = CompanyModelConfig.MaxLengthId;
                do
                {
                    entity.Id = (TKey)(object) GenerateId(length);
                } while (entities.SingleOrDefault(x => x.Id.Equals(entity.Id)) != null);
            }
            await entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TKey id)
        {
            var entity = await GetAsync(id);
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _ = await context.SaveChangesAsync();
        }

        private string GenerateId(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
