using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data;

namespace Common.Data.Repositories
{
    public class PropertyRepository : GenericRepository<Property>, IPropertyRepository
    {
        private readonly DataContext _dataContext;

        public PropertyRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Property> GetByIdWithInfoAsync(int id)
        {
            return await _dataContext.Properties.Where(P => P.Id == id)
                                          .Include(p => p.Type)
                                          .Include(p => p.Owner)
                                          .Include(p => p.SizeType)
                                          .Include(p => p.ZipCode)
                                          .Include(p => p.Extra)
                                          .Include(p => p.EnergyCertificate)
                                          .Include(p => p.PropertyPhotos)
                                          .FirstOrDefaultAsync();
        }

        public async Task<Property> GetPropertyByIdAsync(int id)
        {
            return await _dataContext.Properties.Where(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}
