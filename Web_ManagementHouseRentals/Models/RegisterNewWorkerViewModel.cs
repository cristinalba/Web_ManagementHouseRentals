using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data.Entities;

namespace Web_ManagementHouseRentals.Models
{
    public class RegisterNewWorkerViewModel: User
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
       
        [MinLength(6)]
        public string Password { get; set; }

       
        [Compare("Password")]
        public string Confirm { get; set; }
    }
}
