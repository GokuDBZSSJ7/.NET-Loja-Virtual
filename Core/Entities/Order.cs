using Core.Enums;

namespace Core.Entities;

public class Order : BaseEntity
{
    public decimal Total { get; set; }
    public decimal Freight { get; set; }
    public decimal Discount { get; set; }
    public OrderStatus Status { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();


}