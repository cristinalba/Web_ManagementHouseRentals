using System;
using System.Collections.Generic;
using System.Text;

namespace WebRentals.Prism.Models
{
    public class Property_PhotoResponse
    {
        public int Id { get; set; }

        public PropertyResponse Property { get; set; }

        public string ImageUrl { get; set; }
    }
}
