using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using TP1.API.Data;
using TP1.API.Exceptions;
using TP1.API.Interfaces;
using TP1.API.Models;

namespace TP1.API.Services
{
    public class CategoriesService : ICategoriesService
    {
        public Categorie Add(Categorie categorie)
        {
            if (categorie is null)
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    "La catégorie ne peut être une valeur nulle."
                );
            }

            var erreurs = Valider(categorie);

            if (erreurs.Any())
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    erreurs.ToArray()
                );
            }

            categorie.Id = Repository.IdSequenceCategorie++;
            Repository.Categories.Add(categorie);

            return categorie;
        }

        public void Delete(int id)
        {
            var categorieASupprimer = Repository.Categories.FirstOrDefault(c => c.Id == id);

            if (categorieASupprimer is null)
            {
                throw new HttpException(
                    StatusCodes.Status404NotFound,
                     "Catégorie introuvable."
                );
            }

            var categorieEstAssocie = Repository.CategorieEvenements.Any(e => e.IdCategorie == id);

            if (categorieEstAssocie)
            {
                throw new HttpException(
                   StatusCodes.Status400BadRequest,
                   "La catégorie est associée à un évènement et donc ne peut être supprimée."
               );
            }

            Repository.Categories.Remove(categorieASupprimer);
        }

        public Categorie GetById(int id)
        {
            var categorie = Repository.Categories.FirstOrDefault(c => c.Id == id);
            return categorie;
        }

        public IEnumerable<Categorie> GetList()
        {
            return Repository.Categories;
        }

        public Categorie Update(int id, Categorie categorie)
        {
            if (categorie is null)
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    "La catégorie ne peut être une valeur nulle."
                );
            }

            if (id != categorie.Id)
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    "L'identifiant passé en paramètre ne correspond pas à celui de la catégorie à modifier."
                );
            }

            var categorieAModifier = Repository.Categories.FirstOrDefault(c => c.Id == id);

            if (categorieAModifier is null)
            {
                throw new HttpException(
                    StatusCodes.Status404NotFound,
                    "Catégorie introuvable."
                );
            }

            var erreurs = Valider(categorie);

            if(erreurs.Any())
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    erreurs.ToArray()
                );
            }
           
            categorieAModifier.Nom = categorie.Nom;

            return categorieAModifier;
        }

        private List<string> Valider(Categorie categorie)
        {
            var erreurs = new List<string>();

            if (string.IsNullOrEmpty(categorie.Nom))
            {
                erreurs.Add("Veuillez ajouter un nom à votre catégorie.");
            }

            var existe = Repository.Categories.Any(c => c.Nom == categorie.Nom);

            if (existe)
            {
                erreurs.Add("Cette catégorie existe déjà.");
            }

            return erreurs;
        }
    }
}
