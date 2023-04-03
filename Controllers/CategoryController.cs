using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoH_WebApi_DD_7194.Data;
using ProjetoH_WebApi_DD_7194.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoH_WebApi_DD_7194.Controllers
{
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
        {
            var categories = await context.Categories.AsNoTracking().ToListAsync();
            if(categories is null) return NotFound(new {message = "Nenhuma categoria cadastrada."});

            return Ok(categories);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> GetById(int id, [FromServices] DataContext context)
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (category is null) return NotFound(new { message = "Categoria não encontrada." });

            return Ok(category);
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post([FromBody] Category model, [FromServices] DataContext context)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await context.Categories.AddAsync(model);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível criar a categoria. Exceção: {ex.Message}." });
            }
           
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Category model, [FromServices] DataContext context)
        {
            if (model.Id != id)
                return NotFound(new { message = "Categoria não encontrada"} );

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = $"Os dados da categoria já foram atualizados. Exceção: {ex.Message}." });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível atualizar os dados da categoria. Exceção: {ex.Message}." });
            } 
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id, [FromServices] DataContext context)
        {
            var categoria = await context.Categories.FindAsync(id);
            if (categoria is null) return NotFound(new { message = "Categoria não encontrada" });

            try
            {
                context.Categories.Remove(categoria);
                await context.SaveChangesAsync();
                
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível remover a categoria. Exceção: {ex.Message}." });
            }            
        }
    }
}
