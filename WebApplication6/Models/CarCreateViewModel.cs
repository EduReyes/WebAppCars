using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCars.Models
{
    public class CarCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        [DisplayName("Model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Car price is required.")]
        [Range(0, int.MaxValue,
        ErrorMessage = "Only positive number allowed")]
        [DisplayName("Price")]
        public int Price { get; set; }

        public string Company { get; set; }
        public IFormFile Photo { get; set; }
    }
}
