using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data.Entities;
using Web_ManagementHouseRentals.Helpers;

namespace Web_ManagementHouseRentals.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Worker");
            await _userHelper.CheckRoleAsync("Customer");

            //Create Roles
            var user = await _userHelper.GetUserByEmailAsync("rental4u.c@gmail.com");
            var userWorker = await _userHelper.GetUserByEmailAsync("worker@yopmail.com");
            var userLandlord = await _userHelper.GetUserByEmailAsync("landlord@yopmail.com");
            var userTenant = await _userHelper.GetUserByEmailAsync("tenant@yopmail.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Zena",
                    LastName = "Antunes",
                    Email = "rental4u.c@gmail.com",
                    UserName = "rental4u.c@gmail.com",
                    CC = _random.Next(10000000, 59999999).ToString(),
                    NIF = _random.Next(100000000, 599999999).ToString(),
                    ZipCode = $"{_random.Next(1000, 9999).ToString()}-{_random.Next(100, 999).ToString()}",
                    Address = "Rua das Flores",
                    BirthDate = new DateTime(_random.Next(1980, 2000), _random.Next(1, 12), _random.Next(1, 27)),

                };
                userWorker = new User
                {
                    FirstName = "Jonh",
                    LastName = "Smith",
                    Email = "worker@yopmail.com",
                    UserName = "worker@yopmail.com",
                    CC = _random.Next(10000000, 59999999).ToString(),
                    NIF = _random.Next(100000000, 599999999).ToString(),
                    ZipCode = $"{_random.Next(1000, 9999).ToString()}-{_random.Next(100, 999).ToString()}",
                    Address = "Rua X",
                    BirthDate = new DateTime(_random.Next(1980, 2000), _random.Next(1, 12), _random.Next(1, 27)),

                };
                userLandlord = new User
                {
                    FirstName = "Lola",
                    LastName = "Flores",
                    Email = "landlord@yopmail.com",
                    UserName = "landlord@yopmail.com",
                    CC = _random.Next(10000000, 59999999).ToString(),
                    NIF = _random.Next(100000000, 599999999).ToString(),
                    ZipCode = $"{_random.Next(1000, 9999).ToString()}-{_random.Next(100, 999).ToString()}",
                    Address = "Rua Y",
                    BirthDate = new DateTime(_random.Next(1980, 2000), _random.Next(1, 12), _random.Next(1, 27)),

                };
                userTenant = new User
                {
                    FirstName = "Amalia",
                    LastName = "Rodrigues",
                    Email = "tenant@yopmail.com",
                    UserName = "tenant@yopmail.com",
                    CC = _random.Next(10000000, 59999999).ToString(),
                    NIF = _random.Next(100000000, 599999999).ToString(),
                    ZipCode = $"{_random.Next(1000, 9999).ToString()}-{_random.Next(100, 999).ToString()}",
                    Address = "Rua Z",
                    BirthDate = new DateTime(_random.Next(1980, 2000), _random.Next(1, 12), _random.Next(1, 27)),

                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                var resultWorker = await _userHelper.AddUserAsync(userWorker, "123456");
                var resultLandlord = await _userHelper.AddUserAsync(userLandlord, "123456");
                var resultTenant = await _userHelper.AddUserAsync(userTenant, "123456");

                if (result != IdentityResult.Success || resultWorker != IdentityResult.Success || resultLandlord != IdentityResult.Success || resultTenant != IdentityResult.Success)
                {
                    throw new InvalidOperationException("It wasn´t possible to create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
                await _userHelper.AddUserToRoleAsync(userWorker, "Worker");
                await _userHelper.AddUserToRoleAsync(userLandlord, "Customer");
                await _userHelper.AddUserToRoleAsync(userTenant, "Customer");

                //var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                //await _userHelper.ConfirmEmailAsync(user, token);
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }
            var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            await _userHelper.ConfirmEmailAsync(user, token);

            var isInRoleWorker = await _userHelper.IsUserInRoleAsync(userWorker, "Worker");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Worker");
            }
            var token1 = await _userHelper.GenerateEmailConfirmationTokenAsync(userWorker);
            await _userHelper.ConfirmEmailAsync(userWorker, token1);

            var isInRoleLandlord = await _userHelper.IsUserInRoleAsync(userLandlord, "Customer");
            if (!isInRoleLandlord)
            {
                await _userHelper.AddUserToRoleAsync(userLandlord, "Customer");
            }
            var token2 = await _userHelper.GenerateEmailConfirmationTokenAsync(userLandlord);
            await _userHelper.ConfirmEmailAsync(userLandlord, token2);

            var isInRoleTenant = await _userHelper.IsUserInRoleAsync(userTenant, "Customer");
            if (!isInRoleTenant)
            {
                await _userHelper.AddUserToRoleAsync(userTenant, "Customer");
            }
            var token3 = await _userHelper.GenerateEmailConfirmationTokenAsync(userTenant);
            await _userHelper.ConfirmEmailAsync(userTenant, token3);

            if (!_context.EnergyCertificates.Any())
            {
                addEnergyCertificate("A+ - 0,9%");
                addEnergyCertificate("A - 3,8%");
                addEnergyCertificate("B - 6,7%");
                addEnergyCertificate("B- - 9,9%");
                addEnergyCertificate("C - 32,2%");
                addEnergyCertificate("D - 26,6%");
                addEnergyCertificate("E - 14,1%");
                addEnergyCertificate("F - 5,8%");
                await _context.SaveChangesAsync();
            }

            if (!_context.PropertyTypes.Any())
            {
                addPropertyType("Apartment");
                addPropertyType("Home");
                addPropertyType("Bedroom");
                addPropertyType("Land");
                addPropertyType("Store");
                addPropertyType("Storage");
                addPropertyType("Building");
                addPropertyType("Office");
                addPropertyType("Garage");
                await _context.SaveChangesAsync();
            }

            if (!_context.SizeTypes.Any())
            {
                addSizeType("T0");
                addSizeType("T1");
                addSizeType("T2");
                addSizeType("T3");
                addSizeType("T4");
                addSizeType("T5");
                addSizeType("T6");
                addSizeType("T7");
                addSizeType("T8 or higher");
                await _context.SaveChangesAsync();
            }

            if (!_context.Extras.Any())
            {
                addExtra("Swimmingpool");
                addExtra("Garage");
                addExtra("Elevator");
                addExtra("Air conditioning");
                addExtra("Backyard");
                await _context.SaveChangesAsync();
            }
        }

        private void addExtra(string extra)
        {
            _context.Extras.Add(new Extra
            {
                Name = extra,
            });
        }

        private void addSizeType(string type)
        {
            _context.SizeTypes.Add(new SizeType
            {
                Name = type,
            });
        }

        private void addPropertyType(string type)
        {
            _context.PropertyTypes.Add(new PropertyType
            {
                Name = type,
            });
        }

        private void addEnergyCertificate(string type)
        {
            _context.EnergyCertificates.Add(new EnergyCertificate
            {
                Name = type,
            });
        }
    }
}
