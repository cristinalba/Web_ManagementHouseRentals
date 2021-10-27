using Common.Data.Repositories;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        private readonly IPropertyRepository _propertyRepository;
        private readonly IProposalRepository _proposalRepository;
        private readonly IContractRepository _contractRepository;
        private Random _random;
        private object sendMail;

        public AdminController(IUserHelper userHelper,
                                IImageHelper imageHelper,
                                IConverterHelper converterHelper,
                                IConfiguration configuration,
                                IMailHelper mailHelper,
                                UserManager<User> userManager,
                                IPropertyRepository propertyRepository,
                                IProposalRepository proposalRepository,
                                IContractRepository contractRepository)
        {
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
            _configuration = configuration;
            _mailHelper = mailHelper;
            _userManager = userManager;
            _propertyRepository = propertyRepository;
            _proposalRepository = proposalRepository;
            _contractRepository = contractRepository;
            _random = new Random();
        }

        // GET: AdminController
        public async Task<IActionResult> Index()
        {
            var customers = await _userHelper.GetUsersWithThisRole("Customer");
            var landlords = customers.Where(x => x.IsLandlord == true);
            var tenants = customers.Where(x => x.IsLandlord == false);
            var workers = await _userHelper.GetUsersWithThisRole("Worker");         

            var model = new AdminViewModel 
            { 
              TotalLandlords=  landlords.Count(),
              TotalTenants = tenants.Count(),
              TotalWorkers = workers.Count(),
              TotalProperties = _propertyRepository.GetAll().Count()
            };
            return View(model);
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
                    ViewBag.ErrorTitle =   $"{user.FullName} might be used";
                    ViewBag.ErrorMessage = $"{user.FullName} can´t be deleted because it has other information associated!</br>" +
                                            "Try to delete first that information and then come back to delete the user!";
                }
                return View("ErrorAdmin");
            }
        }
        /////////////////////////////////////////////////////////////////////////////
        ///                                                                 /////////
        ///             MANAGEMENT Properties                               /////////
        ///                                                                 /////////
        /////////////////////////////////////////////////////////////////////////////

        // GET: PropertiesController/IndexProperties
        public ActionResult IndexProperties() //List of new properties
        {
            var properties = _propertyRepository.GetAll()
                .Include(x => x.Owner)
                .OrderBy(o => o.MonthlyPrice)
                .Where(y => y.IsPropertyDeleted == false && y.AvailableProperty == false);

            return View(properties);
        }

        // GET: PropertiesController/IndexEnableProperties
        public ActionResult IndexAvailableProperties() //List of properties available
        {
            var properties = _propertyRepository.GetAll()
                .Include(x => x.Owner)
                .OrderBy(o => o.MonthlyPrice)
                .Where(y => y.IsPropertyDeleted==false && y.AvailableProperty==true);

            return View(properties);
        }

        // GET: AdminController/Enable/5
        public async Task<ActionResult> Enable(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("PropertyNotFound");
            }

            var model = new EditPropertyViewModel();
            var property = await _propertyRepository.GetByIdAsync(id.Value);
            if (property == null)
            {
                return new NotFoundViewResult("PropertyNotFound");
            }
            model.Id = property.Id;
            model.NameProperty = property.NameProperty;
            model.MonthlyPrice = property.MonthlyPrice;
            
            return View(model);
        }

        // POST: AdminController/Enable/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Enable(int id, EditPropertyViewModel model)
        {
            if (id != model.Id)
            {
                return new NotFoundViewResult("PropertyNotFound");
            }

            try
            {
                var property = await _propertyRepository.GetPropertyByIdAsync(model.Id);
                

                if (property != null)
                {

                    model.NameProperty = property.NameProperty;
                    model.MonthlyPrice = property.MonthlyPrice;
                    property.AvailableProperty = true;

                    await _propertyRepository.UpdateAsync(property);

                    Response response = _mailHelper
                        .SendEmail(property.Owner.Email, "Confirmation from Rental4U", $"<h3>The publication of your property has been approved</h3>" +
                                                     $"Thank you for choosing us");

                }
                else
                {
                    return new NotFoundViewResult("PropertyNotFound");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _propertyRepository.ExistAsync(model.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(IndexProperties));
        }

        // GET: AdminController/Disable/5
        public async Task<ActionResult> Disable(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("PropertyNotFound");
            }

            var model = new EditPropertyViewModel();
            var property = await _propertyRepository.GetByIdAsync(id.Value);
            if (property == null)
            {
                return new NotFoundViewResult("PropertyNotFound");
            }
            model.Id = property.Id;
            model.NameProperty = property.NameProperty;
            model.MonthlyPrice = property.MonthlyPrice;

            return View(model);
        }

        // POST:  AdminController/Disable/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disable(int id, EditPropertyViewModel model)
        {
            if (id != model.Id)
            {
                return new NotFoundViewResult("PropertyNotFound");
            }

            try
            {
                var property = await _propertyRepository.GetPropertyByIdAsync(model.Id);

                if (property != null)
                {
                    property.NameProperty = model.NameProperty;
                    property.MonthlyPrice = model.MonthlyPrice;
                    property.IsPropertyDeleted = true;
                    await _propertyRepository.UpdateAsync(property);
                }
                else
                {
                    return new NotFoundViewResult("PropertyNotFound");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _propertyRepository.ExistAsync(model.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(IndexAvailableProperties));
        }


        /////////////////////////////////////////////////// <returns></returns>
        //GET: Admin/DetailsProperty/5
        public async Task<IActionResult> DetailsProperty(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("PropertyNotFound");
            }

            var property = await _propertyRepository.GetByIdAsync(id.Value);

            if (property == null)
            {
                return new NotFoundViewResult("PropertyNotFound");
            }

            return View(property);
        }

        // GET: Admin/DeleteProperty/5
        public async Task<ActionResult> DeleteProperty(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("PropertyNotFound");
            }

            var property = await _propertyRepository.GetByIdAsync(id.Value);
            if (property == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            return View(property);
        }

        // POST: PropertiesController/Delete/5
        [HttpPost, ActionName("DeleteProperty")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePropertyConfirmed(int id)
        {
            var property = await _propertyRepository.GetByIdAsync(id);

            try
            {
                await _propertyRepository.DeleteAsync(property);
                return RedirectToAction(nameof(IndexProperties));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle =   "The property might be in used";
                    ViewBag.ErrorMessage = "It is not possible to deleted because it has other information associated!</br>" +
                                            "Try to delete first that information and then come back to delete the property!";
                }
                return View("ErrorAdmin");
            }
        }
        public IActionResult PropertyNotFound()
        {
            return View();
        }

        /////////////////////////////////////////////////////////////////////////////
        ///                                                                 /////////
        ///             MANAGEMENT Proposals                                /////////
        ///                                                                 /////////
        /////////////////////////////////////////////////////////////////////////////
        ///

        public IActionResult IndexPendingProposals()
        {

            var proposals = _proposalRepository.GetAcceptedProposalsAsync("Wating for Admin approval");

            return View(proposals);
        }

        /////////////////////////////////////////////////////////////////////////////
        ///                                                                 /////////
        ///             MANAGEMENT Contracts                                /////////
        ///                                                                 /////////
        /////////////////////////////////////////////////////////////////////////////
        ///

        public IActionResult IndexContracts()
        {
            var contracts = _contractRepository.GetContractsWithInfo().OrderByDescending(c => c.Start);

            return View(contracts);
        }


        public async Task<IActionResult> CreatePdfContract(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var proposal = await _proposalRepository.GetProposalByIdAsync(id.Value);
            var proposalClient = await _proposalRepository.GetClientProposal(proposal.Client, proposal.property);

            proposalClient.proposalState = _proposalRepository.GetProposalStates(1);
            proposal.proposalState = _proposalRepository.GetProposalStates(1);

            proposal.property.IsPropertyDeleted = true;

            await _proposalRepository.UpdateAsync(proposal);
            await _proposalRepository.UpdateAsync(proposalClient);
            await _propertyRepository.UpdateAsync(proposal.property);


            string guid = _random.Next(10000, 99999).ToString();
            var landlordEmail = proposal.property.Owner.Email;
            var tenantEmail = proposal.Client.Email;
            var landlordName = proposal.property.Owner.FullName;
            var tenantName = proposal.Client.FullName;
            var propertyId = proposal.property.Id;

            Contract contract = new()
            {
                GuidId = guid,
                Tenant = proposal.Client,
                Start = DateTime.Now,
                MonthlyPrice = proposal.property.MonthlyPrice,
                Property = proposal.property,
                ContractIssued = true
            };
            await _contractRepository.CreateAsync(contract);

            
            var pathPdf = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\pdf\\");

            string pathTemplate = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\pdf\\template\\rental4ucontract.pdf");

            var namePdf = contract.GuidId + ".pdf";
            string newFile = pathPdf + namePdf;

            PdfReader pdfreader = new PdfReader(pathTemplate);
            try
            {
                PdfStamper pdfstamper = new PdfStamper(pdfreader, new FileStream(newFile, FileMode.Create));
                AcroFields camposPDF = pdfstamper.AcroFields;
                camposPDF.SetField("txtContractId", $"ID:{contract.GuidId}");
                camposPDF.SetField("txtDate", DateTime.Now.ToShortDateString());
                camposPDF.SetField("txtLandlord", landlordName);
                camposPDF.SetField("txtTenant", tenantName);
                camposPDF.SetField("txtPropertyId", $"REF:{_random.Next(10000, 99999).ToString()}");

                pdfstamper.Close();

            }
            catch (Exception ex)
            {
                throw;
            }

            //TODO: METER um alert bonito e fazer esta exception aqui em cima

            try
            {
                SmtpClient sc = new SmtpClient();
                MailMessage mail = new MailMessage();


                mail.From = new MailAddress("dinocinel3@gmail.com");
                mail.To.Add(new MailAddress(tenantEmail));
                mail.To.Add(new MailAddress(landlordEmail));
                mail.Subject = $"Rental4U lease contract";

                mail.IsBodyHtml = true;

                mail.Body = "<br/><br/><b>The contract is attached to this email</b><br/>" +
                    "Thank you for choosing us";

                sc.Host = "smtp.gmail.com";
                sc.Port = 587;

                Attachment att = new Attachment(newFile);
                mail.Attachments.Add(att);
                sc.Credentials = new NetworkCredential("dinocinel3@gmail.com", "dinoPass3");
                sc.EnableSsl = true;

                sc.Send(mail);

                ViewBag.Message = "Message sent!";

                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message.ToString();
            }

            return RedirectToAction("IndexContracts");
        }
    }
}
