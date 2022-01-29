using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TP1.API.Models;

namespace TP1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // GET: api/<CategorieController>
        [HttpGet]
        public ActionResult<IEnumerable<Categorie>> Get()
        {
            return new List<Categorie>();
        }

        // GET api/<CategorieController>/5
        [HttpGet("{id:int}")]
        public ActionResult<Categorie> Get(int id)
        {
            return new Categorie { Id = 1, Nom = "Sport" };
        }

        // POST api/<CategorieController>
        [HttpPost]
        public IActionResult Post([FromBody] Categorie categorie)
        {
            return CreatedAtAction(nameof(Get), new { id = 1 }, null);
        }
        /*
         * Réponse : 
         * Headers :
         * ....
         * Location : https://localhost:5001/api/categories/{id}
         * Body:
         *   ---
         */

        // PUT api/<CategorieController>/5
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Categorie categorie)
        {
            return NoContent();
        }

        // DELETE api/<CategorieController>/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}
