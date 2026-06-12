using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuggyApp.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuggyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceContext _context;

        public InvoiceController(InvoiceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetInvoice()
        {
            // Pulling data dynamically straight from the database table!
            List<Item> items = await _context.InvoiceItems.ToListAsync();

            if (items == null || items.Count == 0)
            {
                return NotFound("No invoice items found in the database.");
            }

            return Ok(new { items });
        }

        public class Item
        {
            [System.ComponentModel.DataAnnotations.Key] 
            public int ItemID { get; set; }
            public int InvoiceID { get; set; }
            public string? Name { get; set; }
            public double Price { get; set; }
        }
    }
}