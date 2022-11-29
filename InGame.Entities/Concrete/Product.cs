using InGame.Entities.Interface;

namespace InGame.Entities.Concrete
{
    public class Product: BaseEntity, IEntity
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
