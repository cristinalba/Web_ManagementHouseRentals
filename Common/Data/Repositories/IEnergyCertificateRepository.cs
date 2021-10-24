using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data.Entities;

namespace Common.Data.Repositories
{
    public interface IEnergyCertificateRepository : IGenericRepository<EnergyCertificate>
    {
        Task<EnergyCertificate> GetEnergyCertificateByIdAsync(int id);

    }
}
