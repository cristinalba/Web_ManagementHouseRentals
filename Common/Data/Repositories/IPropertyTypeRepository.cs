using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data.Entities;

namespace Common.Data.Repositories
{
    public interface IPropertyTypeRepository : IGenericRepository<PropertyType>
    {
        Task<List<PropertyType>> GetAllTypes();
        Task<PropertyType> GetPropertyTypeByIdAsync(int id);
    }
}
