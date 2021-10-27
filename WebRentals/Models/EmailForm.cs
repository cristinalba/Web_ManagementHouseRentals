using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ManagementHouseRentals.Models
{
    public class EmailForm
    {
        //Contact Form
        [Required(ErrorMessage = "'Name' required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "'Mail' required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "'Subject' required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "'Message' required")]
        public string Message { get; set; }

       
    }
}
