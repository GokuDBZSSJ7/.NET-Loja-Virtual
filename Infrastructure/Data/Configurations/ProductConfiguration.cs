using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.ToTable("products");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
              .HasColumnName("id");

        entity.Property(e => e.Name)
              .HasColumnName("name")
              .IsRequired()
              .HasMaxLength(100);

        entity.Property(e => e.Description)
              .HasColumnName("description")
              .HasColumnType("text");

        entity.Property(e => e.Price)
              .HasColumnName("price")
              .HasColumnType("decimal(10,2)");

        entity.Property(e => e.Stock)
              .HasColumnName("stock")
              .IsRequired();

        entity.Property(e => e.CategoryId)
              .HasColumnName("category_id")
              .IsRequired();
    }
}
