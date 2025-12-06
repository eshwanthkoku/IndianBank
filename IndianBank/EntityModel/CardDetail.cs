using System;
using System.Collections.Generic;

namespace IndianBank.EntityModel;

public partial class CardDetail
{
    public int CardId { get; set; }

    public int? AccountId { get; set; }

    public string? CardNumber { get; set; }

    public string? CardType { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public string? Cvv { get; set; }

    public virtual AccountDetail? Account { get; set; }
}
