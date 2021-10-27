using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data.Entities;

namespace Web_ManagementHouseRentals.Models
{
    public class RegisterNewUserViewModel : User
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }


        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Range(900000000,999999999, ErrorMessage = "The field {0} must start with 9 and have to contain 9 numbers.")]
        public int Phone { get; set; }

        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }
    }
}
