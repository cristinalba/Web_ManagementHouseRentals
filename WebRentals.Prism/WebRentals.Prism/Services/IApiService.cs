using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebRentals.Prism.Models;

namespace WebRentals.Prism.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);
    }
}
