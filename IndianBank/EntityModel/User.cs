using System;
using System.Collections.Generic;

namespace IndianBank.EntityModel;

public partial class User
{
    public int UserId { get; set; }

    public int? DepartmentId { get; set; }

    public int? RoleId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public DateOnly? DateJoined { get; set; }

    public virtual ICollection<AccountDetail> AccountDetails { get; set; } = new List<AccountDetail>();

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<BranchDetail> BranchDetails { get; set; } = new List<BranchDetail>();

    public virtual Department? Department { get; set; }

    public virtual Personal? Personal { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<UserPayeeDetail> UserPayeeDetails { get; set; } = new List<UserPayeeDetail>();
}
