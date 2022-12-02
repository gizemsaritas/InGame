using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InGame.Business.Concrete.DTO.Concrete;
using InGame.Entities.Concrete;

namespace InGame.Business.Interface
{
    public interface IProductService:IGenericService<Product>
    {
        Task<ServiceResult> GetProductWithCategory(string whereClause);
    }
}
