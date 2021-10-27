using Common.Data.Entities;
using Common.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data;
using Web_ManagementHouseRentals.Data.Entities;
using Web_ManagementHouseRentals.Helpers;
using Web_ManagementHouseRentals.Models;

namespace Web_ManagementHouseRentals.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IComboHelper _comboHelper;
        private readonly IExtraRepository _extraRepository;
        private readonly IEnergyCertificateRepository _energyCertificateRepository;
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly ISizeTypeRepository _sizeTypeRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IProperty_PhotoRepository _property_PhotoRepository;
        private readonly IProposalRepository _proposalRepository;
        private readonly IContractRepository _contractRepository;
        private readonly IApiServiceHelper _apiServiceHelper;
        private readonly IUserHelper _userHelper;

        public PropertiesController(IUserHelper userHelper,
                                    IConverterHelper converterHelper,
                                    IImageHelper imageHelper,
                                    IMailHelper mailHelper,
                                    IComboHelper comboHelper,
                                    IPropertyRepository propertyRepository,
                                    IExtraRepository extraRepository,
                                    IEnergyCertificateRepository energyCertificateRepository,
                                    IPropertyTypeRepository propertyTypeRepository,
                                    ISizeTypeRepository sizeTypeRepository,
                                    IProperty_PhotoRepository property_PhotoRepository,
                                    IProposalRepository proposalRepository,
                                    IContractRepository contractRepository,
                                    IApiServiceHelper apiServiceHelper) 

        {
            _propertyRepository = propertyRepository;
            _comboHelper = comboHelper;
            _extraRepository = extraRepository;
            _energyCertificateRepository = energyCertificateRepository;
            _propertyTypeRepository = propertyTypeRepository;
            _sizeTypeRepository = sizeTypeRepository;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
            _mailHelper = mailHelper;
            _property_PhotoRepository = property_PhotoRepository;
            _proposalRepository = proposalRepository;
            _contractRepository = contractRepository; 
            _apiServiceHelper = apiServiceHelper;
            _userHelper = userHelper;  
        }

        //[Authorize(Roles = "Admin")]
        // GET: PropertiesController
        public async Task<ActionResult> Index(int page = 1)
        {
            ShowPropertiesIndexViewModel propertiesView = new();

            propertiesView.PropertiesPerPage = 6;
            propertiesView.Properties = await _propertyRepository.GetPropertiesToIndexAsync();
            propertiesView.CurrentPage = page;

            return View(propertiesView);
        }

        //[Authorize(Roles = "Customers")]
        // GET: PropertiesController
        public async Task<IActionResult> IndexCustomers()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            
            var properties = await _propertyRepository.GetPropertiesOfCustomerAsync(user.Id);

            return View(properties);
        }

        // GET: PropertiesController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var property = await _propertyRepository.GetByIdWithInfoAsync(id.Value);

            if (property == null)
            {
                return new NotFoundViewResult("PropertyNotFound");
            }

            return View(property);
        }

        // GET: PropertiesController/Create
        public async Task<ActionResult> Create()
        {
            var model = new CreatePropertyViewModel
            {
                PropertyTypes = _comboHelper.GetComboPropertyTypes(),
                SizeTypes = _comboHelper.GetComboSizeTypes(),
                EnergyCertificates = _comboHelper.GetComboCertificate(),
                Extras = await _extraRepository.GetAllExtras(),
                Area = 5,
                MonthlyPrice = 1
            };

            return View(model);
        }

        // POST: PropertiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreatePropertyViewModel model, string[] selectedExtras)
        {
            if (ModelState.IsValid)
            {
                List<Extra> Extras = new();

                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                var energyCertificate = await _energyCertificateRepository.GetEnergyCertificateByIdAsync(model.EnergyCertificateId);
                var propertyType = await _propertyTypeRepository.GetPropertyTypeByIdAsync(model.PropertyTypeId);
                var sizeType = await _sizeTypeRepository.GetSizeTypeByIdAsync(model.SizeTypeId);

                if(selectedExtras.Length != 0)
                {
                    foreach (var item in selectedExtras)
                    {
                        int selectedExtraId = Convert.ToInt32(item);
                        var extra = await _extraRepository.GetExtraByIdAsync(selectedExtraId);
                        Extras.Add(extra);
                    }
                }

                if (model.ZipCode.Contains("-"))
                {
                    string str = model.ZipCode;
                    model.ZipCode = str.Remove(4,1);
                }

                var responseApi = await _apiServiceHelper.GetZipCodeInfo("https://api.duminio.com", "/ptcp/v2/ptapi617149fb8434c0.60647858/", model.ZipCode);

                if (!responseApi.IsSuccess)
                {
                    ViewBag.MessageZipCode = "Zip Code is not valid. Please insert a valid Zip Code.";

                    model.PropertyTypes = _comboHelper.GetComboPropertyTypes();
                    model.SizeTypes = _comboHelper.GetComboSizeTypes();
                    model.EnergyCertificates = _comboHelper.GetComboCertificate();
                    model.Extras = await _extraRepository.GetAllExtras();
                    model.Area = 5;
                    model.MonthlyPrice = 1;

                    return View(model);

                }

                //var ZipCodeResult = responseApi.Results;

                List<ZipCodeHelper> temporaryZipCode = (List<ZipCodeHelper>)responseApi.Results;

                var property = _converterHelper.ToProperty(model, Extras, energyCertificate, propertyType, sizeType, user, temporaryZipCode);
                await _propertyRepository.CreateAsync(property);

                var path = string.Empty;

                if (model.ImagesFiles != null && model.ImagesFiles.Count > 0)
                {
                    foreach (var imageFile in model.ImagesFiles)
                    {
                        path = await _imageHelper.UploadImageAsync(imageFile, "property");

                        var propertyPhoto = new Property_Photo
                        {
                            Property = property,
                            ImageUrl = path,
                        };
                        await _property_PhotoRepository.CreateAsync(propertyPhoto);
                    }
                }

                model.PropertyTypes = _comboHelper.GetComboPropertyTypes();
                model.SizeTypes = _comboHelper.GetComboSizeTypes();
                model.EnergyCertificates = _comboHelper.GetComboCertificate();
                model.Extras = await _extraRepository.GetAllExtras();
                model.Area = 5;
                model.MonthlyPrice = 1;

                ViewBag.Message = "Your Property as been submitted and is now being reviewed";
                return View(model);
            }

            return View(model);
        }

        // GET: PropertiesController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("PropertyNotFound");
            }

            var model = new EditPropertyViewModel();
            var property = await _propertyRepository.GetByIdAsync(id.Value);
            if(property == null)
            {
                return new NotFoundViewResult("PropertyNotFound");
            }
            model.Id = property.Id;
            model.MonthlyPrice = property.MonthlyPrice;
            return View(model);
        }

        // POST: PropertiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EditPropertyViewModel model)
        {
            if (id != model.Id)
            {
                return new NotFoundViewResult("PropertyNotFound");
            }

            try
            {
                var property = await _propertyRepository.GetPropertyByIdAsync(model.Id);

                if(property != null)
                {
                    property.MonthlyPrice = model.MonthlyPrice;
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
            return RedirectToAction(nameof(Index));

        }

        // GET: PropertiesController/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var property = await _propertyRepository.GetByIdAsync(id);

            try
            {
                await _propertyRepository.DeleteAsync(property);
                return RedirectToAction(nameof(IndexCustomers));
            }
            catch(DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle =   "The property might be in used</br>";
                    ViewBag.ErrorMessage = "It is not possible to deleted because it has other information associated!</br></br>" +
                                           "Try to delete first that information and then come back to delete the property!";
                }
                return View("Error");
            }
        }


        public async Task<IActionResult> SendProposal(int? id)
        {
            var property = await _propertyRepository.GetByIdWithInfoAsync(id.Value);
            var model = new CreateProposalViewModel
            {
                PropertyId = id.Value,
                OwnerName = property.Owner.FullName,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendProposal(CreateProposalViewModel model)
        {
            var client = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var property = await _propertyRepository.GetByIdWithInfoAsync(model.PropertyId);
            var owner = await _userHelper.GetUserByEmailAsync(property.Owner.Email);
            var proposalState =  _proposalRepository.GetProposalStates(5);

            if (ModelState.IsValid)
            {
                var proposal =_converterHelper.ToProposalAsync(model, client, owner, property, proposalState);
                await _proposalRepository.CreateAsync(proposal);
                ViewBag.Message = "Message sent successfully!";
                Response response = _mailHelper
                        .SendEmail(property.Owner.Email, "A potencial tenant from Rental4U", $"<h3>Check your proposals</h3>" +
                        $"<h5>You have a message. Someone is interested in your property in {property.Address}</h5> Thank you for choosing us");
                model.Message = "";
                model.Name = "";
                model.Email = "";
                return View(model);
            }

            return View(model);
        }

        public IActionResult Proposals()
        {
            var proposals = _proposalRepository.GetProposalsFromUser(this.User.Identity.Name).OrderByDescending(p => p.ProposalDate);

            return View(proposals);
        }

        public async Task<IActionResult> ProposalDetails(int? id)
        {
            var proposal = await _proposalRepository.GetProposalByIdAsync(id.Value);
            if (proposal == null)
            {
                return NotFound();
            }

            if (proposal.proposalState != _proposalRepository.GetProposalStates(2) || proposal.proposalState != _proposalRepository.GetProposalStates(1))
            {
                proposal.proposalState = _proposalRepository.GetProposalStates(3);

                await _proposalRepository.UpdateAsync(proposal);
            }

            var model = new EditProposalViewModel
            {
                Id = proposal.Id,
                OwnerId = proposal.Owner.Id,
                ClientId = proposal.Client.Id,
                ClientName = proposal.Client.FullName,
                PropertyId = proposal.property.Id,
                ProposalStateId = proposal.proposalState.Id,
                ProposalStates = _comboHelper.GetComboProposalStates(),
                Message = proposal.Message,
                ProposalDate = proposal.ProposalDate,
                propertyOwner = proposal.property.Owner.Email,
                loggedUser = this.User.Identity.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProposalDetails(EditProposalViewModel model)
        {
            var client = await _userHelper.GetUserByIdAsync(model.ClientId);
            var property = await _propertyRepository.GetByIdWithInfoAsync(model.PropertyId);
            var owner = await _userHelper.GetUserByIdAsync(model.OwnerId);
            var proposalState = _proposalRepository.GetProposalStates(model.ProposalStateId);

            if (ModelState.IsValid)
            {
                var proposalResponse = _converterHelper.ToResponseProposalAsync(model, client, owner, property, proposalState);

                await _proposalRepository.CreateAsync(proposalResponse);
                ViewBag.Message = "Message sent successfully!";

                Response response = _mailHelper
                        .SendEmail(client.Email, "Message from a landlord of Rental4U", $"<h3>Check your proposals</h3>" +
                        $"<h5>You have a message. The landlord of the property in {property.Address} contacted you.</h5> Thank you for choosing us");

                return View(model);
            }

            return View(model);

        }

        public async Task<IActionResult> ProposalAccepted(int? id)
        {
           
            var proposal = await _proposalRepository.GetProposalByIdAsync(id.Value);

            proposal.proposalState = _proposalRepository.GetProposalStates(2);

            var proposalToClient = new Proposal
            {
                property = proposal.property,
                proposalState = _proposalRepository.GetProposalStates(1),
                Message = "Proposal accepted! The admin will create the contract and send it to you email.",
                ProposalDate = proposal.ProposalDate,
                Owner = proposal.Client,
                Client = proposal.Owner
            };

            await _proposalRepository.CreateAsync(proposalToClient);

            await _proposalRepository.UpdateAsync(proposal);

            Response response = _mailHelper
                       .SendEmail(proposal.Client.Email, "Message from Rental4U", $"<h3>Check your proposals</h3>" +
                       $"<h2>Your proposal has been accepted by the landlord of the property in {proposal.property.Address}</h2> Thank you for choosing us");

            return RedirectToAction("Proposals");

        }

        public async Task<IActionResult> ProposalRejected(int? id)
        {
            var proposal = await _proposalRepository.GetProposalByIdAsync(id.Value);

            proposal.proposalState = _proposalRepository.GetProposalStates(4);

            var proposalToClient = new Proposal
            {
                property = proposal.property,
                proposalState = proposal.proposalState,
                Message = "Proposal Rejected!",
                ProposalDate = proposal.ProposalDate,
                Owner = proposal.Client,
                Client = proposal.Owner
            };

            await _proposalRepository.UpdateAsync(proposal);
            
            Response response = _mailHelper
                       .SendEmail(proposal.Client.Email, "Message from Rental4U", $"<h3>Check your proposals</h3>" +
                       $"<h2>Your proposal has been rejected by the landlord of the property in {proposal.property.Address}</h2> Thank you for choosing us");

            return RedirectToAction("Proposals");

        }

        public async Task<IActionResult> DeleteProposal(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProposalNotFound");
            }

            var proposal = await _proposalRepository.GetByIdAsync(id.Value);

            try
            {
                await _proposalRepository.DeleteAsync(proposal);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Proposals");
        }

        public IActionResult IndexContracts()
        {         
            var contracts = _contractRepository.GetContractsOfUserAsync(this.User.Identity.Name);

            return View(contracts);
        }


        public IActionResult PropertyNotFound()
        {
            return View();
        }
    }
}