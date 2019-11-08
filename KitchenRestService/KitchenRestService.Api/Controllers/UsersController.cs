﻿using System;
using System.Threading.Tasks;
using KitchenRestService.Api.Models;
using KitchenRestService.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KitchenRestService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public UsersController(IUserRepo userRepo)
        {
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<ApiUser>> GetAsync(string email)
        {
            if (await _userRepo.GetUserByEmailAsync(email) is User user)
            {
                return new ApiUser
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Admin = user.Admin
                };
            }
            return NotFound();
        }

        public async Task<ActionResult> PostAsync([Bind("Email,Name")] ApiUser model)
        {
            var user = new User
            {
                Email = model.Email,
                Name = model.Name
            };
            User newUser;
            try
            {
                newUser = await _userRepo.CreateUserAsync(user);
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }
            var newModel = new ApiUser
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Email = newUser.Email,
                Admin = newUser.Admin
            };
            return CreatedAtAction(nameof(GetAsync), new { newModel.Email }, newModel);
        }
    }
}
