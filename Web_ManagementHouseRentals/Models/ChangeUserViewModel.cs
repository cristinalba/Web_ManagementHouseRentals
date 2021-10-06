using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data.Entities;

namespace Web_ManagementHouseRentals.Models
{
    public class ChangeUserViewModel : User
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }


        [Display(Name = "Current password")]
        public string OldPassword { get; set; }


        [Display(Name = "New password")]
        public string NewPassword { get; set; }


        [Compare("NewPassword")]
        public string Confirm { get; set; }


        [Display(Name = "Profile Photo")]
        public IFormFile ImageFile { get; set; }
    }
}