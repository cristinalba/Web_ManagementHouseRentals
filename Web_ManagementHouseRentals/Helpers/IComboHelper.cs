using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ManagementHouseRentals.Helpers
{
    public interface IComboHelper
    {
        ICollection<SelectListItem> GetComboPropertyTypes();

        ICollection<SelectListItem> GetComboSizeTypes();

        //TODO: João Check box
        //ICollection<SelectListItem> GetComboExtras();

        ICollection<SelectListItem> GetComboCertificate();
    }
}
