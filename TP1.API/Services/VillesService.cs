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
        public Ville Add(Ville ville)
        {
            if (ville is null)
            {
                throw new HttpException
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Errors = new[] { "La ville doit être une valeur non nulle." }
                };
            }

            var (estValide, erreurs) = Valider(ville);

            if (!estValide)
            {
                throw new HttpException
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Errors = erreurs
                };
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
                throw new HttpException
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Errors = new[] { "La ville est introuvable." }
                };
            }

            var evenementsAssocies = Repository.Evenements.Where(e => e.IdVille == id);
            if (evenementsAssocies.Any())
            {
                throw new HttpException
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Errors = new[] 
                    { 
                        "Vous ne pouvez supprimer une ville qui a au moins un évènement associé à elle." 
                    }
                };
            }

            Repository.Villes.Remove(ville);
        }

        public Ville GetById(int id)
        {
            var ville = Repository.Villes.FirstOrDefault(v => v.Id == id);

            if (ville is null)
            {
                throw new HttpException
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Errors = new[] { "La ville est introuvable." }
                };
            }

            return ville;
        }

        public IEnumerable<Ville> GetList()
        {
            return Repository.Villes;
        }

        public Ville Update(int id, Ville ville)
        {
            if (id != ville.Id)
            {
                throw new HttpException
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Errors = new[]
                    {
                        "L'identifiant passé en paramètre est différent de l'identifiant de la ville passé dans le corps de la requête."
                    }
                };
            }

            var villeExistante = Repository.Villes.FirstOrDefault(v => v.Id == id);

            if (villeExistante is null)
            {
                throw new HttpException
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Errors = new[] { "La ville est introuvable." }
                };
            }


            var (estValide, erreurs) = Valider(ville);

            if (!estValide)
            {
                throw new HttpException
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Errors = erreurs
                };
            }

            villeExistante.Nom = ville.Nom;
            villeExistante.Region = ville.Region;

            return villeExistante;
        }

        private (bool estValide, IEnumerable<string> erreurs) Valider(Ville ville)
        {
            var erreurs = new List<string>();
            bool estValide = true;

            if (string.IsNullOrEmpty(ville.Nom))
            {
                erreurs.Add("Le nom de la ville ne doit pas être vide.");
                estValide = false;
            }

            if (ville.Region == Region.Aucune)
            {
                erreurs.Add("La ville doit appartenir à une région. (Ne doit pas être aucune.)");
                estValide = false;
            }

            return (estValide, erreurs);
        }
    }
}
