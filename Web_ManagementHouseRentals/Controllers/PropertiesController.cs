using Common.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Helpers;
using Web_ManagementHouseRentals.Models;

namespace Web_ManagementHouseRentals.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IComboHelper _comboHelper;
        private readonly IUserHelper _userHelper;

        public PropertiesController(IUserHelper userHelper,
                                    IPropertyRepository propertyRepository, 
                                    IComboHelper comboHelper)
        {
            _propertyRepository = propertyRepository;
            _comboHelper = comboHelper;
            _userHelper = userHelper;
        }

        //[Authorize(Roles = "Admin")]
        // GET: PropertiesController
        public ActionResult Index()
        {
            var properties = _propertyRepository.GetAll()
                .Include(x => x.Owner)
                .OrderBy(o => o.MonthlyPrice);

            return View(properties);
        }

        // GET: PropertiesController/Details/5
        public ActionResult Details(int? id)
        {
            var property = _propertyRepository.GetByIdWithInfoAsync(id.Value);
            if (property == null)
            {
                return NotFound();
            }

            return View();
        }

        // GET: PropertiesController/Create
        public ActionResult Create()
        {
            var model = new CreatePropertyViewModel
            {
                PropertyTypes = _comboHelper.GetComboPropertyTypes(),
                SizeTypes = _comboHelper.GetComboSizeTypes(),
                EnergyCertificates = _comboHelper.GetComboCertificate(),
            };

            return View(model);
        }

        // POST: PropertiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PropertiesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PropertiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PropertiesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PropertiesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
