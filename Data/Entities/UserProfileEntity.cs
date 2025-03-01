using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class UserProfileEntity
{
    [Key]
    public int Id { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfileImage { get; set; }
    public DateTime? Created { get; set; } = DateTime.Now;
    public DateTime? Updated { get; set; } = DateTime.Now;

    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;
    public int UserAddressId { get; set; }
    public UserAddressEntity UserAddress { get; set; } = null!;
}
