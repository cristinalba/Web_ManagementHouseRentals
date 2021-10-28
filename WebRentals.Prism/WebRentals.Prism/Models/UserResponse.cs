﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WebRentals.Prism.Models
{
    public class UserResponse
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Id { get; set; }

        public int CC { get; set; }

        public int NIF { get; set; }

        public string ZipCode { get; set; }

        public string Locality { get; set; }

        public string Municipality { get; set; }

        public string District { get; set; }

        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        public string ImageUrl { get; set; }

        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string ConcurrencyStamp { get; set; }

        public object PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public object LockoutEnd { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }
    }
}