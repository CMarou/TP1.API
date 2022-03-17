using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using TP1.API.Data.Models;
using TP1.API.Data.Repository;
using TP1.API.DTOs;
using TP1.API.Exceptions;
using TP1.API.Interfaces;

namespace TP1.API.Services
{
    public class VillesService : IVillesService
    {
        private readonly IVilleRepository _repository;
        
        public VillesService(IVilleRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<RequeteVilleDto> GetList()
        {
            return _repository.GetCitiesByEventCountDescending();
        }

        public IEnumerable<VilleEvenementDto> GetEventsForCity(int id)
        {
            var ville = _repository.GetById(id);
            if (ville is null)
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    "La ville ne peut être une valeur nulle."
                );
            }

            return _repository.GetEventsForCity(id);
        }

        public RequeteVilleDto GetById(int id)
        {
            var ville = _repository.GetById(id);
            return ville;
        }

        public RequeteVilleDto Add(EnvoiVilleDto ville)
        {
            if (ville is null)
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    "La ville doit être une valeur non nulle."
                );
            }

            var erreurs = Valider(ville);

            if (erreurs.Any())
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    erreurs.ToArray()
                );
            }

            var villeAjoutee = _repository.Add(ville);

            return villeAjoutee;
        }

        public void Update(int id, RequeteVilleDto ville)
        {
            if (ville is null)
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    "La ville ne peut être une valeur nulle."
                );
            }

            if (id != ville.Id)
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    "L'identifiant passé en paramètre est différent de l'identifiant de la ville passé dans le corps de la requête."
                );
            }

            var villeExistante = _repository.GetById(id);

            if (villeExistante is null)
            {
                throw new HttpException(
                   StatusCodes.Status404NotFound,
                   "La ville est introuvable."
               );
            }

            var erreurs = Valider(ville);

            if (erreurs.Any())
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    erreurs.ToArray()
                );
            }

            _repository.Update(ville);
        }

        public void Delete(int id)
        {
            var ville = _repository.GetById(id);

            if (ville is null)
            {
                throw new HttpException(
                    StatusCodes.Status404NotFound,
                    "La ville est introuvable."
                );
            }

            var evenementsSontAssocies = _repository.GetEventsForCity(id).Any();
            if (evenementsSontAssocies)
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                     "Vous ne pouvez supprimer une ville qui a au moins un évènement associé à elle."
                );
            }

            _repository.Delete(id);
        }

        private List<string> Valider(EnvoiVilleDto ville)
        {
            return Valider(new RequeteVilleDto { Nom = ville.Nom, Region = ville.Region });
        }

        private List<string> Valider(RequeteVilleDto ville)
        {
            var erreurs = new List<string>();

            if (string.IsNullOrEmpty(ville.Nom))
            {
                erreurs.Add("Le nom de la ville ne doit pas être vide.");
            }

            var existe = _repository.GetList(v => v.Nom == ville.Nom).Any();

            if (existe)
            {
                erreurs.Add("Cette ville existe déjà.");
            }

            if (ville.Region == Region.Aucune)
            {
                erreurs.Add("La ville doit appartenir à une région. (Ne doit pas être aucune.)");
            }

            return erreurs;
        }
    }
}