using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using InGame.Business.Concrete.DTO.Concrete;
using InGame.Business.Concrete.Enum;
using InGame.Business.Interface;
using InGame.DataAccess.Concrete;
using InGame.Entities.Interface;
using Microsoft.EntityFrameworkCore;

namespace InGame.Business.Concrete.Manager
{

    public class GenericManager<TEntity> : IGenericService<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly ApplicationDbContext _context;
        public GenericManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResult> AddAsync(TEntity entity)
        {
            ServiceResult serviceResult = new ServiceResult(ServiceResultType.Notknown);
            try
            {
                await _context.AddAsync(entity);
                var result = await _context.SaveChangesAsync();
                serviceResult.ServiceResultType = ServiceResultType.Success;
            }
            catch (Exception e)
            {
                serviceResult.ServiceResultType = ServiceResultType.Error;
            }

            return serviceResult;

        }
        public async Task<ServiceResult> FindByIdAsync(int id)
        {
            ServiceResult serviceResult = new ServiceResult(ServiceResultType.Notknown);
            try
            {
                var result = await _context.FindAsync<TEntity>(id);
                if (result != null)
                {
                    serviceResult.Data = result;
                    serviceResult.ServiceResultType = ServiceResultType.Success;
                }
                else
                {
                    serviceResult.ServiceResultType = ServiceResultType.Error;
                }

            }
            catch (Exception e)
            {
                serviceResult.ServiceResultType = ServiceResultType.Error;
            }

            return serviceResult;
        }
        public async Task<ServiceResult> GetAllAsync()
        {
            ServiceResult serviceResult = new ServiceResult(ServiceResultType.Notknown);
            try
            {
                var result = await _context.Set<TEntity>().ToListAsync();
                if (result.Any())
                {
                    serviceResult.Data = result;
                    serviceResult.ServiceResultType = ServiceResultType.Success;
                }
                else
                {
                    serviceResult.ServiceResultType = ServiceResultType.Error;
                }
            }
            catch (Exception e)
            {
                serviceResult.ServiceResultType = ServiceResultType.Error;
            }

            return serviceResult;

        }
        public async Task<ServiceResult> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            ServiceResult serviceResult = new ServiceResult(ServiceResultType.Notknown);
            try
            {
                var result = await _context.Set<TEntity>().Where(filter).ToListAsync();
                if (result.Any())
                {
                    serviceResult.Data = result;
                    serviceResult.ServiceResultType = ServiceResultType.Success;
                }
                else
                {
                    serviceResult.ServiceResultType = ServiceResultType.Error;
                }
            }
            catch (Exception e)
            {
                serviceResult.ServiceResultType = ServiceResultType.Error;
            }

            return serviceResult;
        }
        public async Task<ServiceResult> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            ServiceResult serviceResult = new ServiceResult(ServiceResultType.Notknown);
            try
            {
                var result = await _context.Set<TEntity>().FirstOrDefaultAsync(filter);
                if (result != null)
                {
                    serviceResult.Data = result;
                    serviceResult.ServiceResultType = ServiceResultType.Success;
                }
                else
                {
                    serviceResult.ServiceResultType = ServiceResultType.Error;
                }
            }
            catch (Exception e)
            {
                serviceResult.ServiceResultType = ServiceResultType.Error;
            }

            return serviceResult;

        }
        public async Task<ServiceResult> RemoveAsync(TEntity entity)
        {
            ServiceResult serviceResult = new ServiceResult(ServiceResultType.Notknown);
            try
            {
                _context.Remove(entity);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    serviceResult.ServiceResultType = ServiceResultType.Success;
                }
                else serviceResult.ServiceResultType = ServiceResultType.Error;
            }
            catch (Exception e)
            {
                serviceResult.ServiceResultType = ServiceResultType.Error;
            }

            return serviceResult;
        }
        public async Task<ServiceResult> UpdateAsync(TEntity entity)
        {
            ServiceResult serviceResult = new ServiceResult(ServiceResultType.Notknown);
            try
            {
                _context.Update(entity);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    serviceResult.ServiceResultType = ServiceResultType.Success;
                }
                else serviceResult.ServiceResultType = ServiceResultType.Error;
            }
            catch (Exception e)
            {
                serviceResult.ServiceResultType = ServiceResultType.Error;
            }

            return serviceResult;



        }
    }
}
