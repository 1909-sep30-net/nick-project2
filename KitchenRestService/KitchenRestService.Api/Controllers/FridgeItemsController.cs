using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenRestService.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitchenRestService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeItemsController : ControllerBase
    {
        private readonly Fridge _fridge;

        public FridgeItemsController(Fridge fridge)
        {
            _fridge = fridge ?? throw new ArgumentNullException(nameof(fridge));
        }

        // GET: api/FridgeItems
        [HttpGet]
        public IEnumerable<FridgeItem> Get()
        {
            // with a return type like this, ASP.NET choose status code "200" for success,
            // and serializes the return value for the response body.
            return _fridge.Items;
        }

        // GET: api/FridgeItems/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/FridgeItems
        [HttpPost]
        public ActionResult Post([FromBody, Bind("Name,Expiration")] FridgeItem item)
        {
            // we don't need this, because with [ApiController] attribute,
            // we automatically do this on every action method:
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest("all error infos"); // 400, client error
            //}

            // other validation besides data annotations, you'll need to return BadRequest manually.

            // the client can't set the ID
            var newId = _fridge.Items.Max(i => i.Id) + 1;

            item.Id = newId;

            _fridge.Items.Add(item);

            // in a response to POST, you're supposed to
            // send "201 Created" status, with a Location header indicating
            // the URL of the newly created resource, and a representation of the
            // new resource in the body.
            return CreatedAtRoute("Get", new { Id = newId }, item);
        }

        // PUT: api/FridgeItems/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FridgeItem item)
        {
            // ignore id in the body
            if (_fridge.Items.FirstOrDefault(i => i.Id == id) is FridgeItem oldItem)
            {
                if (oldItem.Expiration.Date != item.Expiration.Date)
                {
                    // 403 forbidden (not authorized to make that change)
                    return StatusCode(StatusCodes.Status403Forbidden);
                    return Forbid();
                }
                oldItem.Name = item.Name;
                return NoContent(); // 204 success and nothing is in the body
            }
            // not found (404)
            return NotFound();
        }

        // DELETE: api/FridgeItems/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_fridge.Items.FirstOrDefault(i => i.Id == id) is FridgeItem oldItem)
            {
                _fridge.Items.Remove(oldItem);
                return NoContent();
            }
            // not found (404)
            return NotFound();
        }
    }
}
