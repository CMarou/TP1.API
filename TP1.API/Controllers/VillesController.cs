using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TP1.API.Interfaces;
using TP1.API.Models;

namespace TP1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillesController : ControllerBase
    {
        private readonly IVillesService _villesService;
        private readonly IEvenementsService _evenementsService;

        public VillesController(IVillesService villesService, IEvenementsService evenementsService)
        {
            _villesService = villesService;
            _evenementsService = evenementsService;
        }

        // GET: api/<VillesController>
        [HttpGet]
        public ActionResult<IEnumerable<Ville>> Get()
        {
            return Ok(_villesService.GetList());
        }

        [HttpGet("{id:int}/events")]
        public ActionResult<IEnumerable<Evenement>> GetEvenements(int id)
        {
            var ville =_villesService.GetById(id);
            if (ville is null)
            {
                return NotFound(new { StatusCode = StatusCodes.Status404NotFound, Errors = new[] { "Ville introuvable." } });
            }
            
            var evenements = _evenementsService.GetList(e => e.IdVille == ville.Id);
            return Ok(evenements);
        }

        // GET api/<VillesController>/5
        [HttpGet("{id:int}")]
        public ActionResult<Ville> Get(int id)
        {
            var ville = _villesService.GetById(id);
            if (ville is null)
            {
                return NotFound(new { StatusCode = StatusCodes.Status404NotFound, Errors = new[] { "Ville introuvable." } });
            }
            return ville;
        }

        // POST api/<VillesController>
        [HttpPost]
        public IActionResult Post([FromBody] Ville ville)
        {
            var nouvelleVille = _villesService.Add(ville);
            return CreatedAtAction(nameof(Get), new { id = nouvelleVille.Id }, null);
        }

        // PUT api/<VillesController>/5
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Ville ville)
        {
            _ = _villesService.Update(id, ville); 
            return NoContent();
        }

        // DELETE api/<VillesController>/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _villesService.Delete(id);
            return NoContent();
        }
    }
}
