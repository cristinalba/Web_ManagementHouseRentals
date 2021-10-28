using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ManagementHouseRentals.Models
{
    public class CreateProposalViewModel
    {
        public int PropertyId { get; set; }

        public string OwnerName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Message must contain between 2 and 250 characters.")]
        public string Message { get; set; }
    }
}