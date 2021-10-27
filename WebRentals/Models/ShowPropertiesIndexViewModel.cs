using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data;
using Web_ManagementHouseRentals.Data.Entities;

namespace Web_ManagementHouseRentals.Models
{
    public class ShowPropertiesIndexViewModel
    {
        public IEnumerable<Property> Properties { get; set; }

        public int PropertiesPerPage { get; set; }

        public int CurrentPage { get; set; }

        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(Properties.Count() / (double)PropertiesPerPage));
        }

        public IEnumerable<Property> PaginatedProperties()
        {
            int start = (CurrentPage - 1) * PropertiesPerPage;
            return Properties.OrderBy(p => p.Id).Skip(start).Take(PropertiesPerPage);
        }
    }
}
