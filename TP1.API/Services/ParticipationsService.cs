using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using TP1.API.Data;
using TP1.API.Data.Models;
using TP1.API.Exceptions;
using TP1.API.Interfaces;

namespace TP1.API.Services
{
    public class ParticipationsService : IParticipationsService
    {
        public IEnumerable<Participation> GetList()
        {
            return Repository.Participations;
        }

        public IEnumerable<Participation> GetList(Func<Participation, bool> predicat)
        {
            return Repository.Participations.Where(predicat);
        }

        public Participation GetById(int id)
        {
            var participation = Repository.Participations.FirstOrDefault(p => p.Id == id);
            return participation;
        }

        public Participation Add(Participation participation)
        {
            if (participation is null)
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    "Veuillez remplir les champs obligatoires."
                );
            }
            
            var erreurs = Valider(participation);

            if (erreurs.Any())
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    erreurs.ToArray()
                );
            }

            participation.Id = Repository.IdSequenceParticipation++;
            Repository.Participations.Add(participation);

            return participation;
        }

        public void Delete(int id)
        {
            var participation = Repository.Participations.FirstOrDefault(p => p.Id == id);

            if (participation is null)
            {
                throw new HttpException(
                    StatusCodes.Status404NotFound,
                    "La participation demandée est introuvable"
                );
            }

            Repository.Participations.Remove(participation);
        }

        private List<string> Valider(Participation participation)
        {
            var erreurs = new List<string>();

            if(string.IsNullOrEmpty(participation.Nom) || string.IsNullOrEmpty(participation.Prenom))
            {
                erreurs.Add("La participation doit être avec un nom et prénom valide.");
            }

            // expression régulière pour matcher les adresses courriel. 
            var regexEmail = new Regex(@"^([a-zA-Z0-9]+)(([-_\.]){1}[a-zA-Z0-9]+)*@([a-z0-9\-]+\.)+[a-z]{2,}$");

            if(string.IsNullOrEmpty(participation.AdresseCourriel) || !regexEmail.IsMatch(participation.AdresseCourriel))
            {
                erreurs.Add("L'adresse courriel associée à l'évènement doit être valide.");
            }

            if (participation.NombrePlace < 1)
            {
                erreurs.Add("La participation doit être faite pour un minimum de 1 place.");
            }

            var evenementExiste = Repository.Evenements.Any(e => e.Id == participation.IdEvenement);

            if (!evenementExiste)
            {
                erreurs.Add("L'évènement dont vous essayer de créer une participation pour n'existe pas.");
            }

            var participationExiste = Repository.Participations.Any(p =>
            {
                return p.AdresseCourriel == participation.AdresseCourriel
                       && p.IdEvenement == participation.IdEvenement;
            });

            if (participationExiste)
            {
                erreurs.Add("Une participation avec cette adresse courriel est déjà existante pour cet évènement.");
            }

            return erreurs;
        }
    }
}