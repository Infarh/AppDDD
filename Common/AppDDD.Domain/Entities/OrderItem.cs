using AppDDD.Domain.Base.Entities;

namespace AppDDD.Domain.Entities;

public class OrderItem : Entity
{
    public Product Product { get; set; }

    public int Quantity { get; set; }
}