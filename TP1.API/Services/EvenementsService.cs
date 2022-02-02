using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using TP1.API.Data;
using TP1.API.Interfaces;
using TP1.API.Models;

namespace TP1.API.Services
{
    public class EvenementsServices : IEvenementsService
    {
        private readonly IHttpExceptionThrower _exceptionThrower;

        public EvenementsServices(IHttpExceptionThrower exceptionThrower)
        {
            _exceptionThrower = exceptionThrower;
        }

        public IEnumerable<Evenement> GetList()
        {
            return Repository.Evenements;
        }

        public IEnumerable<Evenement> GetList(Func<Evenement, bool> predicat)
        {
            return Repository.Evenements.Where(predicat);
        }

        public Evenement GetById(int id)
        {
            var evenementExistant = Repository.Evenements.FirstOrDefault(e => e.Id == id);

            if (evenementExistant is null)
            {
                _exceptionThrower.ThrowHttpException(
                    StatusCodes.Status400BadRequest,
                    "Évènement introuvable"
                );
            }

            return evenementExistant;
        }

        public Evenement Add(Evenement evenement)
        {
           if (evenement is null)
            {
                _exceptionThrower.ThrowHttpException(
                    StatusCodes.Status400BadRequest,
                    "L'évènement doit être une valeur non nulle."
                );
            }

           var erreurs = Valider(evenement);

           if (erreurs.Any())
           {
                _exceptionThrower.ThrowHttpException(
                    StatusCodes.Status400BadRequest,
                    erreurs.ToArray()
                );
           }

           evenement.Id = Repository.IdSequenceEvenement++;
           Repository.Evenements.Add(evenement);

           return evenement;

        }

        public Evenement Update(int id, Evenement evenement)
        {
            if (evenement is null)
            {
                _exceptionThrower.ThrowHttpException(
                    StatusCodes.Status400BadRequest,
                    "L'évènement doit être une valeur non nulle."
                );
            }

            if (id != evenement.Id)
            {
                _exceptionThrower.ThrowHttpException(
                    StatusCodes.Status400BadRequest,
                    "L'identifiant passé en paramètre est différent de l'identifiant de la ville passé dans le corps de la requête."
                );
            }

            var evenementAModifier = Repository.Evenements.FirstOrDefault(e => e.Id == id);

            if (evenementAModifier is null)
            {
                _exceptionThrower.ThrowHttpException(
                    StatusCodes.Status404NotFound,
                    "Évènement introuvable"
                );
            }

            var erreurs = Valider(evenement);

            if (erreurs.Any())
            {
                _exceptionThrower.ThrowHttpException(
                    StatusCodes.Status400BadRequest,
                    erreurs.ToArray()
                );
            }

            evenementAModifier.Titre = evenement.Titre;
            evenementAModifier.DateDebut = evenement.DateDebut;
            evenementAModifier.DateFin = evenement.DateFin;
            evenementAModifier.NomOrganisateur = evenement.NomOrganisateur;
            evenementAModifier.Description = evenement.Description;
            evenementAModifier.AdresseCivique = evenement.AdresseCivique;
            evenementAModifier.Prix = evenement.Prix;
            evenementAModifier.CategoriesId = evenement.CategoriesId;
            evenementAModifier.IdVille = evenement.IdVille;

            return evenementAModifier;
        }

        public void Delete(int id)
        {
            var evenement = Repository.Evenements.FirstOrDefault(e => e.Id == id);

            if (evenement is null)
            {
                _exceptionThrower.ThrowHttpException(
                    StatusCodes.Status404NotFound,
                    "L'évènement est introuvable."
                );
            }

            //Dans un contexte d'entreprise, si l'evenement cible a une participation associee ou plus,
            //une action devrait etre entreprise pour aviser les participants de l'annulation d'un evenement.

            var participations = Repository.Participations.Where(p => p.IdEvenement == evenement.Id);

            //On supprime les participations associées à cet évenement. Il faudrait probablement notifier un service externe pour
            //rembourser les participants.
            foreach(var participation in participations)
            {
                Repository.Participations.Remove(participation);
            }

            Repository.Evenements.Remove(evenement);
        }

        private List<string> Valider(Evenement evenement)
        {
            var erreurs = new List<string>();

            if (string.IsNullOrEmpty(evenement.Titre))
            {
                erreurs.Add("Le titre de l'évènement ne doit pas être vide.");
            }

            if (string.IsNullOrEmpty(evenement.NomOrganisateur))
            {
                erreurs.Add("Le nom de l'organisateur ne doit pas être vide.");
            }

            if (string.IsNullOrEmpty(evenement.Description))
            {
                erreurs.Add("La description ne doit pas être vide.");
            }

            if (string.IsNullOrEmpty(evenement.AdresseCivique))
            {
                erreurs.Add("L'adresse civique du lieu de l'évènement ne doit pas être vide.");
            }

            if (evenement.CategoriesId.Count < 1)
            {
                erreurs.Add("L'évènement doit avoir au moins une catégorie.");
            }

            var maintenant = DateTime.Now;

            if (evenement.DateDebut < maintenant)
            {
                erreurs.Add("L'évènement ne peut commencer à une date antérieure à aujourd'hui.");
            }

            if (evenement.DateFin <= evenement.DateDebut)
            {
                erreurs.Add("L'évènement ne peut se terminer avant ou en même temps qu'il commence.");
            }

            if (evenement.Prix is not null && evenement.Prix <= 0)
            {
                erreurs.Add("L'évènement doit avoir un prix minimal supérieur à zéro si un prix est fourni.");
            }

            var villeExiste = Repository.Villes.Any(v => v.Id == evenement.IdVille);

            if (!villeExiste)
            {
                erreurs.Add("La ville assignée pour l'évènement n'existe pas.");
            }

            var evenementExiste = Repository.Evenements.Any(e => e.Equals(evenement));

            if (evenementExiste)
            {
                erreurs.Add("L'évènement existe déjà.");
            }

            return erreurs;
        }
    }
}