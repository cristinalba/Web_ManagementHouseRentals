using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data.Entities;
using Web_ManagementHouseRentals.Models;

namespace Web_ManagementHouseRentals.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
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
    }
}
