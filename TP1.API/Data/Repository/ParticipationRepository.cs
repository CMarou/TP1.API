using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TP1.API.Data.Models;
using TP1.API.DTOs;

namespace TP1.API.Data.Repository
{
    public class ParticipationRepository : IParticipationRepository
    {
        private readonly ApplicationContext _context;

        public ParticipationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<RequeteParticipationDto> GetList()
        {
            return _context.Participations
                .AsNoTracking()
                .Include(p => p.Evenement)
                .Select(p => new RequeteParticipationDto
                {
                    Id = p.Id,
                    AdresseCourriel = p.AdresseCourriel,
                    Prenom = p.Prenom,
                    Nom = p.Nom,
                    Places = p.NombrePlace,
                    EvenementId = p.Evenement.Id,
                    TitreEvenement = p.Evenement.Titre
                });
        }

        public IEnumerable<RequeteParticipationDto> GetList(Expression<Func<RequeteParticipationDto, bool>> predicate)
        {
            return _context.Participations
                .AsNoTracking()
                .Include(p => p.Evenement)
                .Select(p => new RequeteParticipationDto
                {
                    Id = p.Id,
                    AdresseCourriel = p.AdresseCourriel,
                    Prenom = p.Prenom,
                    Nom = p.Nom,
                    Places = p.NombrePlace,
                    EvenementId = p.Evenement.Id,
                    TitreEvenement = p.Evenement.Titre
                })
                .Where(predicate);
        }

        public RequeteParticipationDto GetById(int id)
        {
            var participation = _context.Participations
                .AsNoTracking()
                .Include(p => p.Evenement)
                .FirstOrDefault(p => p.Id == id);

            if (participation is not null)
            {
                return new RequeteParticipationDto
                {
                    Id = participation.Id,
                    AdresseCourriel = participation.AdresseCourriel,
                    Prenom = participation.Prenom,
                    Nom = participation.Nom,
                    Places = participation.NombrePlace,
                    EvenementId = participation.Evenement.Id,
                    TitreEvenement = participation.Evenement.Titre
                };
            }

            return null;
        }

        public RequeteParticipationDto Add(EnvoiParticipationDto participation)
        {
            var evenement = _context.Evenements
                .FirstOrDefault(e => e.Id == participation.EvenementId);

            var entite = new Participation
            {
                Prenom = participation.Prenom,
                Nom = participation.Nom,
                AdresseCourriel = participation.AdresseCourriel,
                Evenement = evenement,
                NombrePlace = participation.NombrePlace
            };
            
            _context.Participations.Add(entite);
            _context.SaveChanges();

            return new RequeteParticipationDto
            {
                Id = entite.Id,
                AdresseCourriel = entite.AdresseCourriel,
                Prenom = entite.Prenom,
                Nom = entite.Nom,
                Places = entite.NombrePlace,
                EvenementId = entite.Evenement.Id,
                TitreEvenement = entite.Evenement.Titre
            };
        }

        public void Update(RequeteParticipationDto participation)
        {        
            var participationToUpdate = _context.Participations
                .IgnoreQueryFilters()
                .FirstOrDefault(p => p.Id == participation.Id);
            _context.Participations.Update(participationToUpdate);
            _context.SaveChanges();
        }
        
        public void Delete(int id)
        {
            var entity = _context.Participations.FirstOrDefault(c => c.Id == id);

            if (entity is null)
                throw new DbUpdateException("Entity to delete was not found.");

            _context.Participations.Remove(entity);
            _context.SaveChanges();
        }

        public RequeteParticipationDto GetByIdUnfiltered(int id)
        {
            var participation = _context.Participations
               .IgnoreQueryFilters()
               .AsNoTracking()
               .Include(p => p.Evenement)      
               .FirstOrDefault(p => p.Id == id);

            if (participation is not null)
            {
                return new RequeteParticipationDto
                {
                    Id = participation.Id,
                    AdresseCourriel = participation.AdresseCourriel,
                    Prenom = participation.Prenom,
                    Nom = participation.Nom,
                    Places = participation.NombrePlace,
                    EvenementId = participation.Evenement.Id,
                    TitreEvenement = participation.Evenement.Titre
                };
            }

            return null;
        }

        public bool Exists(int id)
        {
            var participation = _context.Participations
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);

            return participation is not null;
        }

    }
}
