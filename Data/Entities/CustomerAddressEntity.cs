using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CustomerAddressEntity
{
    [Key]
    public int customerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;

    public string Address { get; set; } = null!;


    public int CityId { get; set; }
    public CityEntity City { get; set; } = null!;
    public int PostalCodeId { get; set; }
    public PostalCodeEntity PostalCode { get; set; } = null!;
    public int AddressTypeId { get; set; }
    public AddressTypeEntity AddressType { get; set; } = null!;

    
}
