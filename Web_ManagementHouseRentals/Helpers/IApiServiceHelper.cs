using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ManagementHouseRentals.Helpers
{
    public interface IApiServiceHelper
    {
        Task<Response> GetZipCodeInfo(string urlBase, string controller, string zipCode);
    }
}
