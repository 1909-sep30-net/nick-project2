using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using KitchenRestService.Api.Models;
using KitchenRestService.Api.Services;
using KitchenRestService.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KitchenRestService.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class FridgeItemsController : ControllerBase
    {
        private readonly IKitchenRepo _kitchenRepo;
        private readonly IUserRepo _userRepo;
        private readonly IFridgeService _fridge;

        public FridgeItemsController(IKitchenRepo kitchenRepo, IUserRepo userRepo, IFridgeService fridge)
        {
            _kitchenRepo = kitchenRepo ?? throw new ArgumentNullException(nameof(kitchenRepo));
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _fridge = fridge ?? throw new ArgumentNullException(nameof(fridge));
        }

        // GET: api/FridgeItems
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<ApiFridgeItem>> GetAsync()
        {
            var items = await _kitchenRepo.GetAllFridgeItemsAsync();
            return items.Select(i => new ApiFridgeItem
            {
                Id = i.Id,
                Name = i.Name,
                Expiration = i.Expiration,
                OwnerId = i.OwnerId
            });
        }

        // GET: api/FridgeItems/5
        [HttpGet("{id}", Name = "GetFridgeItem")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ApiFridgeItem>> GetByIdAsync(int id)
        {
            var item = await _kitchenRepo.GetFridgeItemAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            return new ApiFridgeItem
            {
                Id = item.Id,
                Name = item.Name,
                Expiration = item.Expiration,
                OwnerId = item.OwnerId
            };
        }

        // POST: api/FridgeItems
        [HttpPost]
        [ProducesResponseType(typeof(ApiFridgeItem), 201)]
        public async Task<ActionResult> PostAsync(
            [FromBody, Bind("Name,Expiration")] ApiFridgeItem model,
            [FromServices] AuthInfoService authInfo)
        {
            var email = await authInfo.GetUserEmailAsync(Request);
            var user = await _userRepo.GetUserByEmailAsync(email);

            var item = new FridgeItem
            {
                Name = model.Name,
                Expiration = model.Expiration == default ? DateTime.MaxValue : model.Expiration,
                Owner = user
            };
            var newItem = await _kitchenRepo.CreateFridgeItemAsync(item);
            var newModel = new ApiFridgeItem
            {
                Id = newItem.Id,
                Name = newItem.Name,
                Expiration = newItem.Expiration,
                OwnerId = newItem.OwnerId
            };

            // in a response to POST, you're supposed to
            // send "201 Created" status, with a Location header indicating
            // the URL of the newly created resource, and a representation of the
            // new resource in the body.
            return CreatedAtRoute("GetFridgeItem", new { newModel.Id }, newModel);
        }

        // DELETE: api/FridgeItems/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] AuthInfoService authInfo)
        {
            var item = await _kitchenRepo.GetFridgeItemAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            var email = await authInfo.GetUserEmailAsync(Request);
            var user = await _userRepo.GetUserByEmailAsync(email);

            if (!user.Admin && user.Id != item.OwnerId)
            {
                return Forbid();
            }

            await _kitchenRepo.DeleteFridgeItemAsync(id);
            return NoContent();
        }

        // DELETE: api/FridgeItems/expired
        [HttpDelete("expired")]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeleteExpiredAsync([FromServices] AuthInfoService authInfo)
        {
            var email = await authInfo.GetUserEmailAsync(Request);
            var user = await _userRepo.GetUserByEmailAsync(email);
            if (!user.Admin)
            {
                return Forbid();
            }

            await _fridge.CleanFridgeAsync();
            return NoContent();
        }
    }
}
