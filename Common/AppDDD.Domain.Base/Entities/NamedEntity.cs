using AppDDD.Interfaces.Base.Entities;

namespace AppDDD.Domain.Base.Entities;

public abstract class NamedEntity : Entity, INamedEntity
{
    public string Name { get; set; }
}
