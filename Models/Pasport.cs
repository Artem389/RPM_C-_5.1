using System;
using System.Collections.Generic;

namespace WebApplication5.Models;

public partial class Pasport
{
    public int IdPasports { get; set; }

    public short? Serial { get; set; }

    public int? Number { get; set; }

    public DateOnly? DateOfIssue { get; set; }

    public virtual ICollection<Client>? Clients { get; set; } = new List<Client>();

    public virtual ICollection<Staff>? Staff { get; set; } = new List<Staff>();
}
