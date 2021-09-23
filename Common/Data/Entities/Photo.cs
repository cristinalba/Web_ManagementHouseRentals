using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ManagementHouseRentals.Data.Entities
{
    public class Photo : IEntity
    {
        public int Id { get; set; }

       
        public List<Property_Photo> Photography { get; set; }
        

        
    }
}
