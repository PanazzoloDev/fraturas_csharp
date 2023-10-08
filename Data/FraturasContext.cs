using fraturas_csharp.Entities;
using Microsoft.EntityFrameworkCore;

namespace fraturas_csharp.Data;

public partial class FraturasContext : DbContext
{
    public FraturasContext(DbContextOptions<FraturasContext> options)
        : base(options)
    {
    }
    public virtual DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(e => e.Id).HasName("id");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(x => x.Name).HasColumnName("name");
            entity.Property(x => x.Password).HasColumnName("password");
        });
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
