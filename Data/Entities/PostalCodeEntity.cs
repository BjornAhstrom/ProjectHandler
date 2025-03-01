using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class PostalCodeEntity
{
    [Key]
    public int Id { get; set; }
    public string PostalCode { get; set; } = null!;
}