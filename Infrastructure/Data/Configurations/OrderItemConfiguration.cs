using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> entity)
    {
        entity.ToTable("order_items");

        entity.HasKey(oi => new { oi.OrderId, oi.ProductId });

        entity.Property(oi => oi.OrderId)
            .HasColumnName("order_id")
            .IsRequired();

        entity.Property(oi => oi.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

        entity.Property(oi => oi.Quantity)
            .HasColumnName("quantity")
            .IsRequired();

        entity.Property(oi => oi.UnitPrice)
            .HasColumnName("unit_price")
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        entity.HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId);

        entity.HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);
    }
}
