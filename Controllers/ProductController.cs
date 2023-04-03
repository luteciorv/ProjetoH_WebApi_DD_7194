using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoH_WebApi_DD_7194.Data;
using ProjetoH_WebApi_DD_7194.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoH_WebApi_DD_7194.Controllers
{
    [Route("Products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            var products = await context.Products.Include(p => p.Category).AsNoTracking().ToListAsync();
            if (products.Count == 0) return NotFound(new { message = "Nenhum produto cadastrado." });

            return Ok(products);
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> GetById(int id, [FromServices] DataContext context)
        {
            var product = await context.Products.Include(p => p.Category).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (product is null) return NotFound(new { message = "Produto não encontrado" });

            return Ok(product);
        }

        [HttpGet]
        [Route("categories/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> GetByCategory(int id, [FromServices] DataContext context)
        {
            var products = await context.Products.Include(p => p.Category).AsTracking().Where(p => p.CategoryId == id).ToListAsync();
            if (products.Count == 0) return NotFound(new { message = "Nenhum produto correspondente a categoria foi encontrado" });

            return Ok(products);
        }

        [HttpPost]
        [Authorize(Roles = "employee,manager")]
        [Route("")]
        public async Task<ActionResult<Product>> Post([FromBody] Product product, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();

                return Ok(product);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível criar o Produto. Exceção: {ex.Message}." });
            }
        }
    }
}
