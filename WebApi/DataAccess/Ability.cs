using System;
using System.Collections.Generic;

namespace WebApi.DataAccess;

public partial class Ability
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Person> People { get; } = new List<Person>();
}
