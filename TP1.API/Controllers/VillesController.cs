using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TP1.API.Interfaces;
using TP1.API.Models;

namespace TP1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillesController : ControllerBase
    {
        private readonly IVillesService _villesService;

        public VillesController(IVillesService villesService)
        {
            _villesService = villesService;
        }

        // GET: api/<VillesController>
        [HttpGet]
        public ActionResult<IEnumerable<Ville>> Get()
        {
            return Ok(_villesService.GetList());
        }

        // GET api/<VillesController>/5
        [HttpGet("{id:int}")]
        public ActionResult<Ville> Get(int id)
        {
            var ville = _villesService.GetById(id);
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
