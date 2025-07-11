using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable("products");

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Price).HasColumnType("decimal(10,2)");
            entity.Property(e => e.Stock).IsRequired();
            entity.Property(e => e.CategoryId).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}
