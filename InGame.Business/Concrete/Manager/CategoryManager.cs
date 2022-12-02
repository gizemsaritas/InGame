using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using InGame.Business.Concrete.DTO.Concrete;
using InGame.Business.Concrete.DTO.Concrete.Category;
using InGame.Business.Concrete.Enum;
using InGame.Business.Interface;
using InGame.Business.Tools;
using InGame.Business.Tools.SQLHelper;
using InGame.DataAccess.Concrete;
using InGame.Entities.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InGame.Business.Concrete.Manager
{
    public class CategoryManager : GenericManager<Category>, ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryManager(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ServiceResult> GetAllWithSubCategoryAsync(int? parentId)
        {
            ServiceResult serviceResult = new ServiceResult(ServiceResultType.Notknown);
            try
            {
                List<Category> result = new List<Category>();
                var list = await GetCategory(parentId, result);
                if (list != null)
                {
                    serviceResult.Data = list;
                    serviceResult.ServiceResultType = ServiceResultType.Success;
                }
            }
            catch (Exception e)
            {
                serviceResult.ServiceResultType = ServiceResultType.Error;
            }

            return serviceResult;
        }
        private async Task<ServiceResult> GetCategory(int? parentId, List<Category> result)
        {
            ServiceResult serviceResult = new ServiceResult(ServiceResultType.Notknown);
            try
            {
                var categories = await _context.Categories.Where(I => I.ParentCategoryId == parentId).ToListAsync();
                if (categories.Any())
                {
                    categories.ForEach(async m =>
                    {
                        if (m.SubCategories == null)
                            m.SubCategories = new List<Category>();
                        await GetCategory(m.Id, m.SubCategories);
                        if (!result.Contains(m))
                            result.Add(m);
                    });
                    serviceResult.Data = categories;
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
        public async Task<ServiceResult> GetCategoryWithSql()
        {
            ServiceResult serviceResult = new ServiceResult(ServiceResultType.Notknown);
            try
            {
                IEnumerable<CategoryWithSqlDto> categoryWithSql;
                var sql = SqlHelper.GetCategoryWithParentId();
                await using (var sqlConnection = new SqlConnection(GlobalConstraint.ConnectionString))
                {
                    categoryWithSql = await sqlConnection.QueryAsync<CategoryWithSqlDto>(sql);
                }

                var categoryWithSqlDtos = categoryWithSql.ToList();
                if (categoryWithSqlDtos.Any())
                {
                    serviceResult.Data = categoryWithSqlDtos.AsList();
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
    }
}
