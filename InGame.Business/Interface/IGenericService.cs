using InGame.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using InGame.Business.Concrete.DTO.Concrete;

namespace InGame.Business.Interface
{
    public interface IGenericService<TEntity> where TEntity:class,IEntity,new()
    {
        Task<ServiceResult> GetAllAsync();
        Task<ServiceResult> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<ServiceResult> GetAsync(Expression<Func<TEntity, bool>> filter);
        Task<ServiceResult> FindByIdAsync(int id);
        Task<ServiceResult> AddAsync(TEntity entity);
        Task<ServiceResult> UpdateAsync(TEntity entity);
        Task<ServiceResult> RemoveAsync(TEntity entity);
    }
}
