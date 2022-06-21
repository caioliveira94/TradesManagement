using TradesManagement.Data.Contexts;
using TradesManagement.Domain.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TradesManagement.Data.Repositories
{
    public class BaseRepository<TEntity, TId> : IDisposable, IBaseRepository<TEntity, TId> where TEntity : class
    {
        #region Fields
        protected readonly ApplicationDbContext applicationDbContext;
        #endregion

        #region Constructors
        public BaseRepository(ApplicationDbContext context) =>
            applicationDbContext = context;
        #endregion

        #region IRepositoryBase
        public async Task AddAsync(TEntity entity)
        {
            await applicationDbContext.Set<TEntity>().AddAsync(entity);
            Save();
        }

        public virtual async Task<TEntity> GetByIdAsync(TId id)
        {
            return await applicationDbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.applicationDbContext.Set<TEntity>();

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await applicationDbContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            applicationDbContext.Set<TEntity>().Update(entity);
            Save();
        }

        public async Task DeleteAsync(TId id)
        {
            var lookup = await applicationDbContext.FindAsync<TEntity>(id);
            applicationDbContext.Remove<TEntity>(lookup);
            Save();
        }
        #endregion

        #region Private Methods
        protected void Save() =>
            applicationDbContext.SaveChanges();
        #endregion

        #region IDisposable
        private bool disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing) applicationDbContext.Dispose();
            disposed = true;
        }
        #endregion
    }
}
