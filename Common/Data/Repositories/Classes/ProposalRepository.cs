using Common.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data;
using Web_ManagementHouseRentals.Data.Entities;

namespace Common.Data.Repositories.Classes
{
    public class ProposalRepository : GenericRepository<Proposal>, IProposalRepository
    {
        private readonly DataContext _dataContext;

        public ProposalRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
       

        public ProposalState GetProposalStates(int id)
        {
            return _dataContext.ProposalStates.Where(p => p.Id == id).FirstOrDefault();
        }
    }
}
