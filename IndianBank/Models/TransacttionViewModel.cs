namespace IndianBank.Models
{
    public class TransacttionViewModel
    {
        public int TransactionId { get; set; }
        public int? FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public Decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int? TransactionTypeId { get; set; }
    }
}
