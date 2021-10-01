using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data.Entities;

namespace Common.Data.Entities
{
    public class ProposalState : IEntity
    {
        public int Id { get; set; }

        public string State { get; set; }
    }
}
