using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ManagementHouseRentals.Data.Entities
{
    public class EnergyCertificate : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Property> Properties { get; set; }
    }
}
