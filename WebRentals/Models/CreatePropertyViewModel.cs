using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data;
using Web_ManagementHouseRentals.Data.Entities;

namespace Web_ManagementHouseRentals.Models
{
    public class CreatePropertyViewModel
    {
        [Display(Name = "Ad Title")]
        [Required]
        [StringLength(80, MinimumLength = 4, ErrorMessage = "Title must contain between 4 and 80 characters.")]
        public string NameProperty { get; set; }
        
        [Required]
        [StringLength(800, MinimumLength = 10, ErrorMessage = "Description must contain between 10 and 800 characters.")]
        public string Description { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Address must contain between 5 and 150 characters.")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        [RegularExpression(@"^\d{4}(-\d{3})?$", ErrorMessage = "Incorrect Zip Code fomart. Ex: 2000-000")]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Area m²")] 
        [Range(5 , 1000000, ErrorMessage = "Minimum 5m² and maximum 1000000m².")]
        public double Area { get; set; }

        [Required]
        [Display(Name = "Monthly Price")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Range(0.0001, 1000000, ErrorMessage = "Minimum 1€ and maximum value of 1000000€.")]
        public double MonthlyPrice { get; set; }

        [Required(ErrorMessage = "Insert at least one image.")]
        [Display(Name = "Images")]
        public ICollection<IFormFile> ImagesFiles { get; set; }

        public List<Extra> Extras { get; set; }

        [Required]
        [Display(Name = "Property type")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a property type.")]
        public int PropertyTypeId { get; set; }

        public ICollection<SelectListItem> PropertyTypes { get; set; }

        [Required]
        [Display(Name = "Typology")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a size type.")]
        public int SizeTypeId { get; set; }

        public ICollection<SelectListItem> SizeTypes { get; set; }

        [Required]
        [Display(Name = "Energy Certificate")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select an energy certificate.")]
        public int EnergyCertificateId { get; set; }

        public ICollection<SelectListItem> EnergyCertificates { get; set; }
    }
}
