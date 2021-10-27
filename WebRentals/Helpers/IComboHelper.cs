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

        ICollection<SelectListItem> GetComboCertificate();

        ICollection<SelectListItem> GetComboProposalStates();
    }
}
