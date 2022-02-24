using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TP1.API.Interfaces;
using System.Net.Mime;
using TP1.API.Data.Models;

namespace TP1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class EvenementsController : ControllerBase
    {
        private readonly IEvenementsService _evenementsService;
        private readonly IParticipationsService _participationsService;

        public EvenementsController(IParticipationsService participationsService, IEvenementsService evenementsService)
        {
            _participationsService = participationsService;
            _evenementsService = evenementsService;
        }

        /// <summary>
        /// Obtiens la liste de tous les évènements.
        /// </summary>
        /// <returns>Une liste d'évènements.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Evenement>),StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Evenement>> Get()
        {
            return Ok(_evenementsService.GetList());
        }

        /// <summary>
        /// Obtiens la liste des participations pour l'évènement dont l'id
        /// est passé en paramètre.
        /// </summary>
        /// <param name="id">L'identifiant de l'évènement.</param>
        /// <returns>Une liste de participations.</returns>
        [HttpGet("{id:int}/participations")]
        [ProducesResponseType(typeof(List<Participation>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Permet l'obtention d'un seul évènement selon l'id spécifié en paramètre.
        /// </summary>
        /// <param name="id">L'identifiant de l'évènement.</param>
        /// <returns>Retourne la catégorie dont l'identifiant correspond à celui spécifié en paramètre, si il existe.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Evenement), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Evenement> Get(int id)
        {
            var evenement = _evenementsService.GetById(id);
            if (evenement is null)
            {
                return NotFound(new { StatusCode = StatusCodes.Status404NotFound, Errors = new[] {"Évènement introuvable."}});
            }
            return evenement;
        }

        /// <summary>
        /// Ajoute un nouvel évènement s'il est valide.
        /// </summary>
        /// <param name="evenement">L'évènement à ajouter.</param>
        /// <returns>La réponse HTTP de création, ainsi que le lien API permettant d'accéder à l'évènement nouvellement créé par son identifiant.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Evenement evenement)
        {
            var nouvelEvenement = _evenementsService.Add(evenement);

            return CreatedAtAction(nameof(Get), new { id = nouvelEvenement.Id }, null);
        }

        /// <summary>
        /// Permet la modification d'un évènement préexistante.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'évènement à modifier.</param>
        /// <param name="evenement">La catégorie à modifier.</param>
        /// <returns>Une réponse HTTP NoContent, si la modification est réussie.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] Evenement evenement)
        {
            _ = _evenementsService.Update(id, evenement);
            return NoContent();
        }

        /// <summary>
        /// Permet la suppression d'un évènement préexistant.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'évènement à supprimer.</param>
        /// <returns>Une réponse HTTP NoContent, si la suppression est réussie.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            _evenementsService.Delete(id);
            return NoContent();
        }
    }
}
