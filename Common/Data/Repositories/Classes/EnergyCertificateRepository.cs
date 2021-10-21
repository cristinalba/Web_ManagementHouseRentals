using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data;
using Web_ManagementHouseRentals.Data.Entities;

namespace Common.Data.Repositories.Classes
{
    public class EnergyCertificateRepository : GenericRepository<EnergyCertificate>, IEnergyCertificateRepository
    {
        private readonly DataContext _dataContext;

        public EnergyCertificateRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
