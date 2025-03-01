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
    public DbSet<UserProfileEntity> UserProfiles { get; set; }
    public DbSet<AddressTypeEntity> AddressTypes { get; set; }
    public DbSet<UserAddressEntity> UserAddresses { get; set; }
    public DbSet<CustomerAddressEntity> CustomerAddresses { get; set; }
    public DbSet<CityEntity> Cities { get; set; }
    public DbSet<PostalCodeEntity> PostalCodes { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<CustomerContactEntity> CustomerContacts { get; set; }
    public DbSet<OrderStatusEntity> OrderStatuses { get; set; }


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


        modelBuilder.Entity<UserProfileEntity>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId) 
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<UserAddressEntity>()
            .HasOne(x => x.City)
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);
        

        modelBuilder.Entity<UserAddressEntity>()
            .HasOne(x => x.PostalCode)
            .WithMany()
            .HasForeignKey(x => x.PostalCodeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserAddressEntity>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.userId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<CustomerAddressEntity>()
            .HasOne(x => x.City)
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CustomerAddressEntity>()
            .HasOne(x => x.PostalCode)
            .WithMany()
            .HasForeignKey(x => x.PostalCodeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CustomerAddressEntity>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
