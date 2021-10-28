using System;
using System.Collections.Generic;
using System.Text;

namespace WebRentals.Prism.Models
{
    public class PropertyResponse
    {
        public int Id { get; set; } //PropertyID


        public PropertyTypeResponse Type { get; set; } //property Type


        public UserResponse Owner { get; set; }


        public string NameProperty { get; set; }


        public string Description { get; set; } //property Description: text


        public string Address { get; set; }//Location


        public string ZipCode { get; set; }

        public double Lat { get; set; } //Mapa1

        public double Long { get; set; } //Mapa2

        public string Locality { get; set; }

        public string Municipality { get; set; }

        public string District { get; set; }


        public double Area { get; set; } //Area


        public SizeTypeResponse SizeType { get; set; } //Size Type


        public ICollection<ExtraResponse> Extra { get; set; } //Amenities


        public EnergyCertificateResponse EnergyCertificate { get; set; } //QS

        public bool AvailableProperty { get; set; } //Status


        public bool IsPropertyDeleted { get; set; }
        //To deactivate the property, as it is not allow to delete in cascade 


        public double MonthlyPrice { get; set; } // Price


        public ICollection<Property_PhotoResponse> PropertyPhotos { get; set; }

        public string PhotoMobile { get; set; }
    }
}
