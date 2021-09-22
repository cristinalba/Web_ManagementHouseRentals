using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data.Entities;

namespace Web_ManagementHouseRentals.Data
{
    public class Property : IEntity
    {
        public int Id { get; set; }

        public PropertyType type;
        
        [Display(Name = "Owner")]
        public User Owner { get; set; }


        [Display(Name = "Name Property")]
        public string NameProperty { get; set; }

        public string Description { get; set; }

        public SizeType SizeType { get; set; }

        public string Address { get; set; }

        public ZipCode ZipCode { get; set; }

        public List<Extra> Extra { get; set; }

        public Photo Cod_Photo { get; set; }

        public double Area { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public EnergyCertificate EnergyCertificate { get; set; }

        public bool AvailableProperty { get; set; }










    }
}
