using CQRSandMediatorWebApi.Data;
using CQRSandMediatorWebApi.Features.Items.CreateItem;
using CQRSandMediatorWebApi.Features.Items.GetItemByID;
using CQRSandMediatorWebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CQRSandMediatorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        //after adding mediator pattern, no need to include AppDbContext _context and other related dependencies here, since we've moved them into handler classes
        private readonly AppDbContext _context;
        //required for mediatr and cqrs implementation
        private readonly ISender _sender;
        public ItemsController(AppDbContext context, ISender sender)
        {
            _context = context;
            _sender = sender;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        //get itemById API
        //before cqrs and mediator pattern implementation
        [HttpGet("without-mediator/{id}")]
        public async Task<ActionResult<Item>> GetItemWithoutMediator(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        //get itemById API
        //after adding cqrs and mediator pattern implementation
        [HttpGet("with-mediator/{id}")]
        public async Task<ActionResult<Item>> GetItemWithMediator(Guid id)
        {
            var item= new GetItemByIDQuery(id);
            var itemResponse = _sender.Send(item);
            if (itemResponse == null)
            {
                return NotFound();
            }
            return Ok(itemResponse);
        }

        //create item API
        //before cqrs and mediator pattern implementation
        [HttpPost("without-mediator")]
        public async Task<ActionResult<Item>> AddItemWithoutMediator(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        //create item API
        //after adding cqrs and mediator pattern implementation
        [HttpPost("with-mediator")]
        public async Task<ActionResult<Item>> AddItemWithMediator(CreateItemCommand createItemCommand)
        {
            var itemId = _sender.Send(createItemCommand);
            return Ok(itemId);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(Guid id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(Guid id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
