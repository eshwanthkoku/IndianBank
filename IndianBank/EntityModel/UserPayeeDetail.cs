using System;
using System.Collections.Generic;

namespace IndianBank.EntityModel;

public partial class UserPayeeDetail
{
    public int PayeeId { get; set; }

    public int? UserId { get; set; }

    public string? PayeeName { get; set; }

    public string? PayeeAccountNo { get; set; }

    public string? PayeeBankName { get; set; }

    public string? Ifsccode { get; set; }

    public virtual User? User { get; set; }
}
