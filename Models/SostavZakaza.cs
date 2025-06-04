using System;
using System.Collections.Generic;

namespace WebApplication5.Models;

public partial class SostavZakaza
{
    public int IdSostavZakaza { get; set; }

    public decimal? Price { get; set; }

    public int? MebelId { get; set; }

    public virtual Mebel? Mebel { get; set; }
}
