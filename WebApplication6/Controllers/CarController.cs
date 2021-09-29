using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCars.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebAppCars.Controllers
{
    public class CarController : Controller
    {
        private UserManager<AppUser> userManager;
        private AppIdentityDbContext _dbcontext;
        private readonly IHostingEnvironment hostingEnvironment;

        //private AppUser user = await userManager.GetUserAsync(HttpContext.User);

        public CarController(UserManager<AppUser> userMgr, AppIdentityDbContext dbcontext,
                             IHostingEnvironment _hostingEnvironment)
        {
            userManager = userMgr;
            _dbcontext = dbcontext;
            hostingEnvironment = _hostingEnvironment;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            string message = "Hello " + user.UserName;
            
            string company = user.Company;

            List<Car> cars = (List<Car>)_dbcontext.GetCars(company);

            ViewBag.company = user.Company;
            return View(cars);
        }


        [HttpGet]
        public async Task<ActionResult> CreateAsync()
        {

            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            ViewBag.company = user.Company;
            var brandlist = (from brand in _dbcontext.Brands
                         select brand).ToList();
            ViewBag.BrandID = new SelectList(brandlist, "BrandName", "BrandName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Car car)
        {

            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            ViewBag.company = user.Company;
            var brandlist = (from brand in _dbcontext.Brands
                             select brand).ToList();
            ViewBag.BrandID = new SelectList(brandlist, "BrandName", "BrandName");

            if (ModelState.IsValid)
            {

                string uniqueFileName = null;
                if (car.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "imagesCar");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + car.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    car.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                car.PhotoPath = uniqueFileName;

                if (!User.IsInRole("Admin")) { car.Company = user.Company; }

                await _dbcontext.AddAsync(car);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            
            
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            ViewBag.company = user.Company;
            var brandlist = (from brand in _dbcontext.Brands
                             select brand).ToList();
            ViewBag.BrandID = new SelectList(brandlist, "BrandName", "BrandName");
            Car car = _dbcontext.GetCarbyID(id);
            if (car != null)
                return View(car);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, string _brand, string model, int price, string company)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            ViewBag.company = user.Company;
            var brandlist = (from brand in _dbcontext.Brands
                             select brand).ToList();
            ViewBag.BrandID = new SelectList(brandlist, "BrandName", "BrandName");
            Car car = _dbcontext.GetCarbyID(id);

            if (!string.IsNullOrEmpty(_brand))
            {
                car.Brand = _brand + "pepe";
            }
            else
                RedirectToAction("Index");

            if (!string.IsNullOrEmpty(model))
            {
                car.Model = model;
            }
            else
                return View(car);

            if (price > 0)
                car.Price = price;
            else
                return View(car);

            if (User.IsInRole("Admin")){car.Company = company;}
            else car.Company = user.Company;

            Car car2 = new Car(car.Brand, car.Model,car.Price,car.Company,car.PhotoPath);
            _dbcontext.Remove(_dbcontext.Cars.Single(a => a.Id == id));
            _dbcontext.SaveChanges();
            await _dbcontext.AddAsync(car2);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _dbcontext.Remove(_dbcontext.Cars.Single(a => a.Id == id));
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
