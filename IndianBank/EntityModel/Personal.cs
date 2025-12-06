using System;
using System.Collections.Generic;

namespace IndianBank.EntityModel;

public partial class Personal
{
    public int PersonalId { get; set; }

    public int? UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Pan { get; set; }

    public string? Aadhar { get; set; }

    public virtual User? User { get; set; }
}
