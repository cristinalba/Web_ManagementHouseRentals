using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ManagementHouseRentals.Data.Entities
{
    public class PropertyType : IEntity 
    {
        public int Id { get; set; }

        [Display(Name = "Property type")]
        public string Name { get; set; }

        public ICollection<Property> Properties { get; set; }
    }
}
