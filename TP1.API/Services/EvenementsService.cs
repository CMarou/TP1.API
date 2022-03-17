using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TP1.API.Data;
using TP1.API.Exceptions;
using TP1.API.Interfaces;
using TP1.API.Data.Repository;
using TP1.API.DTOs;
using System.Linq.Expressions;
using TP1.API.Data.Models;

namespace TP1.API.Services
{
    public class EvenementsService : IEvenementsService
    {
        private readonly IEvenementRepository _evenementRepository;
        private readonly IVilleRepository _villeRepository;

        public EvenementsService(IEvenementRepository repository, IVilleRepository villeRepository)
        {
            _evenementRepository = repository;
            _villeRepository = villeRepository;
        }

        public IEnumerable<RequeteEvenementDto> GetList(int pageIndex, int pageSize)
        {
            return _evenementRepository.GetList(pageIndex, pageSize);
        }

        public IEnumerable<RequeteEvenementDto> GetList(Expression<Func<RequeteEvenementDto, bool>> predicate, int pageIndex, int pageSize)
        {
            return _evenementRepository.GetList(predicate, pageIndex, pageSize);
        }

        public IEnumerable<EvenementParticipationDto> GetParticipationsForEvent(int eventId)
        {
            return _evenementRepository.GetParticipationsForEvent(eventId);
        }

        public RequeteEvenementDto GetById(int id)
        {
            return _evenementRepository.GetById(id);
        }

        public RequeteEvenementDto Add(EnvoiEvenementDto evenement)
        {
            if (evenement is null)
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    "L'évènement doit être une valeur non nulle."
                );
            }

            var erreurs = Valider(evenement);

            if (erreurs.Any())
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    erreurs.ToArray()
                );
            }

            return _evenementRepository.Add(evenement);
        }

        public void Update(int id, RequeteEvenementDto evenement)
        {
            if (evenement is null)
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    "L'évènement doit être une valeur non nulle."
                );
            }

            if (id != evenement.Id)
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    "L'identifiant passé en paramètre est différent de l'identifiant de la ville passé dans le corps de la requête."
                );
            }

            var evenementAModifier = _evenementRepository.GetById(id);

            if (evenementAModifier is null)
            {
                throw new HttpException(
                    StatusCodes.Status404NotFound,
                    "Évènement introuvable"
                );
            }

            var erreurs = Valider(evenement);

            if (erreurs.Any())
            {
                throw new HttpException(
                    StatusCodes.Status400BadRequest,
                    erreurs.ToArray()
                );
            }

            _evenementRepository.Update(evenement);
        }

        public void Delete(int id)
        {
            var evenement = _evenementRepository.GetById(id);

            if (evenement is null)
            {
                throw new HttpException(
                    StatusCodes.Status404NotFound,
                    "L'évènement est introuvable."
                );
            }

            _evenementRepository.Delete(id);
        }

        private List<string> Valider(EnvoiEvenementDto evenement)
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

            var villeExiste = _villeRepository.GetList(v => v.Id == evenement.VilleId).Any();

            if (!villeExiste)
            {
                erreurs.Add("La ville assignée pour l'évènement n'existe pas.");
            }

            var evenementExiste = _evenementRepository
                .GetList(
                    e => e.Titre == evenement.Titre 
                        && e.AdresseCivique == evenement.AdresseCivique 
                        && e.DateDebut == evenement.DateDebut
                        && e.DateFin == evenement.DateFin
                        && e.NomOrganisateur == evenement.NomOrganisateur
                )
                .Any();

            if (evenementExiste)
            {
                erreurs.Add("L'évènement existe déjà.");
            }

            return erreurs;
        }

        private List<string> Valider(RequeteEvenementDto evenement)
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

            if (evenement.Categories.Count < 1)
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

            var villeExiste = _villeRepository
                .GetList(v => v.Nom == evenement.Ville && v.Region.ToString() == evenement.Region)
                .Any();

            if (!villeExiste)
            {
                erreurs.Add("La ville assignée pour l'évènement n'existe pas.");
            }

            var evenementExiste = _evenementRepository.GetList(e => e.Equals(evenement)).Any();

            if (evenementExiste)
            {
                erreurs.Add("L'évènement existe déjà.");
            }

            return erreurs;
        }
    }
}