using Common.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data;
using Web_ManagementHouseRentals.Data.Entities;

namespace Common.Data.Repositories
{
    public interface IProposalRepository : IGenericRepository<Proposal>
    {
        ProposalState GetProposalStates(int id);
        IQueryable<Proposal> GetProposalsFromUser(string email);

        Task<Proposal> GetProposalByIdAsync(int id);



        IQueryable<Proposal> GetAcceptedProposalsAsync(string state);

        Task<Proposal> GetClientProposal(User client, Property property);
    }
}
