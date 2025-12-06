using System;
using System.Collections.Generic;

namespace IndianBank.EntityModel;

public partial class AccountType
{
    public int AccountTypeId { get; set; }

    public string AccountTypeName { get; set; } = null!;

    public virtual ICollection<AccountDetail> AccountDetails { get; set; } = new List<AccountDetail>();
}
