using System;
using System.Collections.Generic;

namespace IndianBank.EntityModel;

public partial class LoanDivision
{
    public int LoanTypeId { get; set; }

    public string LoanTypeName { get; set; } = null!;

    public decimal? InterestRate { get; set; }

    public decimal? MaxAmount { get; set; }
}
