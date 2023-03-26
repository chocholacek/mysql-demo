using System;
using System.Collections.Generic;

namespace WebApi.DataAccess;

public partial class Address
{
    public int Id { get; set; }

    public string Street { get; set; } = null!;

    public int? StreetNumer { get; set; }

    public string City { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public virtual ICollection<Person> People { get; } = new List<Person>();
}
