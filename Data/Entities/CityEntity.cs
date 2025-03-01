using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CityEntity
{
    [Key]
    public int Id { get; set; }
    public string CityName { get; set; } = null!;

}
