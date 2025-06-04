using System;
using System.Collections.Generic;

namespace WebApplication5.Models;

public partial class Staff
{
    public int IdStaff { get; set; }

    public string? Suname { get; set; }

    public string? Name { get; set; }

    public string? Fatherland { get; set; }

    public int? PassportId { get; set; }

    public int? PositionsId { get; set; }

    public virtual ICollection<Application>? Applications { get; set; } = new List<Application>();

    public virtual Pasport? Passport { get; set; }

    public virtual Position? Positions { get; set; }
}
