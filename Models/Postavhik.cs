using System;
using System.Collections.Generic;

namespace WebApplication5.Models;

public partial class Postavhik
{
    public int IdPostavchik { get; set; }

    public string? Name { get; set; }

    public string? Contact { get; set; }

    public virtual ICollection<Postavki>? Postavkis { get; set; } = new List<Postavki>();
}
