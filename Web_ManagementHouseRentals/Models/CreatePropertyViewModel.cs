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
        public string NameProperty { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        [Display(Name = "Area m2")]
        public double Area { get; set; }

        public bool AvailableProperty { get; set; }

        [Display(Name = "Monthly Price")]
        public double MonthlyPrice { get; set; }

        [Display(Name = "Images")]
        public ICollection<IFormFile> ImagesFiles { get; set; }

        public List<Extra> Extras { get; set; }

        [Display(Name = "Property type")]
        public int PropertyTypeId { get; set; }

        public ICollection<SelectListItem> PropertyTypes { get; set; }

        [Display(Name = "Typology")]
        public int SizeTypeId { get; set; }

        public ICollection<SelectListItem> SizeTypes { get; set; }

        [Display(Name = "Energy Certificate")]
        public int EnergyCertificateId { get; set; }

        public ICollection<SelectListItem> EnergyCertificates { get; set; }

    }
}
