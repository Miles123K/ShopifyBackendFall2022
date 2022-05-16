using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopifyBackendFall2022.Models;

namespace ShopifyBackendFall2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryItemsController : ControllerBase
    {
        private readonly InventoryContext _context;

        public InventoryItemsController(InventoryContext context)
        {
            _context = context;
        }

        // GET: api/InventoryItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItem>>> GetInventoryItems()
        {
            if (_context.inventoryItems == null)
            {
                return NotFound();
            }
            return await _context.inventoryItems.ToListAsync();
        }

        // GET: api/InventoryItems/findByName/name
        [HttpGet("findByName/{name}")]
        public async Task<ActionResult<IEnumerable<InventoryItem>>> GetInventoryItemsByName(string name)
        {
            if (_context.inventoryItems == null)
            {
                return NotFound();
            }

            return await _context.inventoryItems.Where(x => x.Name == name).ToListAsync();
        }

        [HttpGet("findByLocationID/{locationId}")]
        public async Task<ActionResult<IEnumerable<InventoryItem>>> GetInventoryItemsByLocation(int locationId)
        {
            if (_context.inventoryItems == null)
            {
                return NotFound();
            }

            return await _context.inventoryItems.Where(x => (x.LocationId != null) && (x.LocationId == locationId)).ToListAsync();
        }

        // GET: api/InventoryItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryItem>> GetInventoryItem(int id)
        {
            if (_context.inventoryItems == null)
            {
                return NotFound();
            }
            var inventoryItem = await _context.inventoryItems.FindAsync(id);

            if (inventoryItem == null)
            {
                return NotFound();
            }

            return inventoryItem;
        }

        // PUT: api/InventoryItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryItem(int id, InventoryItem inventoryItem)
        {
            if (id != inventoryItem.Id)
            {
                return BadRequest();
            }

            if (_context.inventoryItems == null)
            {
                return NotFound();
            }
            //_context.Entry(inventoryItem).State = EntityState.Modified;

            if (_context.locations != null && !_context.locations.Where(x => x.Id == inventoryItem.LocationId).Any())
            {
                return NotFound();
            }

            _context.inventoryItems.Update(inventoryItem);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/InventoryItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InventoryItem>> PostInventoryItem(InventoryItem inventoryItem)
        {
            if (_context.inventoryItems == null)
            {
                return Problem("Entity set 'InventoryContext.inventoryItems' is null.");
            }
            _context.inventoryItems.Add(inventoryItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInventoryItem), new { id = inventoryItem.Id }, inventoryItem);
        }

        // DELETE: api/InventoryItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryItem(int id)
        {
            if (_context.inventoryItems == null)
            {
                return NotFound();
            }
            var inventoryItem = await _context.inventoryItems.FindAsync(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            _context.inventoryItems.Remove(inventoryItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryItemExists(int id)
        {
            return (_context.inventoryItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
