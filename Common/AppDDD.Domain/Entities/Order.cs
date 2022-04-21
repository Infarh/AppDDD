using AppDDD.Domain.Base.Entities;

namespace AppDDD.Domain.Entities;

public class Order : Entity
{
    public DateTimeOffset Time { get; set; } = DateTimeOffset.Now;

    public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
}