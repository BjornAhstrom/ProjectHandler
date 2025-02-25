using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DataContext()
    {
        
    }

    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<StatusTypeEntity> StatusTypes { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserRoleEntity> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>()
            .Property(x => x.Id)
            .UseIdentityColumn(100, 1);

        modelBuilder.Entity<UserRoleEntity>().HasKey(x => new {x.UserId, x.RoleId});

        modelBuilder.Entity<UserRoleEntity>()
            .HasOne(x => x.User)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserRoleEntity>()
            .HasOne(x => x.Role)
            .WithMany(x => x.UsersRoles)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
