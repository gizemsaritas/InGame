using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InGame.Entities.Interface;

namespace InGame.Entities.Concrete
{
    public class Category:BaseEntity,IEntity
    {
        public string Name { get; set; }
        public int ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public List<Category> SubCategories { get; set; }
        public List<Product> Products { get; set; }
    }
}
