using System;
using System.Collections.Generic;

namespace WebApplication5.Models;

public partial class Position
{
    public int IdPositions { get; set; }

    public string? Positions { get; set; }

    public decimal? Salary { get; set; }

    public virtual ICollection<Staff>? Staff { get; set; } = new List<Staff>();
}
