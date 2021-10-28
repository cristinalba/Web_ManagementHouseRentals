using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data;

namespace Common.Data.Repositories
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
        Task<Property> GetByIdWithInfoAsync(int id);
        
        Task<Property> GetPropertyByIdAsync(int id);
        
        Task<IEnumerable<Property>> GetPropertiesOfCustomerAsync(string id);

        Task<IEnumerable<Property>> GetPropertiesToIndexAsync();

        IQueryable<Property> GetPropertiesToApi();
    }
}
