﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ManagementHouseRentals.Data.Entities
{
    public class ZipCode : IEntity
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Neighborhood { get; set; }
        
    }
}