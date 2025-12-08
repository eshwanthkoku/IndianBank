namespace IndianBank.Models
{
    public class AccountDetailsViewModel
    {
        public int AccountId { get; set; }
        public int? AccountTypeId { get; set; }
        public string? AccountType { get; set; }
        public int? BranchId { get; set; }
        public string? Branch { get; set; }
        public int? UserId { get; set; }
        public decimal Balance { get; set; }
    }
}
