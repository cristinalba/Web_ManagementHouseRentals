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
        private readonly IMailHelper _mailHelper;
        private readonly UserManager<User> _userManager;
        private Random _random;

        public AdminController(IUserHelper userHelper,
                                IImageHelper imageHelper,
                                IConverterHelper converterHelper,
                                IConfiguration configuration,
                                IMailHelper mailHelper,
                                UserManager<User> userManager)
        {
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
            _configuration = configuration;
            _mailHelper = mailHelper;
            _userManager = userManager;
            _random = new Random();
        }

        // GET: AdminController
        public IActionResult Index()
        {
            return View();
        }
        /////////////////////////////////////////////////////////////////////////////
        ///                                                                 /////////
        ///             MANAGEMENT USERS                                    /////////
        ///                                                                 /////////
        /////////////////////////////////////////////////////////////////////////////
        
        // GET: AdminController/IndexCustomers
        public async Task<IActionResult> IndexCustomers()
        {
            //Show Workers and Customers
            //return View(_userHelper.GetAll());

            return View(await _userHelper.GetUsersWithThisRole("Customer"));
        }

        // GET: AdminController/IndexLandlords
        public async Task<IActionResult> IndexLandlords()
        {
            var customers = await _userHelper.GetUsersWithThisRole("Customer");

            var landlords = customers.Where(x => x.IsLandlord == true);

            return View(landlords);
        }

        //// GET: AdminController/IndexTenants
        public async Task<IActionResult> IndexTenants()
        {
            var customers = await _userHelper.GetUsersWithThisRole("Customer");

            var tenants = customers.Where(x => x.IsLandlord==false);

            return View(tenants);
        }

        //// GET: AdminController/IndexMultiple (Landord+Tenant)
        public async Task<IActionResult> IndexMultiple()
        {
            var customers = await _userHelper.GetUsersWithThisRole("Customer");
            //If the customer isLandlord and if the customer has a contract:
            var customerMultiple = customers.Where(x => x.IsLandlord == true);
            //TODO:apply filter Where() when we have done the management of the contracts

            return View(customerMultiple);
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
        //Create Account of Workers

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(RegisterNewWorkerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user == null)
                {
                    user = new User
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
                        
                    };
                    model.Password = _random.Next(100000, 999999).ToString();
                    var result = await _userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return View(model);
                    }
                    var isInRoleWorker = await _userHelper.IsUserInRoleAsync(user, "Worker");
                    if (!isInRoleWorker)
                    {
                        await _userHelper.AddUserToRoleAsync(user, "Worker");
                    }

                    string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                    string tokenLink = Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    Response response = _mailHelper.SendEmail(model.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                        $"To allow you to log in, " +
                        $"please click in this link:</br></br><a href = \"{tokenLink}\">to confirm your account </a></br></br>" +
                        $"</br>Your password is: {model.Password}, we recommend to change once you are logged");

                    if (response.IsSuccess)
                    {
                        ViewBag.Message = "The instructions to allow the user have been sent to the email";

                        return View(model);
                    }
                }

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
        /////////////////////////////////////////////////////////////////////////////
        ///                                                                 /////////
        ///             MANAGEMENT Properties                               /////////
        ///                                                                 /////////
        /////////////////////////////////////////////////////////////////////////////





        /////////////////////////////////////////////////////////////////////////////
        ///                                                                 /////////
        ///             MANAGEMENT Contracts                                /////////
        ///                                                                 /////////
        /////////////////////////////////////////////////////////////////////////////



        /////////////////////////////////////////////////////////////////////////////
        ///                                                                 /////////
        ///             MANAGEMENT Proposals                                /////////
        ///                                                                 /////////
        /////////////////////////////////////////////////////////////////////////////
    }
}
