using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data.Entities;

namespace Web_ManagementHouseRentals.Helpers
{
    public class ApiService : IApiService
    {

        public async Task<Response> GetZipCodeInfo(string urlBase, string controller, string zipCode)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);

                var controllerZipCode = $@"{controller}{zipCode}";

                var response = await client.GetAsync(controllerZipCode);

                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                var AdressInfo = JsonConvert.DeserializeObject<List<ZipCodeHelper>>(result);

                return new Response
                {
                    IsSuccess = true,
                    Results = AdressInfo
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
