using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TP1.API.Data.Models;
using TP1.API.Interfaces;

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

        /// <summary>
        /// Obtient la liste complète de toutes les villes.
        /// </summary>
        /// <returns>Retourne la liste complète des villes.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Ville>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Ville>> Get()
        {
            return Ok(_villesService.GetList());
        }

        /// <summary>
        /// Permet l'obtention des évènements selon l'id d'une ville spécifié en paramètre.
        /// </summary>
        /// <param name="id">L'identifiant unique assigné à une ville.</param>
        /// <returns>
        /// Retourne la liste des évènements associés à la ville dont l'identifiant correspond à celui spécifié en paramètre, si elle existe. 
        /// </returns>
        [HttpGet("{id:int}/events")]
        [ProducesResponseType(typeof(List<Evenement>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Permet l'obtention d'une seule ville selon l'id spécifié en paramètre.
        /// </summary>
        /// <param name="id">L'identifiant unique assigné à une ville.</param>
        /// <returns>
        /// Retourne la ville dont l'identifiant correspond à celui spécifié en paramètre, si elle existe. 
        /// </returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Ville), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Ville> Get(int id)
        {
            var ville = _villesService.GetById(id);
            if (ville is null)
            {
                return NotFound(new { StatusCode = StatusCodes.Status404NotFound, Errors = new[] { "Ville introuvable." } });
            }
            return ville;
        }

        /// <summary>
        /// Permet la création d'une nouvelle ville.
        /// </summary>
        /// <param name="categorie">La ville à ajouter.</param>
        /// <returns>
        /// La réponse HTTP de création, ainsi que le lien API permettant d'accéder à la ville nouvellement créée par son identifiant.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Ville ville)
        {
            var nouvelleVille = _villesService.Add(ville);
            return CreatedAtAction(nameof(Get), new { id = nouvelleVille.Id }, null);
        }

        /// <summary>
        /// Permet la modification d'une ville préexistante.
        /// </summary>
        /// <param name="id">L'identifiant unique de la ville à modification.</param>
        /// <returns>Une réponse HTTP NoContent, si la modification est réussie.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] Ville ville)
        {
            _ = _villesService.Update(id, ville); 
            return NoContent();
        }

        /// <summary>
        /// Permet la suppression d'une ville préexistante.
        /// </summary>
        /// <param name="id">L'identifiant unique de la ville à supprimer.</param>
        /// <param name="categorie">La ville à supprimer.</param>
        /// <returns>Une réponse HTTP NoContent, si la suppression est réussie.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            _villesService.Delete(id);
            return NoContent();
        }
    }
}
