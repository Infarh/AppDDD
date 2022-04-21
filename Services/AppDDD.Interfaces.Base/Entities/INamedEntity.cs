using System.ComponentModel.DataAnnotations;

namespace AppDDD.Interfaces.Base.Entities;

public interface INamedEntity : IEntity
{
    [Required]
    string Name { get; set; }
}
