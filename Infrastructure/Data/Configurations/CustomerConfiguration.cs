using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.ToTable("customers");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
              .HasColumnName("id");

        entity.Property(e => e.Name)
              .HasColumnName("name")
              .IsRequired()
              .HasMaxLength(100);

        entity.Property(e => e.Email)
              .HasColumnName("email")
              .IsRequired()
              .HasMaxLength(100);

        entity.Property(e => e.PasswordHash)
              .HasColumnName("password_hash")
              .IsRequired()
              .HasMaxLength(255);

        entity.Property(e => e.Phone)
              .HasColumnName("phone")
              .HasMaxLength(20);

        entity.Property(e => e.ZipCode)
              .HasColumnName("zip_code")
              .HasMaxLength(9);

        entity.Property(e => e.Address)
              .HasColumnName("address")
              .HasMaxLength(255);

        entity.Property(e => e.Type)
              .HasColumnName("type")
              .HasConversion<string>()
              .IsRequired();
    }
}
