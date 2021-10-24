using Common.Data.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data;

namespace Web_ManagementHouseRentals.Helpers
{
    public class ComboHelper : IComboHelper
    {
        private readonly DataContext _context;

        public ComboHelper(DataContext dataContext)
        {
            _context = dataContext;
        }

        public ICollection<SelectListItem> GetComboCertificate()
        {
            var list = _context.EnergyCertificates.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select an energy certificate...)",
                Value = "0"
            });

            return list;
        }

        public ICollection<SelectListItem> GetComboPropertyTypes()
        {
            var list = _context.PropertyTypes.Select(pt => new SelectListItem
            {
                Text = pt.Name,
                Value = pt.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Select type",
                Value = "0"
            });

            return list;
        }

        public ICollection<SelectListItem> GetComboSizeTypes()
        {
            var list = _context.SizeTypes.Select(st => new SelectListItem
            {
                Text = st.Name,
                Value = st.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a size type...)",
                Value = "0"
            });

            return list;
        }
    }
}
