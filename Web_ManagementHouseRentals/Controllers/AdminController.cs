using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data.Entities;
using Web_ManagementHouseRentals.Helpers;
using Web_ManagementHouseRentals.Models;

namespace Web_ManagementHouseRentals.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public AdminController(IUserHelper userHelper,
                                IImageHelper imageHelper,
                                IConverterHelper converterHelper,
                                IConfiguration configuration,
                                UserManager<User> userManager)
        {
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
            _configuration = configuration;
            _userManager = userManager;
        }

        // GET: AdminController
        public IActionResult Index()
        {
            return View();
        }

        // GET: AdminController/IndexCustomers
        public async Task<IActionResult> IndexCustomers()
        {
            //Show Workers and Customers
            //return View(_userHelper.GetAll());

            return View(await _userHelper.GetUsersWithThisRole("Customer"));
        }

        // GET: AdminController/IndexWorkers
        public async Task<IActionResult> IndexWorkers()
        {
            return View(await _userHelper.GetUsersWithThisRole("Worker"));
        }

        // GET: AdminController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            var model = _converterHelper.ToChangeUserViewModel(user);
            return View(model);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userHelper.GetUserByEmailAsync(model.UserName);
                    if (user != null)
                    {
                        var path = model.ImageUrl;
                        if (model.ImageFile != null && model.ImageFile.Length > 0)
                        {
                            path = await _imageHelper.UploadImageAsync(model.ImageFile, "users");
                        }
                        //TODO:Adaptar o converterHelper (usando o -> var user = _converterHepler.ToUser(model) <- O UpdateUserAsync retorna erro de duplicação de dados)
                        //TODO:Terá de ser feito um metodo para a password se ficar na mesma view que o resto dos dados
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.BirthDate = model.BirthDate;
                        user.PhoneNumber = model.PhoneNumber;
                        user.CC = model.CC;
                        user.NIF = model.NIF;
                        user.Address = model.Address;
                        user.ZipCode = model.ZipCode;
                        user.Email = model.Username;
                        user.UserName = model.Username;
                        user.ImageUrl = path;

                        var response = await _userHelper.UpdateUserAsync(user);
                        if (response.Succeeded)
                        {
                            ViewBag.UserMessage = "User updated!";
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // GET: AdminController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: AdminController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            try
            {
                await _userManager.DeleteAsync(user);

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{user.FullName} might be used";
                    ViewBag.ErrorMessage = $"{user.FullName} can´t be deleted because it has other information associated!</br>" +
                                        "Try to delete first that information and then come back to delete the user!";
                }
                return View("Error");
            }
        }
    }
}
