using TradesManagement.Domain.Interfaces.DataAccess;
using TradesManagement.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TradesManagement.ApplicationService
{
    public class BaseService<TEntity, TId> : IBaseService<TEntity, TId> where TEntity : class
    {
        #region Private Members
        private readonly IBaseRepository<TEntity, TId> repositoryBase;
        #endregion

        #region Constructors
        public BaseService(IBaseRepository<TEntity, TId> repositoryBase) => this.repositoryBase = repositoryBase;
        #endregion

        #region IServiceBase
        public virtual async Task AddAsync(TEntity entity)
        {
            await this.repositoryBase.AddAsync(entity);
        }

        public virtual async Task DeleteAsync(TId id)
        {
            await this.repositoryBase.DeleteAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.repositoryBase.GetAllAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> filter = null,
                                                Func<IQueryable<TEntity>,
                                                IOrderedQueryable<TEntity>> orderBy = null,
                                                string includeProperties = "")
        {
            return await this.repositoryBase.GetByFilterAsync(filter, orderBy, includeProperties);
        }

        public virtual async Task<TEntity> GetByIdAsync(TId id)
        {
            return await this.repositoryBase.GetByIdAsync(id);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await this.repositoryBase.UpdateAsync(entity);
        }
        #endregion
    }
}
