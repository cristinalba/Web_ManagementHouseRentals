using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ManagementHouseRentals.Helpers
{
    public class ZipCodeHelper
    {
        //Class para apanhar as propriedades necessárias da api

        [JsonProperty("CodigoPostal")]
        public string CodigoPostal { get; set; }

        [JsonProperty("Morada")]
        public string Morada { get; set; }

        [JsonProperty("Localidade")]
        public string Localidade { get; set; }

        [JsonProperty("Freguesia")]
        public string Freguesia { get; set; }

        [JsonProperty("Concelho")]
        public string Concelho { get; set; }

        [JsonProperty("CodigoDistrito")]
        public int CodigoDistrito { get; set; }

        [JsonProperty("Distrito")]
        public string Distrito { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

    }
}
