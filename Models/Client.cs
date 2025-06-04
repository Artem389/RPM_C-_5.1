using System;
using System.Collections.Generic;

namespace WebApplication5.Models;

public partial class Client
{
    public int IdClients { get; set; }

    public string? Suname { get; set; }

    public string? Name { get; set; }

    public string? Fatherland { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int? AdressId { get; set; }

    public int? PassportId { get; set; }

    public virtual Adress? Adress { get; set; }

    public virtual ICollection<Application>? Applications { get; set; } = new List<Application>();

    public virtual Pasport? Passport { get; set; }
}
