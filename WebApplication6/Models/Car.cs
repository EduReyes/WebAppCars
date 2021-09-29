using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCars.Models
{
    public class Car
    {
        //[Key]
        public int? Id { get; set; }
        [Required]
        public string Brand { get; set; }

        [Required]
        [DisplayName("Model")]
        public string Model {get; set;}

        [Required (ErrorMessage = "Car price is required.")]
        [Range(0, int.MaxValue,
        ErrorMessage = "Only positive number allowed")]
        [DisplayName("Price")]
        public int? Price{get; set;}

        public string Company{get; set;}

        public string PhotoPath { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

        public Car(string brand, string model, int? price, string company, string photoPath)
        {
            Brand = brand;
            Model = model;
            Price = price;
            Company = company;
            PhotoPath = photoPath;
        }
        public Car() { }
    }
}