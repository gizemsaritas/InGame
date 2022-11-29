using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InGame.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InGame.DataAccess.EntityTypeConfiguration
{
    public class ProductTypeConfiguration:IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product), nameof(Product));
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Name).HasMaxLength(100).IsRequired();
            builder.Property(I => I.Price).IsRequired();
            builder.Property(I => I.ImageUrl).HasMaxLength(300);
            builder.Property(I => I.Description).HasColumnType("ntext");
        }
    }
}
