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
        public int Id { get; set; } //PropertyID

        public PropertyType type { get; set; }
        
        [Display(Name = "Owner")]
        public User Owner { get; set; }


        [Display(Name = "Property type")]
        public string NameProperty { get; set; } //property Type

        [Display(Name = "Property description")]
        public string Description { get; set; } //property Description: text

        [Display(Name = "Size type")]
        public SizeType SizeType { get; set; } //Size Type

        public string Address { get; set; }//Location

        [Display(Name = "Zip Code")]
        public ZipCode ZipCode { get; set; }

        [Display(Name = "Extras")]
        public List<Extra> Extra { get; set; } //Amenities

        public Photo Cod_Photo { get; set; } //Images

        public double Area { get; set; } //Area

        public double Latitude { get; set; } //Mapa1

        public double Longitude { get; set; } //Mapa2

        [Display(Name = "Energy Certificate")]
        public EnergyCertificate EnergyCertificate { get; set; } //QS

        [Display(Name = "Status")]
        public bool AvailableProperty { get; set; } //Status


        [Display(Name = "Monthly Price")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double MonthlyPrice { get; set; } // Price










    }
}
