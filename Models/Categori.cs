using System;
using System.Collections.Generic;

namespace WebApplication5.Models;

public partial class Categori
{
    public int IdCategori { get; set; }

    public string? NameCategori { get; set; }

    public virtual ICollection<Mebel>? Mebels { get; set; } = new List<Mebel>();
}
