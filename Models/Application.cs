using System;
using System.Collections.Generic;

namespace WebApplication5.Models;

public partial class Application
{
    public int IdApplications { get; set; }

    public DateOnly? DateOfApplicationSubmission { get; set; }

    public string? ApplicationStatus { get; set; }

    public int? MebelId { get; set; }

    public int? ClientsId { get; set; }

    public int? StaffId { get; set; }

    public virtual Client? Clients { get; set; }

    public virtual Mebel? Mebel { get; set; }

    public virtual Staff? Staff { get; set; }
}
