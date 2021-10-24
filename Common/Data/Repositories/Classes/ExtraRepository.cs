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
    public class ExtraRepository : GenericRepository<Extra>, IExtraRepository
    {
        private readonly DataContext _dataContext;

        public ExtraRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Extra>> GetAllExtras()
        {
            return await _dataContext.Extras.ToListAsync();
        }


        public async Task<Extra> GetExtraByIdAsync(int id)
        {
            return await _dataContext.Extras.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

    }
}
