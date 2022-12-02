using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using InGame.Business.Concrete.DTO.Concrete;
using InGame.Business.Concrete.DTO.Concrete.Category;
using InGame.Business.Concrete.DTO.Concrete.Product;
using InGame.Business.Concrete.Enum;
using InGame.Business.Interface;
using InGame.Business.Tools;
using InGame.Business.Tools.SQLHelper;
using InGame.DataAccess.Concrete;
using InGame.Entities.Concrete;
using Microsoft.Data.SqlClient;

namespace InGame.Business.Concrete.Manager
{
    public class ProductManager:GenericManager<Product>,IProductService
    {
        public ProductManager(ApplicationDbContext context) : base(context)
        {
        }
        
        public async Task<ServiceResult> GetProductWithCategory(string whereClause)
        {
            ServiceResult serviceResult = new ServiceResult(ServiceResultType.Notknown);
            try
            {
                IEnumerable<ProductWithCategoryDto> productWithCategoryDtos;
                var sql = SqlHelper.GetProductWithCategory(whereClause);
                await using (var sqlConnection = new SqlConnection(GlobalConstraint.ConnectionString))
                {
                    productWithCategoryDtos = await sqlConnection.QueryAsync<ProductWithCategoryDto>(sql);
                }

                var list = productWithCategoryDtos.ToList();
                if (list.Any())
                {
                    serviceResult.Data = list.AsList();
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
