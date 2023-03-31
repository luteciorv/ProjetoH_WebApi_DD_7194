using Microsoft.AspNetCore.Mvc;
using ProjetoH_WebApi_DD_7194.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoH_WebApi_DD_7194.Controllers
{
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get()
        {
            return new List<Category>();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            return new Category();
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post([FromBody] Category model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(model);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Category model)
        {
            if (model.Id != id)
                return NotFound(new { message = "Categoria não encontrada"} );

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(model);
        }

        [HttpDelete]
        [Route("")]
        public async Task<ActionResult> Delete()
        {
            return Ok();
        }
    }
}
