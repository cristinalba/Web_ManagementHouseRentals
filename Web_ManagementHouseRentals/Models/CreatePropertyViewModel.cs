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
        public string NameProperty { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public double Area { get; set; }

        public bool AvailableProperty { get; set; }

        public double MonthlyPrice { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageFile{ get; set; }

        //TODO: Várias Imagens
        //[Display(Name = "Images")]
        //public ICollection<IFormFile> ImagesFile{ get; set; }

        public List<Extra> Extras { get; set; }

        public int PropertyTypeId { get; set; }

        public ICollection<SelectListItem> PropertyTypes { get; set; }

        public int SizeTypeId { get; set; }

        public ICollection<SelectListItem> SizeTypes { get; set; }

        public int EnergyCertificateId { get; set; }

        public ICollection<SelectListItem> EnergyCertificates { get; set; }

    }
}
