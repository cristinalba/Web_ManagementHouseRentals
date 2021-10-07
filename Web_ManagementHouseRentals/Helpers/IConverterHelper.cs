using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data.Entities;
using Web_ManagementHouseRentals.Models;

namespace Web_ManagementHouseRentals.Helpers
{
    public interface IConverterHelper
    {
        User ToUser(ChangeUserViewModel model, string path);

        ChangeUserViewModel ToChangeUserViewModel(User user);
    }
}
