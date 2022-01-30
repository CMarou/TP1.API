using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
            return AcceptedAtAction(nameof(Status), new { id = 1 }, null);
        }

        [HttpGet("{id:int}/statut")]
        public IActionResult Status(int id)
        {
            return 1 > 0 ? Ok(new { status = "Validation en attente." }) : SeeOtherAtAction(nameof(Get), new { id });
        }

        // DELETE api/<ParticipationsController>/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }

        private IActionResult SeeOtherAtAction(string actionName, object routeValues)
        {
            Response.Headers.Add("Location", Url.Action(actionName, routeValues));
            return new StatusCodeResult(StatusCodes.Status303SeeOther);
        }
    }
}
