using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Entities.Security;
using Entities;

namespace Entities.Data
{
    public class ApplicationDbContext 
    : IdentityDbContext<AppUser, AppRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<ProductLimit> ProductLimits {get; set;}
    public DbSet<AppEmployee> AppEmployees {get; set; }
    public DbSet<AppJob> AppJobs {get; set;}
    public DbSet<InventoryTransactions> InventoryTransactions {get; set; }
    public DbSet<AppProduct> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RolePermission>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        modelBuilder.Entity<Permission>().ToTable("Permission"); 
        modelBuilder.Entity<ProductLimit>().ToTable("ProductLimit");
        modelBuilder.Entity<AppEmployee>().ToTable("AppEmployee"); 
        modelBuilder.Entity<AppJob>().ToTable("AppJob"); 
        modelBuilder.Entity<InventoryTransactions>().ToTable("InventoryTransactions");    
        modelBuilder.Entity<AppProduct>().ToTable("Products");
    }
}
}