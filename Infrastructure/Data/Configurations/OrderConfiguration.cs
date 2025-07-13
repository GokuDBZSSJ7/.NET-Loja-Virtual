using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> entity)
    {
        entity.ToTable("orders");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
              .HasColumnName("id");

        entity.Property(e => e.CustomerId)
              .HasColumnName("customer_id")
              .IsRequired();

        entity.Property(e => e.Total)
              .HasColumnName("total")
              .HasColumnType("decimal(10,2)")
              .IsRequired();

        entity.Property(e => e.Freight)
              .HasColumnName("freight")
              .HasColumnType("decimal(10,2)");

        entity.Property(e => e.Discount)
              .HasColumnName("discount")
              .HasColumnType("decimal(10,2)");

        entity.Property(e => e.Status)
              .HasColumnName("status")
              .HasConversion<string>()
              .IsRequired();

        entity.Property(e => e.CreatedAt)
              .HasColumnName("created_at")
              .HasColumnType("datetime")
              .IsRequired();

        entity.HasOne(e => e.Customer)
              .WithMany(c => c.Orders)
              .HasForeignKey(e => e.CustomerId);

        // Quando criar o OrderItem:
        // entity.HasMany(e => e.Items)
        //       .WithOne(i => i.Order)
        //       .HasForeignKey(i => i.OrderId);
    }
}
