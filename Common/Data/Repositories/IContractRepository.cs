using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data.Entities;

namespace Common.Data.Repositories
{
    public interface IContractRepository : IGenericRepository<Contract>
    {
        IQueryable<Contract> GetContractsWithInfo();

        IQueryable<Contract> GetContractsOfUserAsync(string mail);
    }
}
