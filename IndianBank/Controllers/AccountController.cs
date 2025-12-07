using IndianBank.EntityModel;
using IndianBank.Helper;
using IndianBank.Models;
using IndianBank.Models.AuthenticateViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IndianBank.Controllers
{
    public class AccountController : Controller
    {
        private readonly BankingDbContext _dbCotext;
        public AccountController(BankingDbContext dbCotext)
        {
            _dbCotext = dbCotext;
        }
        public IActionResult SignUp() => View();

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupViewModel signup)
        {
            if (!ModelState.IsValid)
            {
                return View(signup);
            }
            var userExist = await _dbCotext.Users.FirstOrDefaultAsync(x=> x.Email.ToLower().Trim() == signup.Email.ToLower().Trim());
            if (userExist!=null) 
            {
                ModelState.AddModelError("", "User Email Alreasy Exist");
                return View(signup);
            }
            var userId = _dbCotext.Users.OrderByDescending(x => x.UserId).FirstOrDefault().UserId + 1;
            var user = new User
            {
                UserId = userId,
                Username = signup.FullName,
                Email = signup.Email,
                PasswordHash = PasswordHelper.Hash(signup.Password),
                RoleId=1,
                DepartmentId=4
            };
            _dbCotext.Users.Add(user);
            await _dbCotext.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }
            var userExist = await _dbCotext.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Trim() == model.Email.ToLower().Trim());
            if (userExist == null) 
            {
                ModelState.AddModelError("", "User Doesn't Exist");
                return View(model);
            }
            if (!PasswordHelper.verify(model.Password, userExist.PasswordHash))
            {
                ModelState.AddModelError("", "User Password Invalid");
                return View(model);
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userExist.UserId.ToString()),
                new Claim(ClaimTypes.Name,userExist.Username),
                new Claim(ClaimTypes.Email,userExist.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principle = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle,
                new AuthenticationProperties { IsPersistent = model.RememberMe });
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Profile()
        {
            if(!User.Identity.IsAuthenticated) 
                return RedirectToAction("Login");
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userDeatils = await _dbCotext.Users.FirstAsync(x=>x.UserId== userId);
            if (userDeatils==null)
            {
                return NotFound();
            }
            return View(userDeatils);
        }

        public async Task<IActionResult> Edit()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login");
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userDeatils = await _dbCotext.Users.FirstAsync(x => x.UserId == userId);
            if (userDeatils == null)
            {
                return NotFound();
            }
            return View(userDeatils);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel user)
        {
            if(!User.Identity.IsAuthenticated)
                return RedirectToAction("Login");
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (user.UserId != userId) 
            {
                return Forbid();
            }
            var userDeatils = await _dbCotext.Users.FirstAsync(x => x.UserId == userId);
            if (userDeatils == null)
            {
                return NotFound();
            }
            userDeatils.Username = user.FullName;
            user.PasswordHash = PasswordHelper.Hash(user.PasswordHash);

            _dbCotext.Users.Update(userDeatils);
            await _dbCotext.SaveChangesAsync();

            return RedirectToAction("Profile");
        }

    }
}
