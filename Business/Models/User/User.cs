namespace Business.Models.User;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public UserRole Role { get; set; } = null!;
}

public class UserRole
{
    public int RoleId { get; set; }
    public string RoleName { get; set; } = null!;
}