
using Common.Data.Entities;
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
        public Property ToProperty(CreatePropertyViewModel model, List<Extra> Extras, EnergyCertificate energyCertificate, PropertyType propertyType, SizeType sizeType, User owner, List<ZipCodeHelper> temporaryZipCode)
        {
            owner.IsLandlord = true;
            return new Property
            {
                NameProperty = model.NameProperty,
                Description = model.Description,
                Address = model.Address,
                Area = model.Area,
                MonthlyPrice = model.MonthlyPrice,
                Type = propertyType,
                EnergyCertificate = energyCertificate,
                Extra = Extras,
                SizeType = sizeType,
                Owner = owner,
                ZipCode = temporaryZipCode[0].CodigoPostal,
                Lat = temporaryZipCode[0].Latitude,
                Long = temporaryZipCode[0].Longitude,
                Locality = temporaryZipCode[0].Freguesia,
                Municipality = temporaryZipCode[0].Concelho,
                District = temporaryZipCode[0].Distrito
            };

        }

        public Proposal ToProposalAsync(CreateProposalViewModel model, User client, User owner, Property property, ProposalState proposalState)
        {
            return new Proposal
            {
                property = property,
                proposalState = proposalState,
                Message = model.Message,
                ProposalDate = DateTime.Now,
                Owner = owner,
                Client = client
            };
        }

        public Proposal ToResponseProposalAsync(EditProposalViewModel model, User client, User owner, Property property, ProposalState proposalState)
        {
            return new Proposal
            {
                property = property,
                proposalState = proposalState,
                Message = model.ResponseMessage,
                ProposalDate = DateTime.Now,
                Owner = client,
                Client = owner
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
