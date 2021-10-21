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

            var user = await _userHelper.GetUserByEmailAsync("rental4u.c@gmail.com");
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

                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            //await _userHelper.AddSizeTypeAsync();
        }
    }
}
