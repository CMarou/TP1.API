using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using TP1.API.Data.Repository;
using TP1.API.DTOs;
using TP1.API.Exceptions;
using TP1.API.Interfaces;

namespace TP1.API.Services
{
    public class ParticipationsService : IParticipationsService
    {
        private readonly IParticipationRepository _participationRepository;
        private readonly IEvenementRepository _evenementRepository;

        public ParticipationsService(IParticipationRepository participationRepository, IEvenementRepository evenementRepository)
        {
            _participationRepository = participationRepository;
            _evenementRepository = evenementRepository;
        }

        public IEnumerable<RequeteParticipationDto> GetList()
        {
            return _participationRepository.GetList();
        }

        public IEnumerable<RequeteParticipationDto> GetList(Expression<Func<RequeteParticipationDto, bool>> predicat)
        {
            return _participationRepository.GetList(predicat);
        }

        public RequeteParticipationDto GetById(int id)
        {
            var participation = _participationRepository.GetById(id);
            return participation;
        }

        public RequeteParticipationDto Add(EnvoiParticipationDto participation)
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

            var participationAjoutee = _participationRepository.Add(participation);

            return participationAjoutee;
        }

        public void Activate(int id)
        {
            var participation = _participationRepository.GetByIdUnfiltered(id);
            _participationRepository.Update(participation);
        }

        public void Delete(int id)
        {
            var participationASupprimer = _participationRepository.GetById(id);

            if (participationASupprimer is null)
            {
                throw new HttpException(
                   StatusCodes.Status404NotFound,
                   "La participation demandée est introuvable"
               );
            }

            _participationRepository.Delete(id);
        }

        public bool Exists(int id)
        {
            return _participationRepository.Exists(id); 
        }

        private List<string> Valider(EnvoiParticipationDto participation)
        {
            var erreurs = new List<string>();

            if (string.IsNullOrEmpty(participation.Nom) || string.IsNullOrEmpty(participation.Prenom))
            {
                erreurs.Add("La participation doit être avec un nom et prénom valide.");
            }

            // expression régulière pour matcher les adresses courriel. 
            var regexEmail = new Regex(@"^([a-zA-Z0-9]+)(([-_\.]){1}[a-zA-Z0-9]+)*@([a-z0-9\-]+\.)+[a-z]{2,}$");

            if (string.IsNullOrEmpty(participation.AdresseCourriel) || !regexEmail.IsMatch(participation.AdresseCourriel))
            {
                erreurs.Add("L'adresse courriel associée à l'évènement doit être valide.");
            }

            if (participation.NombrePlace < 1)
            {
                erreurs.Add("La participation doit être faite pour un minimum de 1 place.");
            }

            var evenementExiste = _evenementRepository.GetList(e => e.Id == participation.EvenementId).Any();

            if (!evenementExiste)
            {
                erreurs.Add("L'évènement dont vous essayer de créer une participation pour n'existe pas.");
            }

            var participationExiste = _participationRepository
                .GetList(p => p.AdresseCourriel == participation.AdresseCourriel && p.EvenementId == participation.EvenementId)
                .Any();

            if (participationExiste)
            {
                erreurs.Add("Une participation avec cette adresse courriel est déjà existante pour cet évènement.");
            }

            return erreurs;
        }
    }
}
