﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ManagementHouseRentals.Models
{
    public class ChangeUserViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneMumber { get; set; }

        [Required]
        [Display(Name = "CC")]
        public string CC { get; set; }

        [Required]
        [Display(Name = "NIF")]
        public string NIF { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }



        [Display(Name = "Current password")]
        public string OldPassword { get; set; }


        [Display(Name = "New password")]
        public string NewPassword { get; set; }


        [Compare("NewPassword")]
        public string Confirm { get; set; }
    }
}
