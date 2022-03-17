using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TP1.API.Interfaces;
using System.Net.Mime;
using TP1.API.Data.Models;
using TP1.API.DTOs;

namespace TP1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        /// <summary>
        /// Obtiens la liste complète de toutes les catégories.
        /// </summary>
        /// <returns>Retourne la liste complète des catégories.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<RequeteCategorieDto>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<RequeteCategorieDto>> Get()
        {
            return Ok(_categoriesService.GetList());
        }

        /// <summary>
        /// Permet l'obtention d'une seule catégorie selon l'id spécifié en paramètre.
        /// </summary>
        /// <param name="id">L'identifiant unique assigné à une catégorie.</param>
        /// <returns>
        /// Retourne la catégorie dont l'identifiant correspond à l'identifiant spécifié en paramètre, si elle existe. 
        /// </returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(RequeteCategorieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RequeteCategorieDto> Get(int id)
        {
            var categorie = _categoriesService.GetById(id);
            if (categorie is null)
            {
                return NotFound(new { StatusCode = StatusCodes.Status404NotFound, Errors = new[] { "Catégorie introuvable." }});
            }
            return categorie;
        }

        /// <summary>
        /// Permet la création d'une nouvelle catégorie.
        /// </summary>
        /// <param name="nomCategorie">La catégorie à ajouter.</param>
        /// <returns>
        /// La réponse HTTP de création, ainsi que le lien API permettant d'accéder à la catégorie nouvellement créée par son identifiant.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] string nomCategorie)
        {
            var nouvelleCategorie = _categoriesService.Add(nomCategorie);
            return CreatedAtAction(nameof(Get), new { id = nouvelleCategorie.Id }, null);
        }

        /// <summary>
        /// Permet la modification d'une catégorie préexistante.
        /// </summary>
        /// <param name="id">L'identifiant unique de la catégorie à modifier.</param>
        /// <param name="categorie">La catégorie à modifier.</param>
        /// <returns>Une réponse HTTP NoContent, si la modification est réussie.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] RequeteCategorieDto categorie)
        {
            _categoriesService.Update(id, categorie);
            return NoContent();
        }

        /// <summary>
        /// Permet la suppression d'une catégorie préexistante.
        /// </summary>
        /// <param name="id">L'identifiant unique de la catégorie à supprimer.</param>
        /// <returns>Une réponse HTTP NoContent, si la suppression est réussie.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            _categoriesService.Delete(id);
            return NoContent();
        }
    }
}
