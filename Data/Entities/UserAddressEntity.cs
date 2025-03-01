using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class UserAddressEntity
{
    [Key]
    public int useId {  get; set; }
    public UserEntity User { get; set; } = null!;

    public int AddressTypeId { get; set; }
    public AddressTypeEntity AddressType { get; set; } = null!;

    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
}
