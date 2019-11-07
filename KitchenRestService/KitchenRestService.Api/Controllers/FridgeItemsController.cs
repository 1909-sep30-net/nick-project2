using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenRestService.Api.Models;
using KitchenRestService.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitchenRestService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeItemsController : ControllerBase
    {
        private readonly IKitchenRepo _kitchenRepo;
        private readonly IFridgeService _fridge;

        public FridgeItemsController(IKitchenRepo kitchenRepo, IFridgeService fridge)
        {
            _kitchenRepo = kitchenRepo ?? throw new ArgumentNullException(nameof(kitchenRepo));
            _fridge = fridge ?? throw new ArgumentNullException(nameof(fridge));
        }

        // GET: api/FridgeItems
        [HttpGet]
        public async Task<IEnumerable<ApiFridgeItem>> GetAsync()
        {
            var items = await _kitchenRepo.GetAllFridgeItemsAsync();
            return items.Select(i => new ApiFridgeItem
            {
                Id = i.Id,
                Name = i.Name,
                Expiration = i.Expiration
            });
        }

        // GET: api/FridgeItems/5
        [HttpGet("{id}")]
        public async Task<ApiFridgeItem> GetByIdAsync(int id)
        {
            var item = await _kitchenRepo.GetFridgeItemAsync(id);
            return new ApiFridgeItem
            {
                Id = item.Id,
                Name = item.Name,
                Expiration = item.Expiration
            };
        }

        // POST: api/FridgeItems
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody, Bind("Name,Expiration")] ApiFridgeItem model)
        {
            var item = new FridgeItem
            {
                Name = model.Name,
                Expiration = model.Expiration
            };
            var newItem = await _kitchenRepo.CreateFridgeItemAsync(item);
            var newModel = new ApiFridgeItem
            {
                Id = newItem.Id,
                Name = newItem.Name,
                Expiration = newItem.Expiration
            };

            // in a response to POST, you're supposed to
            // send "201 Created" status, with a Location header indicating
            // the URL of the newly created resource, and a representation of the
            // new resource in the body.
            return CreatedAtAction(nameof(GetByIdAsync), new { newModel.Id }, newModel);
        }

        // DELETE: api/FridgeItems/expired
        [HttpDelete("expired")]
        public async Task<IActionResult> DeleteExpiredAsync()
        {
            await _fridge.CleanFridgeAsync();
            return NoContent();
        }
    }
}
