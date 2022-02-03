using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TP1.API.Interfaces;
using TP1.API.Models;

namespace TP1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvenementsController : ControllerBase
    {
        private readonly IEvenementsService _evenementsService;
        private readonly IParticipationsService _participationsService;

        public EvenementsController(IParticipationsService participationsService, IEvenementsService evenementsService)
        {
            _participationsService = participationsService;
            _evenementsService = evenementsService;
        }

        // GET: api/<EvenementsController>
        [HttpGet]
        public ActionResult<IEnumerable<Evenement>> Get()
        {
            return Ok(_evenementsService.GetList());
        }

        [HttpGet("{id:int}/participations")]
        public ActionResult<IEnumerable<Participation>> GetParticipations(int id)
        {
            var evenement = _evenementsService.GetById(id);
            if (evenement is null)
            {
                return NotFound(new { StatusCode = StatusCodes.Status404NotFound, Errors = new[] {"Évènement introuvable."}});
            }
            var participations = _participationsService.GetList(p => p.IdEvenement == evenement.Id);
            return Ok(participations);
        }

        // TODO: Faire un DTO des évenements
        // GET api/<EvenementsController>/5
        [HttpGet("{id:int}")]
        public ActionResult<Evenement> Get(int id)
        {
            var evenement = _evenementsService.GetById(id);
            if (evenement is null)
            {
                return NotFound(new { StatusCode = StatusCodes.Status404NotFound, Errors = new[] {"Évènement introuvable."}});
            }
            return evenement;
        }

        // POST api/<EvenementsController>
        [HttpPost]
        public IActionResult Post([FromBody] Evenement evenement)
        {
            var nouvelEvenement = _evenementsService.Add(evenement);

            return CreatedAtAction(nameof(Get), new { id = nouvelEvenement.Id }, null);
        }

        // PUT api/<EvenementsController>/5
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Evenement evenement)
        {
            _ = _evenementsService.Update(id, evenement);
            return NoContent();
        }

        // DELETE api/<EvenementsController>/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _evenementsService.Delete(id);
            return NoContent();
        }
    }
}
