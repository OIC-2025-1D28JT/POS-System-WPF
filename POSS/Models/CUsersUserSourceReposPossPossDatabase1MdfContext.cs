using Microsoft.EntityFrameworkCore;
using POSS;

namespace POSS.Models;

public partial class CUsersUserSourceReposPossPossDatabase1MdfContext : DbContext
{
    public CUsersUserSourceReposPossPossDatabase1MdfContext() { }
    public CUsersUserSourceReposPossPossDatabase1MdfContext(DbContextOptions<CUsersUserSourceReposPossPossDatabase1MdfContext> options) : base(options) { }
    public virtual DbSet<Table> Tables { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(DatabaseConfig.ConnectionString);
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Table__3214EC079BDB7C3C");
            entity.ToTable("Table");
            entity.Property(e => e.Barcode).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
        });
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}