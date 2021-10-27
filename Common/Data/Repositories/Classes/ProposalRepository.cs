using Common.Data.Entities;
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
    public class ProposalRepository : GenericRepository<Proposal>, IProposalRepository
    {
        private readonly DataContext _dataContext;

        public ProposalRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Proposal> GetProposalByIdAsync(int id)
        {
            return await _dataContext.Proposals.Where(p => p.Id == id)
                                                .Include(p => p.Owner)
                                                .Include(p => p.Client)
                                                .Include(p => p.property)
                                                .Include(p => p.proposalState)
                                                .FirstOrDefaultAsync();
        }

        public IQueryable<Proposal> GetProposalsFromUser(string email)
        {
            return _dataContext.Proposals.Where(p => p.Owner.Email == email)
                                               .Include(p => p.Client)
                                               .Include(p => p.Owner)
                                               .Include(p => p.property)
                                               .Include(p => p.proposalState);
        }

        public ProposalState GetProposalStates(int id)
        {
            return _dataContext.ProposalStates.Where(p => p.Id == id).FirstOrDefault();
        }

        public IQueryable<Proposal> GetAcceptedProposalsAsync(string state)
        {
            return _dataContext.Proposals.Where(p => p.proposalState.State == state)
                                                                  .Include(p => p.Client)
                                                                  .Include(p => p.Owner)
                                                                  .Include(p => p.property)
                                                                  .Include(p => p.proposalState);
        }

        public async Task<Proposal> GetClientProposal(User client, Property property)
        {
            return await _dataContext.Proposals.Where(p => p.Client == client && p.property == property)
                                                .Include(p => p.proposalState)
                                                .FirstOrDefaultAsync();
        }

    }
}
