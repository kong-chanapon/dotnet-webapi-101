using dotnet_api.Models;
using dotnet_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemService itemService;

        public ItemsController(ItemService itemService)
        {
            this.itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAll()
        {
            return await itemService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetById(int id)
        {
            return await itemService.GetById(id);
        }

        [HttpGet("search")]
        public async Task<ActionResult<Item>> GetByName([FromQuery] string name)
        {
            return await itemService.GetByName(name);
        }

        [HttpPost]
        public async Task<ActionResult<Item>> Add(Item item)
        {
            return await itemService.Add(item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Item>> Update(int id, Item updatedItem)
        {
            return await itemService.Update(id, updatedItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await itemService.Delete(id);
        }
    }
}
