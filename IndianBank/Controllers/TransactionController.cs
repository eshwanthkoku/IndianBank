using Microsoft.AspNetCore.Mvc;

namespace IndianBank.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
