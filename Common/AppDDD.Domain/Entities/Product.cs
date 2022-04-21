using AppDDD.Domain.Base.Entities;

namespace AppDDD.Domain.Entities;

public class Product : NamedEntity
{
    public decimal Price { get; set; }
}