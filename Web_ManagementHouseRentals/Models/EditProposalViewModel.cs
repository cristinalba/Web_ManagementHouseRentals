using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ManagementHouseRentals.Models
{
    public class EditProposalViewModel
    {
        public int Id { get; set; }

        public string OwnerId { get; set; }

        public string ClientId { get; set; }

        public string ClientName { get; set; }

        public int PropertyId { get; set; }

        [Display(Name = "State")]
        public int ProposalStateId { get; set; }

        public ICollection<SelectListItem> ProposalStates { get; set; }

        public string Message { get; set; }

        public string ResponseMessage { get; set; }

        public DateTime ProposalDate { get; set; }

        public string propertyOwner { get; set; }

        public string loggedUser { get; set; }
    }
}
