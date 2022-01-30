using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TP1.API.Interfaces;
using TP1.API.Models;

namespace TP1.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ParticipationsController : ControllerBase
    {
        private readonly IParticipationsService _participationsService;
        private readonly IValidationParticipation _validation;

        public ParticipationsController(IParticipationsService participationsService, IValidationParticipation validation)
        {
            _participationsService = participationsService;
            _validation = validation;
        }

        // GET: api/<ParticipationsController>
        [HttpGet]
        public ActionResult<IEnumerable<Participation>> Get()
        {
            return Ok(_participationsService.GetList());
        }

        // GET api/<ParticipationsController>/5
        [HttpGet("{id:int}")]
        public ActionResult<Participation> Get(int id)
        {
            var participation = _participationsService.GetById(id);
            return participation;
        }

        // POST api/<ParticipationsController>
        [HttpPost]
        public IActionResult Post([FromBody] Participation participation)
        {
            var nouvelleParticipation = _participationsService.Add(participation);
            return AcceptedAtAction(nameof(Status), new { id = nouvelleParticipation.Id }, null);
        }

        [HttpGet("{id:int}/statut")]
        public IActionResult Status(int id)
        {
            var participation = _participationsService.GetById(id);

            _validation.Validate(participation);

            if (participation.EstValide)
                return SeeOtherAtAction(nameof(Get), new { id });
           
            return Ok(new { status = "Validation en attente." });
        }

        // DELETE api/<ParticipationsController>/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _participationsService.Delete(id);
            return NoContent();
        }

        private IActionResult SeeOtherAtAction(string actionName, object routeValues)
        {
            Response.Headers.Add("Location", Url.Action(actionName, routeValues));
            return new StatusCodeResult(StatusCodes.Status303SeeOther);
        }
    }
}
