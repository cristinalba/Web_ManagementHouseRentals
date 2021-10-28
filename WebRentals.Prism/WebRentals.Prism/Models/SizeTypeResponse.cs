using System;
using System.Collections.Generic;
using System.Text;

namespace WebRentals.Prism.Models
{
    public class SizeTypeResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<PropertyResponse> Properties { get; set; }
    }
}
