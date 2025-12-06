using System;
using System.Collections.Generic;

namespace IndianBank.EntityModel;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? FromAccountId { get; set; }

    public int? ToAccountId { get; set; }

    public int? TransactionTypeId { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? Status { get; set; }

    public virtual AccountDetail? FromAccount { get; set; }

    public virtual AccountDetail? ToAccount { get; set; }

    public virtual TransactionType? TransactionType { get; set; }
}
