using AppDDD.Domain.Base.Entities;

namespace AppDDD.Domain.Entities;

public class Employee : NamedEntity
{
    public string LastName { get; set; }

    public string Patronymic { get; set; }

    //[Column(TypeName = "decimal(18,2)")]
    public decimal Salary { get; set; }

    public Departament Departament { get; set; }
}