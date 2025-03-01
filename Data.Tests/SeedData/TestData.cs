using Data.Entities;

namespace Data.Tests.SeedData;

public static class TestData
{

    public static readonly StatusTypeEntity[] StatusesEntities = [

        new StatusTypeEntity { Id = 1, StatusType = "Ej påbörjad"},
        new StatusTypeEntity { Id = 2, StatusType = "Påbörjad",},
        new StatusTypeEntity { Id = 3, StatusType = "Avslutad"},
    ];

    public static readonly ProjectEntity[] ProjectEntities = [

        new ProjectEntity {
            ProjectName = "Databasteknik", 
            Description = "En kurs", 
            StartDate = new DateTime(2025, 04, 01), 
            EndDate = new DateTime(2025, 09, 28), 
            StatusTypeId = 1,
            CustomerId = 2, 
            ProjectManagerId = 1
        }
    ];

    public static readonly ProjectEntity[] NewProjectEntities = [

        new ProjectEntity {
            ProjectName = "Programmering med ASP.NET 1",
            Description = "En annan kurs",
            StartDate = new DateTime(2025, 04, 01),
            EndDate = new DateTime(2025, 09, 28),
            StatusTypeId = 1,
            CustomerId = 2,
            ProjectManagerId = 1
        }
    ];


    public static readonly RoleEntity[] RolesEntities = [
        new RoleEntity {Id = 1, RoleName = "Projektledare"},
        new RoleEntity {Id = 2, RoleName = "Utvecklare"}
    ];

    public static readonly UserRoleEntity[] UserRoleEntities = [
       new UserRoleEntity { UserId = 1, RoleId = 2 },
        new UserRoleEntity { UserId = 2, RoleId = 2 },
    ];

    public static readonly UserEntity[] UsersEntities = [
        new UserEntity { 
            Id = 1,
            FirstName = "Björn",
            LastName = "Åhström",
            Email = "bjorn.ahstrom@icloud.com",
            RoleId = 1,
            Role = RolesEntities[0],
            UserRoles = new List<UserRoleEntity> { UserRoleEntities[0] }
        },
        new UserEntity { Id = 2, 
            FirstName = "Desirèe", 
            LastName = "Åhström", 
            Email = "desiree.ahstrom@icloud.com", 
            RoleId = 2, Role = RolesEntities[1], 
            UserRoles = new List<UserRoleEntity> { UserRoleEntities[1] }
        }
    ];

    public static readonly UserEntity[] UpdatedUsersEntities = [
        new UserEntity { Id = 1, FirstName = "Sebastian", LastName = "Åhström", Email = "sebatian.ahstrom@icloud.com", RoleId = 1, Role = RolesEntities[0], UserRoles = new List<UserRoleEntity> { UserRoleEntities[0] }},
        new UserEntity { Id = 2, FirstName = "Michelle", LastName = "Åhström", Email = "michelle.ahstrom@icloud.com", RoleId = 2, Role = RolesEntities[1], UserRoles = new List<UserRoleEntity> { UserRoleEntities[1] }}
    ];

    public static readonly CustomerEntity[] CustomersEntities = [
        new CustomerEntity {Id = 1, CustomerName = "Sebastian Åhström"},
        new CustomerEntity {Id = 2, CustomerName = "Michelle Åhström"}
    ];

}
