using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> entity)
    {
        entity.ToTable("coupons");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Code)
              .HasColumnName("code")
              .HasMaxLength(50)
              .IsRequired();

        entity.Property(e => e.Value)
              .HasColumnName("value")
              .HasColumnType("decimal(10,2)")
              .IsRequired();

        entity.Property(e => e.IsPercentage)
              .HasColumnName("is_percentage")
              .IsRequired();

        entity.Property(e => e.ExpirationDate)
              .HasColumnName("expiration_date")
              .IsRequired();

        entity.Property(e => e.MaxUsages)
              .HasColumnName("max_usages");

        entity.Property(e => e.TimesUsed)
              .HasColumnName("times_used")
              .IsRequired();

        entity.Property(e => e.MinimumOrderAmount)
              .HasColumnName("minimum_order_amount")
              .HasColumnType("decimal(10,2)");
    }
}
