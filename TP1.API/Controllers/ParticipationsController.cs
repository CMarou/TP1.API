using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TP1.API.Models;

namespace TP1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipationsController : ControllerBase
    {
        // GET: api/<ParticipationsController>
        [HttpGet]
        public ActionResult<IEnumerable<Participation>> Get()
        {
            return new List<Participation>();
        }

        // GET api/<ParticipationsController>/5
        [HttpGet("{id:int}")]
        public ActionResult<Participation> Get(int id)
        {
            return new Participation 
            { 
                Id = 1,
                Nom = "Latruite",
                Prenom = "Kevin",
                AdresseCourriel = "latruite@ocean.com",
                NombrePlace = 1
            };
        }

        // POST api/<ParticipationsController>
        [HttpPost]
        public IActionResult Post([FromBody] Participation participation)
        {
            return CreatedAtAction(nameof(Get), new { id = 1 }, null);
        }

        // PUT api/<ParticipationsController>/5
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Participation participation)
        {
            return NoContent();
        }

        // DELETE api/<ParticipationsController>/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}
