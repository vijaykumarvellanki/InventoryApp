using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDapper.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 10)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 500, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Location { get; set; }
    }
}
