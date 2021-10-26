using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data;
using Web_ManagementHouseRentals.Data.Entities;

namespace Common.Data.Entities
{
    public class Proposal : IEntity
    {
        public int Id { get; set; }

        public Property property { get; set; }

        [Display(Name = "State")]
        public ProposalState proposalState { get; set; }

        public string Message { get; set; }

        [Display(Name = "Date")]
        public DateTime ProposalDate { get; set; }

        public User Owner { get; set; }

        public User Client { get; set; }
    }
}
