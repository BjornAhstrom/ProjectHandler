using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CustomerContactEntity
{
    [Key]
    public int Id { get; set; }
    public string ContactName { get; set; } = null!;
    public string Email = null!;
    public string Phonenumber = null!;

    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;
}