
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data;
using Web_ManagementHouseRentals.Data.Entities;
using Web_ManagementHouseRentals.Models;

namespace Web_ManagementHouseRentals.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        //USER
        public User ToUser(ChangeUserViewModel model, string path)
        {

            return new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                PhoneNumber = model.PhoneNumber,
                CC = model.CC,
                NIF = model.NIF,
                Address = model.Address,
                ZipCode = model.ZipCode,
                Email = model.Username,
                UserName = model.Username,
                ImageUrl = path
            };
        }

        public ChangeUserViewModel ToChangeUserViewModel(User user)
        {
            return new ChangeUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                PhoneNumber = user.PhoneNumber,
                CC = user.CC,
                NIF = user.NIF,
                Address = user.Address,
                ZipCode = user.ZipCode,
                Email = user.UserName,
                Username = user.UserName,
                ImageUrl = user.ImageUrl
            };
        }

        //PROPERTY
        public Property ToProperty(CreatePropertyViewModel model, List<Extra> Extras, EnergyCertificate energyCertificate, PropertyType propertyType, SizeType sizeType)
        {

            return new Property
            {
                NameProperty = model.NameProperty,
                Description = model.Description,
                Address = model.Address,
                Area = model.Area,
                AvailableProperty = model.AvailableProperty,
                MonthlyPrice = model.MonthlyPrice,
                Type = propertyType,
                EnergyCertificate = energyCertificate,
                Extra = Extras,
                SizeType = sizeType,
            };
        }

        //TODO: Deixamos o cliente editar todos os campos?
        //public CreatePropertyViewModel ToPropertyViewModel(Property property)
        //{
        //    return new CreatePropertyViewModel
        //    {
        //        NameProperty = property.NameProperty,
        //        Description = property.Description,
        //        Address = property.Address,
        //        Area = property.Area,
        //        AvailableProperty = property.AvailableProperty,
        //        MonthlyPrice = property.MonthlyPrice,
        //        Type = property.PropertyType,
        //        EnergyCertificate = energyCertificate,
        //        Extra = Extras,
        //        SizeType = sizeType,
        //    };
        //}

    }
}
