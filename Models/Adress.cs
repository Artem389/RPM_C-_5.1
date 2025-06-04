using System;
using System.Collections.Generic;

namespace WebApplication5.Models;

public partial class Adress
{
    public int IdAdress { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Client>? Clients { get; set; } = new List<Client>();
}
