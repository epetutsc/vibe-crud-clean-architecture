using Microsoft.EntityFrameworkCore;
using VibeCrud.Domain.Entities;

namespace VibeCrud.Infrastructure.SqlServer.Data;

public class VibeCrudDbContext : DbContext
{
    public VibeCrudDbContext(DbContextOptions<VibeCrudDbContext> options) : base(options)
    {
    }

    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Street).IsRequired().HasMaxLength(200);
            entity.Property(e => e.HouseNumber).IsRequired().HasMaxLength(10);
            entity.Property(e => e.ZipCode).IsRequired().HasMaxLength(10);
            entity.Property(e => e.City).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Country).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            
            entity.HasIndex(e => e.Email).IsUnique().HasFilter("[Email] IS NOT NULL");
            entity.HasIndex(e => new { e.FirstName, e.LastName });
            entity.HasIndex(e => e.City);
            entity.HasIndex(e => e.ZipCode);
            entity.HasIndex(e => e.IsDeleted);
        });
    }
}