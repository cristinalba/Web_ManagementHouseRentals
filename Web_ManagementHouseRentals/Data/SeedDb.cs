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

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
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
                    CC = "123456789",
                    NIF = "123456789",
                    ZipCode = "1234-567",
                    Address = "Rua das Flores",
                    BirthDate = new DateTime(1993,01,23),

                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }
        }
    }
}
