using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ManagementHouseRentals.Data.Entities
{
    public class Property_Photo : IEntity 
    {

        public int Id { get; set; }


        public byte[] Photography { get; set; }
    }
}
