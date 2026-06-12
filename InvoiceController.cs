
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BuggyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetInvoice()
        {
            List<Item> items = new List<Item>
            {
                new Item { Name = "Widget A", Price = 19.99 }
            };

            if (items == null || items.Count == 0) 
            {
                return NotFound("No invoice found");
            }

            return Ok(new { items });
        }

        public class Item
        {
            public string Name { get; set; }
            public double Price { get; set; }
        }
    }
}
