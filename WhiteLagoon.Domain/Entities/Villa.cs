using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteLagoon.Domain.Entities
{
	public class Villa
	{
        public int Id { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Range(10,1000)]
        [Display(Name = "Price per night")]
        public double Price { get; set; }
        public int Sqft { get; set; }
        [Range(20,50)]
        public int Occupancy { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        [Display(Name ="Image Url")]
        public string? ImageURL { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ValidateNever]
        public IEnumerable<Amenity> VillaAmenity { get; set; }
        [NotMapped]
        public bool IsAvailable { get; set; } = true;
    }
}
