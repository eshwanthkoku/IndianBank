using System;
using System.Collections.Generic;

namespace IndianBank.EntityModel;

public partial class AccountDetail
{
    public int AccountId { get; set; }

    public int? UserId { get; set; }

    public int? AccountTypeId { get; set; }

    public int? BranchId { get; set; }

    public decimal? Balance { get; set; }

    public DateOnly? OpenDate { get; set; }

    public string? Status { get; set; }

    public virtual AccountType? AccountType { get; set; }

    public virtual BranchDetail? Branch { get; set; }

    public virtual ICollection<CardDetail> CardDetails { get; set; } = new List<CardDetail>();

    public virtual ICollection<Transaction> TransactionFromAccounts { get; set; } = new List<Transaction>();

    public virtual ICollection<Transaction> TransactionToAccounts { get; set; } = new List<Transaction>();

    public virtual User? User { get; set; }
}
