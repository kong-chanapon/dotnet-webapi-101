using dotnet_api.Controllers;
using dotnet_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Services
{
    public class ItemService 
    {
        private readonly ItemContext itemContext;

        public ItemService(ItemContext itemContext)
        {
            this.itemContext = itemContext;
        }

        public async Task<ActionResult<IEnumerable<Item>>> GetAll()
        {
            return await itemContext.Items.ToArrayAsync();
        }

        public async Task<ActionResult<Item>> GetById(int id)
        {
            var item = await itemContext.Items.FindAsync(id);
            
            if(item == null)
            {
                return new NotFoundResult();
            }

            return item;
        }

        public async Task<ActionResult<Item>> GetByName(string name)
        {
            var item = await itemContext.Items.FirstOrDefaultAsync(item => item.Name == name);
            
            if(item == null)
            {
                return new NotFoundResult();
            }

            return item;
        }

        public async Task<ActionResult<Item>> Add(Item item)
        {
            itemContext.Items.Add(item);
            await itemContext.SaveChangesAsync();

            return new CreatedAtActionResult(
                actionName: nameof(GetById),
                controllerName: "Items",
                routeValues: new { id = item.Id },
                value: item
            );
        }

        public async Task<ActionResult<Item>> Update(int id, Item updatedItem)
        {
            var item = await itemContext.Items.FindAsync(id);
            if (item == null)
            {
                return new NotFoundResult();
            }

            item.Name = updatedItem.Name;
            item.Description = updatedItem.Description;

            itemContext.Items.Update(item);
            await itemContext.SaveChangesAsync();
            return item;
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await itemContext.Items.FindAsync(id);
            if (item == null)
            {
                return new NotFoundResult();
            }

            itemContext.Items.Remove(item);
            await itemContext.SaveChangesAsync();

            return new NoContentResult();
        }

    }
}