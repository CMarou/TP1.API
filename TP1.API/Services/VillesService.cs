
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using TP1.API.Data;
using TP1.API.Exceptions;
using TP1.API.Interfaces;
using TP1.API.Models;

namespace TP1.API.Services
{
    public class VillesService : IVillesService
    {
        private readonly IHttpExceptionThrower _exceptionThrower;

        public VillesService(IHttpExceptionThrower exceptionThrower)
        {
            _exceptionThrower = exceptionThrower;
        }

        public Ville Add(Ville ville)
        {
            if (ville is null)
            {
                _exceptionThrower.ThrowHttpException(
                    StatusCodes.Status400BadRequest,
                    "La ville doit être une valeur non nulle." 
                );
            }

            var erreurs = Valider(ville);

            if (erreurs.Any())
            {
                _exceptionThrower.ThrowHttpException(
                    StatusCodes.Status400BadRequest,
                    erreurs.ToArray()
                );
            }

            ville.Id = Repository.IdSequenceVille++;
            Repository.Villes.Add(ville);

            return ville;
        }

        public void Delete(int id)
        {
            var ville = Repository.Villes.FirstOrDefault(v => v.Id == id);

            if (ville is null)
            {
                _exceptionThrower.ThrowHttpException(
                    StatusCodes.Status404NotFound,
                    "La ville est introuvable."
                );
            }

            var evenementsAssocies = Repository.Evenements.Where(e => e.IdVille == id);
            if (evenementsAssocies.Any())
            {
                _exceptionThrower.ThrowHttpException(
                    StatusCodes.Status400BadRequest,
                     "Vous ne pouvez supprimer une ville qui a au moins un évènement associé à elle."
                );
            }

            Repository.Villes.Remove(ville);
        }

        public Ville GetById(int id)
        {
            var ville = Repository.Villes.FirstOrDefault(v => v.Id == id);

            if (ville is null)
            {
                _exceptionThrower.ThrowHttpException(
                    StatusCodes.Status404NotFound,
                    "La ville est introuvable."
                );
            }

            return ville;
        }

        public IEnumerable<Ville> GetList()
        {
            return Repository.Villes;
        }

        public Ville Update(int id, Ville ville)
        {
            if (ville is null)
            {
                _exceptionThrower.ThrowHttpException(
                    StatusCodes.Status400BadRequest,
                    "La ville ne peut être une valeur nulle."
                );
            }

            if (id != ville.Id)
            {
                _exceptionThrower.ThrowHttpException(
                    StatusCodes.Status400BadRequest,
                    "L'identifiant passé en paramètre est différent de l'identifiant de la ville passé dans le corps de la requête."
                );
            }

            var villeExistante = Repository.Villes.FirstOrDefault(v => v.Id == id);

            if (villeExistante is null)
            {
                _exceptionThrower.ThrowHttpException(
                   StatusCodes.Status404NotFound,
                   "La ville est introuvable."
               );
            }

            var erreurs = Valider(ville);

            if (erreurs.Any())
            {
                _exceptionThrower.ThrowHttpException(
                    StatusCodes.Status400BadRequest,
                    erreurs.ToArray()
                );
            }

            villeExistante.Nom = ville.Nom;
            villeExistante.Region = ville.Region;

            return villeExistante;
        }

        private static List<string> Valider(Ville ville)
        {
            var erreurs = new List<string>();

            if (string.IsNullOrEmpty(ville.Nom))
            {
                erreurs.Add("Le nom de la ville ne doit pas être vide.");
            }

            var existe = Repository.Villes.Any(v => v.Nom == ville.Nom);

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
