using IndianBank.EntityModel;
using IndianBank.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IndianBank.Controllers
{
    public class HomeController : Controller
    {
        private readonly BankingDbContext _bankingDbContext;
        public HomeController(BankingDbContext bankingDbContext)
        {
            _bankingDbContext = bankingDbContext;
        }
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var account = await _bankingDbContext.AccountDetails.Where(x => x.UserId == userId).ToListAsync();
            var listAccountDetails = new List<AccountDetailsViewModel>();
            foreach (var item in account)
            {
                listAccountDetails.Add(new AccountDetailsViewModel
                {
                    AccountId = item.AccountId,
                    AccountTypeId = item.AccountTypeId,
                    Balance = item.Balance.Value,
                    BranchId = item.BranchId,
                    UserId = item.UserId,
                    AccountType = _bankingDbContext.AccountTypes.Find(item.AccountTypeId).AccountTypeName
                });
            }
            return View(listAccountDetails);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
