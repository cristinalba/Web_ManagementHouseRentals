using Common.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data;

namespace Common.Data.Repositories.Classes
{
    public class ProposalRepository : GenericRepository<Proposal>, IProposalRepository
    {
        private readonly DataContext _dataContext;

        public ProposalRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
