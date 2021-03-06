﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KitchenRestService.Logic;
using Microsoft.EntityFrameworkCore;

namespace KitchenRestService.Data
{
    public class DataSeeder : IDataSeeder
    {
        private readonly KitchenContext _context;

        public DataSeeder(KitchenContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> SeedDataAsync()
        {
            if (!await _context.Database.CanConnectAsync())
            {
                await _context.Database.EnsureCreatedAsync();
            }

            var user = new User
            {
                Email = "nicholasescalona@outlook.com",
                Name = "Nicholas Escalona",
                Admin = true
            };
            user.FridgeItems.UnionWith(new List<FridgeItem> {
                new FridgeItem { Name = "coffee", Expiration = new DateTime(2019, 12, 12) },
                new FridgeItem { Name = "mushrooms", Expiration = new DateTime(2019, 12, 12) },
                new FridgeItem { Name = "mold", Expiration = new DateTime(2018, 12, 12) }, // expired
                new FridgeItem { Name = "milk", Expiration = new DateTime(2019, 10, 1) } // expired
            });
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                return false;
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
