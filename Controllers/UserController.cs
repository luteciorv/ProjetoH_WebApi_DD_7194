using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoH_WebApi_DD_7194.Data;
using ProjetoH_WebApi_DD_7194.Models;
using ProjetoH_WebApi_DD_7194.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoH_WebApi_DD_7194.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<User>> Post([FromBody] User model, [FromServices] DataContext context)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await context.Users.AddAsync(model);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível criar o usuário. Exceção: {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] User model, [FromServices] DataContext context)
        {
            var user = await context.Users.AsNoTracking().Where(u => u.Username == model.Username && u.Password == model.Password).FirstOrDefaultAsync();
           
            if (user is null) 
                return NotFound(new { message = "Usuário ou senha inválidos!" });

            var token = TokenService.GenerateToken(user);
            return new
            {
                User = user,
                Token = token
            };
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] User model, [FromServices] DataContext context)
        {
            if (id != model.Id) 
                return NotFound(new { message = "Usuário não encontrado" });
            
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);            

            try
            {
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = $"O usuário já foi atualizado. Exceção: {ex.Message}" });
            }
            catch(Exception ex)
            {
                return BadRequest(new {message = $"Não foi possível atualizar os dados do usuário. Exceção: {ex.Message}"});
            }
        }
    }
}
