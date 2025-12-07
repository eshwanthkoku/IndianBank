namespace IndianBank.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public int? RoleId { get; set; }

        public virtual ICollection<AccountDetailsViewModel> AccountDetails { get; set; }
    }
}
