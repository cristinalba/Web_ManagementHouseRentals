using Common.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data;
using Web_ManagementHouseRentals.Data.Entities;

namespace Web_ManagementHouseRentals.Helpers
{
    public class PropertyHelper
    {
        private readonly DataContext _dataContext;

        public PropertyHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

    }
}
