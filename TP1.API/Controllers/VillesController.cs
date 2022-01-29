using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TP1.API.Models;

namespace TP1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillesController : ControllerBase
    {
        // GET: api/<VillesController>
        [HttpGet]
        public ActionResult<IEnumerable<Ville>> Get()
        {
            return new List<Ville>();
        }

        // GET api/<VillesController>/5
        [HttpGet("{id:int}")]
        public ActionResult<Ville> Get(int id)
        {
            return new Ville { Id = 1, Nom = "Quebec", Region = Region.CapitaleNationale };
        }

        // POST api/<VillesController>
        [HttpPost]
        public IActionResult Post([FromBody] Ville ville)
        {
            return CreatedAtAction(nameof(Get), new { id = 1 }, null);
        }

        // PUT api/<VillesController>/5
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Ville ville)
        {
            return NoContent();
        }

        // DELETE api/<VillesController>/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}
