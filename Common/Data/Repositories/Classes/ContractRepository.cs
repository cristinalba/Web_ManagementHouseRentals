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
    public class ContractRepository : GenericRepository<Contract> , IContractRepository
    {
        private readonly DataContext _dataContext;

        public ContractRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public IQueryable<Contract> GetContractsWithInfo()
        {
            return _dataContext.Contracts
                        .Include(c => c.Property)
                        .ThenInclude(c => c.Owner)
                        .Include(c => c.Tenant);
        }
    }
}
