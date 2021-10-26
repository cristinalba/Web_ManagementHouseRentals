using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ManagementHouseRentals.Helpers
{
    public interface IApiService
    {
        Task<Response> GetZipCodeInfo(string urlBase, string controller, string zipCode);
    }
}
