using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class UserAddressEntity
{
    [Key]
    public int userId { get; set; }
    public UserEntity User { get; set; } = null!;

    public int CityId { get; set; }
    public CityEntity City { get; set; } = null!;
    public int PostalCodeId { get; set; }
    public PostalCodeEntity PostalCode { get; set; } = null!;
    public int AddressTypeId { get; set; }
    public AddressTypeEntity AddressType { get; set; } = null!;
}
