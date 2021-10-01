using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data;

namespace Common.Data.Entities
{
    class Proposal
    {
        public int Id { get; set; }
   
        public Property property { get; set; }

        public ProposalState ProposalState { get; set; }

        public string Message { get; set; }
    }
}
