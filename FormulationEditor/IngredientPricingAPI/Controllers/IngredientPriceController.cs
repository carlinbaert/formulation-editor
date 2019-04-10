using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngredientPricingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IngredientPricingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientPriceController : ControllerBase
    {
        private readonly IngredientPriceContext _context;

        public IngredientPriceController(IngredientPriceContext context)
        {
            _context = context;

            if (_context.IngredientPrices.Count() == 0)
            {
                _context.IngredientPrices.Add(new IngredientPrice { Id = 1, Price = 100 });
                _context.IngredientPrices.Add(new IngredientPrice { Id = 2, Price = 150 });
                _context.IngredientPrices.Add(new IngredientPrice { Id = 3, Price = 150 });
                _context.IngredientPrices.Add(new IngredientPrice { Id = 4, Price = 150 });
                _context.IngredientPrices.Add(new IngredientPrice { Id = 5, Price = 150 });
                _context.IngredientPrices.Add(new IngredientPrice { Id = 6, Price = 150 });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientPrice>>> GetIngredientPrices()
        {
            return await _context.IngredientPrices.ToListAsync();
        }
    }
}