using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAppCars.Models
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public IList<Car> GetCars(string company)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (comparer.Compare(company, "Admin") == 0)
            {
                return Cars.ToList();
            }
            else 
            return Cars.Where(c => c.Company == company).ToList();
        }

        public Car GetCarbyID(int Id)
        {
            return Cars.Where(c => c.Id == Id).First();
        }

        public void AddCars(Car car)
        {
            Cars.Add(car);

        }

    }
}