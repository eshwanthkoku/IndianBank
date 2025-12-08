using IndianBank.EntityModel;
using IndianBank.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IndianBank.Controllers
{
    public class TransactionController : Controller
    {
        private readonly BankingDbContext _db;
        public TransactionController(BankingDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var accounts = await _db.AccountDetails
                .Where(a => a.UserId == userId)
                .ToListAsync();

            var acctIds = accounts.Select(a => a.AccountId).ToList();
            var transactions = await _db.Transactions
                .Where(t => acctIds.Contains(t.FromAccountId ?? -1) || acctIds.Contains(t.ToAccountId ?? -1))
                .OrderByDescending(t => t.TransactionDate)
                .Take(50)
                .ToListAsync();
            
            ViewBag.Accounts = accounts;
            return View(transactions);
        }

        public async Task<IActionResult> Deposit(int? accountId)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
            ViewBag.AccountId = accountId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(int accountId, decimal amount)
        {
            if (amount <= 0) ModelState.AddModelError("", "Amount must be positive");
            if (!ModelState.IsValid) { ViewBag.AccountId = accountId; return View(); }

            using var tx = await _db.Database.BeginTransactionAsync();
            try
            {
                var account = await _db.AccountDetails.FindAsync(accountId);
                if (account == null) return NotFound();

                account.Balance += amount;
                _db.AccountDetails.Update(account);

                var transaction = new Transaction
                {
                    TransactionId = GenerateTransactionId(),
                    FromAccountId = null,
                    ToAccountId = account.AccountId,
                    Amount = amount,
                    TransactionDate = DateTime.Now,
                    TransactionTypeId = 1
                };
                _db.Transactions.Add(transaction);

                await _db.SaveChangesAsync();
                await tx.CommitAsync();
                TempData["Success"] = "Deposit successful";
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                ModelState.AddModelError("", "Deposit failed: " + ex.Message);
                ViewBag.AccountId = accountId;
                return View();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Withdraw(int? accountId)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
            ViewBag.AccountId = accountId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(int accountId, decimal amount)
        {
            if (amount <= 0) ModelState.AddModelError("", "Amount must be positive");
            if (!ModelState.IsValid) { ViewBag.AccountId = accountId; return View(); }

            using var tx = await _db.Database.BeginTransactionAsync();
            try
            {
                var account = await _db.AccountDetails.FindAsync(accountId);
                if (account == null) return NotFound();

                if (account.Balance < amount)
                {
                    ModelState.AddModelError("", "Insufficient balance.");
                    ViewBag.AccountId = accountId;
                    return View();
                }

                account.Balance -= amount;
                _db.AccountDetails.Update(account);

                var transaction = new Transaction
                {
                    TransactionId = GenerateTransactionId(),
                    FromAccountId = account.AccountId,
                    ToAccountId = null,
                    Amount = amount,
                    TransactionDate = DateTime.Now,
                    TransactionTypeId = 2
                };
                _db.Transactions.Add(transaction);

                await _db.SaveChangesAsync();
                await tx.CommitAsync();
                TempData["Success"] = "Withdrawal successful";
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                ModelState.AddModelError("", "Withdrawal failed: " + ex.Message);
                ViewBag.AccountId = accountId;
                return View();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Transfer() => View();

        [HttpPost]
        public async Task<IActionResult> Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            if (amount <= 0) ModelState.AddModelError("", "Amount must be positive");
            if (fromAccountId == toAccountId) ModelState.AddModelError("", "Cannot transfer to the same account");
            if (!ModelState.IsValid) return View();

            using var tx = await _db.Database.BeginTransactionAsync();
            try
            {
                var fromAcc = await _db.AccountDetails.FindAsync(fromAccountId);
                var toAcc = await _db.AccountDetails.FindAsync(toAccountId);
                if (fromAcc == null || toAcc == null) return NotFound();

                if (fromAcc.Balance < amount)
                {
                    ModelState.AddModelError("", "Insufficient balance.");
                    return View();
                }

                fromAcc.Balance -= amount;
                toAcc.Balance += amount;
                _db.AccountDetails.Update(fromAcc);
                _db.AccountDetails.Update(toAcc);

                var transaction = new Transaction
                {
                    TransactionId = GenerateTransactionId(),
                    FromAccountId = fromAcc.AccountId,
                    ToAccountId = toAcc.AccountId,
                    Amount = amount,
                    TransactionDate = DateTime.Now,
                    TransactionTypeId = 3
                };
                _db.Transactions.Add(transaction);

                await _db.SaveChangesAsync();
                await tx.CommitAsync();
                TempData["Success"] = "Transfer successful";
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                ModelState.AddModelError("", "Transfer failed: " + ex.Message);
                return View();
            }

            return RedirectToAction("Index");
        }

        private int GenerateTransactionId()
        {
            var max = _db.Transactions.Select(t => (int?)t.TransactionId).Max() ?? 1000000;
            return max + 1;
        }
    }
}
