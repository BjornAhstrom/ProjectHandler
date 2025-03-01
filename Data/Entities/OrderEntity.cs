using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class OrderEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }
    public decimal? Total { get; set; }


    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;
    public int OrderStatusId { get; set; }
    public OrderStatusEntity OrderStatus { get; set; } = null!;

}
