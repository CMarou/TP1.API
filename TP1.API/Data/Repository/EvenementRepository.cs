using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TP1.API.Data.Models;
using TP1.API.DTOs;

namespace TP1.API.Data.Repository
{
    public class EvenementRepository : IEvenementRepository
    {
        private readonly ApplicationContext _context;

        public EvenementRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<RequeteEvenementDto> GetList(int pageIndex = 1, int pageSize = 10)
        {
            int skipAmount = (pageIndex - 1) * pageSize;

            return _context.Evenements
                .AsNoTracking()
                .Include(e => e.Ville)
                .Include(e => e.Categories)
                .OrderBy(e => e.DateDebut)
                .Select(e => new RequeteEvenementDto
                {
                    Id = e.Id,
                    Titre = e.Titre,
                    AdresseCivique = e.AdresseCivique,
                    DateDebut = e.DateDebut,
                    DateFin = e.DateFin,
                    NomOrganisateur = e.NomOrganisateur,
                    Description = e.Description,
                    Prix = e.Prix,
                    Ville = e.Ville.Nom,
                    Region = e.Ville.Region.ToString(),
                    Categories = e.Categories.Select(c => c.Nom).ToList()
                })
                .Skip(skipAmount)
                .Take(pageSize);
        }

        public IEnumerable<RequeteEvenementDto> GetList(Expression<Func<RequeteEvenementDto, bool>> predicate, int pageIndex = 1, int pageSize = 10)
        {
            int skipAmount = (pageIndex - 1) * pageSize;

            return _context.Evenements
                .AsNoTracking()
                .Include(e => e.Ville)
                .Include(e => e.Categories)
                .OrderBy(e => e.DateDebut)
                .Select(e => new RequeteEvenementDto
                {
                    Id = e.Id,
                    Titre = e.Titre,
                    AdresseCivique = e.AdresseCivique,
                    DateDebut = e.DateDebut,
                    DateFin = e.DateFin,
                    NomOrganisateur = e.NomOrganisateur,
                    Description = e.Description,
                    Prix = e.Prix,
                    Ville = e.Ville.Nom,
                    Region = e.Ville.Region.ToString(),
                    Categories = e.Categories.Select(c => c.Nom).ToList()
                })
                .Where(predicate)
                .Skip(skipAmount)
                .Take(pageSize);
        }

        public IEnumerable<RequeteEvenementDto> GetList(Expression<Func<RequeteEvenementDto, bool>> predicate)
        {
            return _context.Evenements
                .AsNoTracking()
                .Include(e => e.Ville)
                .Include(e => e.Categories)
                .OrderBy(e => e.DateDebut)
                .Select(e => new RequeteEvenementDto
                {
                    Id = e.Id,
                    Titre = e.Titre,
                    AdresseCivique = e.AdresseCivique,
                    DateDebut = e.DateDebut,
                    DateFin = e.DateFin,
                    NomOrganisateur = e.NomOrganisateur,
                    Description = e.Description,
                    Prix = e.Prix,
                    Ville = e.Ville.Nom,
                    Region = e.Ville.Region.ToString(),
                    Categories = e.Categories.Select(c => c.Nom).ToList()
                })
                .Where(predicate);
        }

        public IEnumerable<EvenementParticipationDto> GetParticipationsForEvent(int eventId)
        { 
            return _context.Participations
                .AsNoTracking()
                .Include(p => p.Evenement)
                .Where(p => p.Id == eventId)
                .Select(p => new EvenementParticipationDto
                {
                    Id = p.Id,
                    Prenom = p.Prenom,
                    Nom = p.Nom,
                    AdresseCourriel = p.AdresseCourriel,
                    Places = p.NombrePlace
                });      
        }

        public double GetTotalSalesAmountFromEvent(int eventId)
        {
            var myEvent = _context.Evenements
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == eventId);

            double total = _context.Participations
                 .AsNoTracking()
                 .Include(p => p.Evenement)
                 .Where(p => p.Evenement.Id == eventId)
                 .Aggregate(0, (salesAmount, p) => salesAmount + p.NombrePlace); ;
              
            total *= myEvent?.Prix ?? 0;

            return total;
        }

        public RequeteEvenementDto GetById(int id)
        {
            var evenement = _context.Evenements
                .AsNoTracking()
                .Include(e => e.Ville)
                .Include(e => e.Categories)
                .FirstOrDefault(c => c.Id == id);

            if (evenement is not null)
            {
                return new RequeteEvenementDto
                {
                    Id = evenement.Id,
                    Titre = evenement.Titre,
                    AdresseCivique = evenement.AdresseCivique,
                    DateDebut = evenement.DateDebut,
                    DateFin = evenement.DateFin,
                    NomOrganisateur = evenement.NomOrganisateur,
                    Description = evenement.Description,
                    Prix = evenement.Prix,
                    Ville = evenement.Ville.Nom,
                    Region = evenement.Ville.Region.ToString(),
                    Categories = evenement.Categories.Select(c => c.Nom).ToList()
                };
            }

            return null;
        }

        public RequeteEvenementDto Add(EnvoiEvenementDto evenement)
        {
            var categories = _context.Categories
                .Where(c => evenement.CategoriesId.Contains(c.Id))
                .ToList();

            var ville = _context.Villes
                .FirstOrDefault(v => v.Id == evenement.VilleId);
         
            var entite = new Evenement
            {
                Titre = evenement.Titre,
                AdresseCivique = evenement.AdresseCivique,
                DateDebut = evenement.DateDebut,
                DateFin = evenement.DateFin,
                Description = evenement.Description,
                NomOrganisateur = evenement.NomOrganisateur,
                Prix = evenement.Prix,
                Ville = ville,
                Categories = categories,
            };
            
            _context.Evenements.Add(entite);
            _context.SaveChanges();

            return new RequeteEvenementDto
            {
                Id = entite.Id,
                Titre = entite.Titre,
                AdresseCivique = entite.AdresseCivique,
                DateDebut = entite.DateDebut,
                DateFin = entite.DateFin,
                NomOrganisateur = entite.NomOrganisateur,
                Description = entite.Description,
                Prix = entite.Prix,
                Ville = entite.Ville.Nom,
                Region = entite.Ville.Region.ToString(),
                Categories = entite.Categories.Select(c => c.Nom).ToList()
            };
        }

        public void Update(RequeteEvenementDto evenement)
        {
            var categories = _context.Categories
                .AsNoTracking()
                .Where(c => evenement.Categories.Contains(c.Nom))
                .ToList();

            var ville = _context.Villes
                .AsNoTracking()
                .FirstOrDefault(v => v.Nom == evenement.Ville && v.Region.ToString() == evenement.Region);
            
            var evenementToUpdate = _context.Evenements.FirstOrDefault(e => e.Id == evenement.Id);

            evenementToUpdate.AdresseCivique = evenement.AdresseCivique;
            evenementToUpdate.Titre = evenement.Titre;
            evenementToUpdate.DateDebut = evenement.DateDebut;
            evenementToUpdate.DateFin = evenement.DateFin;
            evenementToUpdate.Description = evenement.Description;
            evenementToUpdate.Prix = evenement.Prix;
            evenementToUpdate.Categories = categories;
            evenementToUpdate.Ville = ville;

            _context.Evenements.Update(evenementToUpdate);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Evenements.FirstOrDefault(c => c.Id == id);

            _context.Evenements.Remove(entity);
            _context.SaveChanges();
        }
    }
}