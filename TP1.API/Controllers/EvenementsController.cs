using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TP1.API.Models;

namespace TP1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvenementsController : ControllerBase
    {
        // GET: api/<EvenementsController>
        [HttpGet]
        public ActionResult<IEnumerable<Evenement>> Get()
        {
            return new List<Evenement>();
        }

        // TODO: Faire un DTO des évenements
        // GET api/<EvenementsController>/5
        [HttpGet("{id:int}")]
        public ActionResult<Evenement> Get(int id)
        {
            return new Evenement { 
                Id = 1, 
                Titre = "Match Hockey", 
                AdresseCivique = "123 rue de la Patinoire", 
                DateDebut = DateTime.Now,
                DateFin = DateTime.Now.AddHours(2),
                Description = "Match de hockey Pinguin vs Starfish.",
                NomOrganisateur = "Bob Lachance",
                IdVille = 1
            };
        }

        // POST api/<EvenementsController>
        [HttpPost]
        public IActionResult Post([FromBody] Evenement evenement)
        {
            return CreatedAtAction(nameof(Get), new { id = 1 }, null);
        }

        // PUT api/<EvenementsController>/5
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Evenement evenement)
        {
            return NoContent();
        }

        // DELETE api/<EvenementsController>/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}
