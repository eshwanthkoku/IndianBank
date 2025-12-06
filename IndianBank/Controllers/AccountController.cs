using IndianBank.EntityModel;
using IndianBank.Models.AuthenticateViewModel;
using Microsoft.AspNetCore.Mvc;

namespace IndianBank.Controllers
{
    public class AccountController : Controller
    {
        private readonly BankingDbContext _dbCotext;
        public AccountController(BankingDbContext dbCotext)
        {
            _dbCotext = dbCotext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(LoginViewModel model)
        {
            return View();
        }

    }
}
