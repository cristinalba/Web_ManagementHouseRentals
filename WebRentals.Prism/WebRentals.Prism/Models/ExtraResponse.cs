using System;
using System.Collections.Generic;
using System.Text;

namespace WebRentals.Prism.Models
{
    public class ExtraResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Assigned { get; set; }

        public ICollection<PropertyResponse> Properties { get; set; }
    }
}
