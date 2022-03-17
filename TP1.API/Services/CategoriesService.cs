using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using TP1.API.Data.Repository;
using TP1.API.DTOs;
using TP1.API.Exceptions;
using TP1.API.Interfaces;

namespace TP1.API.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategorieRepository _categorieRepository;
        private readonly IEvenementRepository _evenementRepository;

        public CategoriesService(ICategorieRepository repository, IEvenementRepository envRepository)
        {
            _categorieRepository = repository;
            _evenementRepository = envRepository;
        }

        public IEnumerable<RequeteCategorieDto> GetList()
        {
            return _categorieRepository.GetList();
        }

        public RequeteCategorieDto GetById(int id)
        {
            return _categorieRepository.GetById(id);
        }

        public RequeteCategorieDto Add(string categorie)
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

            return _categorieRepository.Add(categorie);
        }

        public void Update(int id, RequeteCategorieDto categorie)
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

            var categorieAModifier = _categorieRepository.GetById(id);

            if (categorieAModifier is null)
            {
                throw new HttpException(
                    StatusCodes.Status404NotFound,
                    "Catégorie introuvable."
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

            _categorieRepository.Update(categorie);
        }

        public void Delete(int id)
        {
            var categorieASupprimer = _categorieRepository.GetById(id);

            if (categorieASupprimer is null)
            {
                throw new HttpException(
                    StatusCodes.Status404NotFound,
                    "Catégorie introuvable."
                );
            }

            var categorieEstAssocie = _evenementRepository.GetList(e => e.Categories.Contains(categorieASupprimer.Nom)).Any();   

            if (categorieEstAssocie)
            {
                throw new HttpException(
                   StatusCodes.Status400BadRequest,
                   "La catégorie est associée à un évènement et donc ne peut être supprimée."
               );
            }

            _categorieRepository.Delete(id);
        }

        private List<string> Valider(RequeteCategorieDto categorie)
        {
            return Valider(categorie.Nom);
        }

        private List<string> Valider(string nomCategorie)
        {
            var erreurs = new List<string>();

            if (string.IsNullOrEmpty(nomCategorie))
            {
                erreurs.Add("Veuillez ajouter un nom à votre catégorie.");
            }

            var existe = _categorieRepository.GetList(c => c.Nom == nomCategorie).Any();

            if (existe)
            {
                erreurs.Add("Cette catégorie existe déjà.");
            }

            return erreurs;
        }
    }
}

