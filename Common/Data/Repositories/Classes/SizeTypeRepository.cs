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
    public class SizeTypeRepository : GenericRepository<SizeType>, ISizeTypeRepository
    {
        private readonly DataContext _dataContext;

        public SizeTypeRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<SizeType> GetSizeTypeByIdAsync(int id)
        {
            return await _dataContext.SizeTypes.Where(pt => pt.Id == id).FirstOrDefaultAsync();
        }
    }
}
