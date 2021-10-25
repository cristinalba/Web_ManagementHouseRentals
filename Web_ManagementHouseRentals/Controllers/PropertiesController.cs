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
        private readonly IProperty_PhotoRepository _property_PhotoRepository;
        private readonly IProposalRepository _proposalRepository;
        private readonly IProposalHelper _proposalHelper;
        private readonly IUserHelper _userHelper;

        public PropertiesController(IUserHelper userHelper,
                                    IPropertyRepository propertyRepository, 
                                    IComboHelper comboHelper,
                                    IExtraRepository extraRepository,
                                    IEnergyCertificateRepository energyCertificateRepository,
                                    IPropertyTypeRepository propertyTypeRepository,
                                    ISizeTypeRepository sizeTypeRepository,
                                    IConverterHelper converterHelper,
                                    IImageHelper imageHelper,
                                    IProperty_PhotoRepository property_PhotoRepository,
                                    IProposalRepository proposalRepository,
                                    IProposalHelper proposalHelper)
        {
            _propertyRepository = propertyRepository;
            _comboHelper = comboHelper;
            _extraRepository = extraRepository;
            _energyCertificateRepository = energyCertificateRepository;
            _propertyTypeRepository = propertyTypeRepository;
            _sizeTypeRepository = sizeTypeRepository;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
            _property_PhotoRepository = property_PhotoRepository;
            _proposalRepository = proposalRepository;
            _proposalHelper = proposalHelper;
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

                var property = _converterHelper.ToProperty(model, Extras, energyCertificate, propertyType, sizeType, user);
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
            }

            return RedirectToAction(nameof(IndexCustomers));
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
                return View();
            }
        }


        ////CREATE PROPOSAL
        //[HttpPost]
        //public async Task<IActionResult> Details(PropertyDetailsViewModel model)
        //{
        //    var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
        //    var proposalState =  _proposalRepository.GetProposalStates(1);
        //    var property = await _propertyRepository.GetByIdWithInfoAsync(model.Id);

        //    var newModel = new PropertyDetailsViewModel
        //    {
        //        Id = property.Id,
        //        Type = property.Type,
        //        Owner = property.Owner,
        //        NameProperty = property.NameProperty,
        //        Description = property.Description,
        //        SizeType = property.SizeType,
        //        Address = property.Address,
        //        ZipCode = property.ZipCode,
        //        Extra = property.Extra,
        //        Area = property.Area,
        //        Latitude = property.Latitude,
        //        Longitude = property.Longitude,
        //        EnergyCertificate = property.EnergyCertificate,
        //        AvailableProperty = property.AvailableProperty,
        //        MonthlyPrice = property.MonthlyPrice,
        //        PropertyPhotos = property.PropertyPhotos,
        //        Property = property,
        //    };

        //    if (ModelState.IsValid)
        //    {
        //        var proposal = _proposalHelper.CreateProposalAsync(newModel, user, proposalState);
        //        await _proposalRepository.CreateAsync(proposal);
        //        ViewBag.Message = "Message sent successfully!";
        //        return View(newModel);
        //    }

        //    return View(newModel);
        //}

        public IActionResult PropertyNotFound()
        {
            return View();
        }
    }
}
