using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TP1.API.Interfaces;
using TP1.API.Models;

namespace TP1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        // GET: api/<CategorieController>
        [HttpGet]
        public ActionResult<IEnumerable<Categorie>> Get()
        {
            return Ok(_categoriesService.GetList());
        }
        
        // GET api/<CategorieController>/5
        [HttpGet("{id:int}")]
        public ActionResult<Categorie> Get(int id)
        {
            var categorie = _categoriesService.GetById(id);
            return categorie;
        }

        // POST api/<CategorieController>
        [HttpPost]
        public IActionResult Post([FromBody] Categorie categorie)
        {
            var nouvelleCategorie = _categoriesService.Add(categorie);
            return CreatedAtAction(nameof(Get), new { id = nouvelleCategorie.Id }, null);
        }
        /*
         * Réponse : 
         * Headers :
         * ....
         * Location : https://localhost:5001/api/categories/{id}
         * Body:
         *   ---
         */

        // PUT api/<CategorieController>/5
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Categorie categorie)
        {
            _ = _categoriesService.Update(id, categorie);
            return NoContent();
        }

        // DELETE api/<CategorieController>/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _categoriesService.Delete(id);
            return NoContent();
        }
    }
}
