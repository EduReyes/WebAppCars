using System.Threading.Tasks;
using WebAppCars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAppCars.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<AppUser> userManager;
        private AppIdentityDbContext _dbcontext;
        public HomeController(UserManager<AppUser> userMgr, AppIdentityDbContext dbcontext)
        {
            userManager = userMgr;
            _dbcontext = dbcontext;
        }

        [Authorize]

        public async Task<IActionResult> Index()
        {
            //var identity = (System.Security.Claims.ClaimsIdentity)_dbcontext.User.Identity;
            //string fullname1 = identity.Claims.FirstOrDefault(c => c.Type == "FullName").Value;
            

            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            string message = "Hello " + user.UserName;


            @ViewBag.company = user.Company;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}