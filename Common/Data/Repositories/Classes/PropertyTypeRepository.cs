using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data;
using Web_ManagementHouseRentals.Data.Entities;

namespace Common.Data.Repositories.Classes
{
    public class PropertyTypeRepository : GenericRepository<PropertyType>, IPropertyTypeRepository
    {
        private readonly DataContext _dataContext;

        public PropertyTypeRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;    
        }

        public async Task<List<PropertyType>> GetAllTypes()
        {
            return await _dataContext.PropertyTypes.ToListAsync();
        }

        public async Task<PropertyType> GetPropertyTypeByIdAsync(int id)
        {
            return await _dataContext.PropertyTypes.Where(pt => pt.Id == id).FirstOrDefaultAsync();
        }

    


    }
}
