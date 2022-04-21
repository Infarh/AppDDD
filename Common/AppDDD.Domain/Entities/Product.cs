using AppDDD.Domain.Base.Entities;

namespace AppDDD.Domain.Entities;

public class Product : NamedEntity
{
    //[Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
}