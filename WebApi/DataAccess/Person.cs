using System;
using System.Collections.Generic;

namespace WebApi.DataAccess;

public partial class Person
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public int Age { get; set; }

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Ability> Abilities { get; } = new List<Ability>();
}
