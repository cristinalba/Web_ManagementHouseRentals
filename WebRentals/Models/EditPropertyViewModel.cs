using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data;

namespace Web_ManagementHouseRentals.Models
{
    public class EditPropertyViewModel : Property
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
