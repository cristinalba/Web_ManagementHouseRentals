using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data.Entities;
using Web_ManagementHouseRentals.Helpers;
using Web_ManagementHouseRentals.Models;

namespace Web_ManagementHouseRentals.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IConfiguration _configuration;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IApiServiceHelper _apiServiceHelper;

        public AccountController(IUserHelper userHelper,
                                 IMailHelper mailHelper,
                                 IConfiguration configuration,
                                 IImageHelper imageHelper,
                                 IConverterHelper converterHelper,
                                 IApiServiceHelper apiServiceHelper)
        {
            _userHelper = userHelper;
            _mailHelper = mailHelper;
            _configuration = configuration;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
            _apiServiceHelper = apiServiceHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);

                var user = await _userHelper.GetUserByEmailAsync(model.Username);

                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(this.Request.Query["ReturnUrl"].First());
                    }
                    if (await _userHelper.IsUserInRoleAsync(user, "Admin")) //If Admin logs in, it goes to the dashboard!
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    return this.RedirectToAction("Index", "Home");
                }

            }

            this.ModelState.AddModelError(string.Empty, "Failed to login");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user == null)
                {
                    if (model.ZipCode.Contains("-"))
                    {
                        string str = model.ZipCode;
                        model.ZipCode = str.Remove(4, 1);
                    }

                    var responseApi = await _apiServiceHelper.GetZipCodeInfo("https://api.duminio.com", "/ptcp/v2/ptapi617149fb8434c0.60647858/", model.ZipCode);

                    if (!responseApi.IsSuccess)
                    {
                        ViewBag.MessageZipCode = "Zip Code is not valid. Please insert a valid Zip Code.";
                        return View(model);
                    }

                    List<ZipCodeHelper> temporaryZipCode = (List<ZipCodeHelper>)responseApi.Results;

                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        BirthDate = model.BirthDate,
                        PhoneNumber = model.Phone.ToString(),
                        CC = model.CC,
                        NIF = model.NIF,
                        Address = model.Address,
                        ZipCode = model.ZipCode,
                        Email = model.Username,
                        UserName = model.Username,
                        Locality = temporaryZipCode[0].Concelho,
                        Municipality = temporaryZipCode[0].Freguesia,
                        District = temporaryZipCode[0].Distrito
                    };
                    var result = await _userHelper.AddUserAsync(user, model.Password);

                    await _userHelper.AddUserToRoleAsync(user, "Customer");

                    if (result != IdentityResult.Success)
                    {
                        ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return View(model);
                    }

                    string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                    string tokenLink = Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    Response response = _mailHelper.SendEmail(model.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                        $"To allow the user, " +
                        $"please click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");

                    if (response.IsSuccess)
                    {
                        ViewBag.Message = "Sign in successful. Check your email box for further instructions.";
                        return View(model);
                    }

                    ModelState.AddModelError(string.Empty, "The user couldn't be logged.");

                }
            }
            return View(model);
        }
        public async Task<IActionResult> ChangeUser()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            var model = _converterHelper.ToChangeUserViewModel(user);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
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

        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user != null)
                {
                    var result = await _userHelper.ValidatePasswordAsync(
                        user,
                        model.Password);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(15),
                            signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return this.Created(string.Empty, results);

                    }
                }
            }

            return BadRequest();
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                //View tem de confirmar o email, vai para seu mail
                //return NotFound();
            }

            return View();
        }
        public IActionResult RecoverPassword()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
                    return View(model);
                }

                var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

                var link = this.Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);

                Response response = _mailHelper.SendEmail(model.Email, "Password Reset", $"<h1>Password Reset</h1>" +
                $"To reset the password click in this link:</br></br>" +
                $"<a href = \"{link}\">Reset Password</a>");

                if (response.IsSuccess)
                {
                    this.ViewBag.Message = "The instructions to recover your password have been sent to email.";
                }

                return this.View();

            }

            return this.View(model);
        }

        public IActionResult ResetPassword(string token)
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userHelper.GetUserByEmailAsync(model.UserName);
            if (user != null)
            {
                var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    this.ViewBag.Message = "Password reset successfully.";
                    return View();
                }

                this.ViewBag.Message = "Error while resetting the password.";
                return View(model);
            }

            this.ViewBag.Message = "User not found.";
            return View(model);
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }


    }
}
