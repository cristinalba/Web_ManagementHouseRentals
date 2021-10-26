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

        [Required]
        public PropertyType Type { get; set; }

        [Required]
        [Display(Name = "Owner")]
        public User Owner { get; set; }

        [Required]
        [Display(Name = "Property name")]
        public string NameProperty { get; set; } //property Type

        [Required]
        [Display(Name = "Property description")]
        public string Description { get; set; } //property Description: text

        [Required]
        [Display(Name = "Size type")]
        public SizeType SizeType { get; set; } //Size Type


        public string Address { get; set; }//Location


        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }


        [Display(Name = "Extras")]
        public ICollection<Extra> Extra { get; set; } //Amenities


        public double Area { get; set; } //Area


        public double Lat{ get; set; } //Mapa1


        public double Long { get; set; } //Mapa2

        [Required]
        [Display(Name = "Energy Certificate")]
        public EnergyCertificate EnergyCertificate { get; set; } //QS

        [Required]
        [Display(Name = "Is property available?")]
        public bool AvailableProperty { get; set; } //Status

        [Display(Name = "Is property deleted?")]
        public bool IsPropertyDeleted { get; set; } 
        //To deactivate the property, as it is not allow to delete in cascade 

        [Required]
        [Display(Name = "Monthly Price")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double MonthlyPrice { get; set; } // Price


        [Display(Name = "Property Photos")]
        public ICollection<Property_Photo> PropertyPhotos { get; set; }
    }
}
