using System;
using System.Collections.Generic;

namespace IndianBank.EntityModel;

public partial class BranchDetail
{
    public int BranchId { get; set; }

    public string? BranchName { get; set; }

    public string? Ifsccode { get; set; }

    public int? ManagerId { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<AccountDetail> AccountDetails { get; set; } = new List<AccountDetail>();

    public virtual User? Manager { get; set; }
}
