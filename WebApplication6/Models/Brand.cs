
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace WebAppCars.Models
{
    public class Brand
    {
        [Key]
        public int? BrandID { get; set; }

        public string BrandName { get; set; }

    }
}
