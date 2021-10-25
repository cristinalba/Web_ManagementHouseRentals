using Common.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data.Entities;
using Web_ManagementHouseRentals.Helpers;

namespace Web_ManagementHouseRentals.Controllers
{
    public class PropertyTypesController : Controller
    {
        private readonly IPropertyTypeRepository _propertyType;

        public PropertyTypesController(IPropertyTypeRepository propertyType)
        {

            _propertyType = propertyType;
            
        }

        // GET: PropertyTypesController
        public async Task<IActionResult> Index()
        {
            var types = await _propertyType.GetAllTypes();
            return View(types);
        }

        // GET: PropertyTypesController/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: PropertyTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PropertyType type)
        {
            if(type == null)
            {
                return NotFound();
            }

            await _propertyType.CreateAsync(type);

            return RedirectToAction("Index");

        }

        // GET: PropertyTypesController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var type = await _propertyType.GetByIdAsync(id.Value);

            if (type == null)
            {
                return NotFound();
            }

            return View(type);           
        }

        // POST: PropertyTypesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var type = await _propertyType.GetByIdAsync(id);
            try
            {
                await _propertyType.DeleteAsync(type);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return View(e);
            }
        }
        // GET: PropertyTypesController/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var type = await _propertyType.GetByIdAsync(id.Value);

        //    if (type == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(type);
        //}
    }
}
