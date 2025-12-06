using System;
using System.Collections.Generic;

namespace IndianBank.EntityModel;

public partial class TransactionType
{
    public int TransactionTypeId { get; set; }

    public string TransactionName { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
