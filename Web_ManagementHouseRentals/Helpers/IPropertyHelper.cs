using Common.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data.Entities;

namespace Web_ManagementHouseRentals.Helpers
{
    public interface IPropertyHelper : IGenericRepository<Extra>, 
        IGenericRepository<PropertyType>, 
        IGenericRepository<SizeType>, 
        IGenericRepository<ZipCode>
    {

    }
}
