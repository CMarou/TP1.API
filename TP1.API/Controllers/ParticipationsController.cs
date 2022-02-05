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

        /// <summary>
        /// Obtient la liste complète de toutes les participations.
        /// </summary>
        /// <returns>Retourne la liste complète des participations.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Participation>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Participation>> Get()
        {
            return Ok(_participationsService.GetList());
        }

        /// <summary>
        /// Permet l'obtention d'une seule participation selon l'id spécifié en paramètre.
        /// </summary>
        /// <param name="id">L'identifiant unique assigné à une participation.</param>
        /// <returns>
        /// Retourne la prticipation dont l'identifiant correspond à celui spécifié en paramètre, si elle existe. 
        /// </returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Participation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Participation> Get(int id)
        {
            var participation = _participationsService.GetById(id);
            if (participation is null)
            {
                return NotFound(new {StatusCode = StatusCodes.Status404NotFound, Errors = new[] { "Participation introuvable." }});
            }
            return participation;
        }

        /// <summary>
        /// Permet la création d'une nouvelle participation.
        /// </summary>
        /// <param name="categorie">La participation à ajouter.</param>
        /// <returns>
        /// La réponse HTTP de création, ainsi que le lien API permettant d'accéder à la participation nouvellement créée par son identifiant.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Participation participation)
        {
            var nouvelleParticipation = _participationsService.Add(participation);
            return AcceptedAtAction(nameof(Status), new { id = nouvelleParticipation.Id }, null);
        }

        /// <summary>
        /// Permet la modification d'une participation préexistante.
        /// </summary>
        /// <param name="id">L'identifiant unique de la participation à modification.</param>
        /// <returns>Une réponse HTTP NoContent, si la modification est réussie.</returns>
        [HttpGet("{id:int}/statut")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status303SeeOther)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Status(int id)
        {
            var participation = _participationsService.GetById(id);

            _validation.Validate(participation);

            if (participation.EstValide)
                return SeeOtherAtAction(nameof(Get), new { id });
           
            return Ok(new { status = "Validation en attente." });
        }

        /// <summary>
        /// Permet la suppression d'une participation préexistante.
        /// </summary>
        /// <param name="id">L'identifiant unique de la participation à supprimer.</param>
        /// <param name="categorie">La participation à supprimer.</param>
        /// <returns>Une réponse HTTP NoContent, si la suppression est réussie.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
