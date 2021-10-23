using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ManagementHouseRentals.Data.Entities
{
    public class SizeType : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } //T0, T1, T2, T3, T4

        public ICollection<Property> Properties { get; set; }
    }
}
