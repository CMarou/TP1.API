using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TP1.API.Data.Models;
using TP1.API.DTOs;

namespace TP1.API.Data.Repository
{
    public class VilleRepository : IVilleRepository
    {
        private readonly ApplicationContext _context;

        public VilleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<RequeteVilleDto> GetList()
        {
            return _context.Villes
                .AsNoTracking()
                .Select(v => new RequeteVilleDto 
                {
                    Id = v.Id,
                    Nom = v.Nom,
                    Region = v.Region,
                });
        }

        public IEnumerable<RequeteVilleDto> GetList(Expression<Func<RequeteVilleDto, bool>> predicate)
        {
            return _context.Villes
                .AsNoTracking()
                .Select(v => new RequeteVilleDto
                {
                    Id = v.Id,
                    Nom = v.Nom,
                    Region = v.Region,
                })
                .Where(predicate);
        }
        
        public IEnumerable<RequeteVilleDto> GetCitiesByEventCountDescending()
        {
            return _context.Villes
                .Select(v => new RequeteVilleDto
                {
                    Id = v.Id,
                    Nom = v.Nom,
                    Region = v.Region,
                })
                .OrderByDescending(v => _context.Evenements.Count(e => e.Ville.Id == v.Id));
        }

        public IEnumerable<VilleEvenementDto> GetEventsForCity(int cityId)
        {
            return _context.Evenements
                .AsNoTracking()
                .Include(e => e.Ville)
                .Include(e => e.Categories)
                .Where(e => e.Ville.Id == cityId)
                .Select(e => new VilleEvenementDto 
                { 
                    Id = e.Id,
                    Titre = e.Titre,
                    AdresseCivique = e.AdresseCivique,
                    DateDebut = e.DateDebut,
                    DateFin = e.DateFin,
                    NomOrganisateur = e.NomOrganisateur,
                    Description = e.Description,
                    Prix = e.Prix,
                    Categories = e.Categories.Select(c => c.Nom).ToList()
                });
        }

        public RequeteVilleDto GetById(int id)
        {
            var ville = _context.Villes
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);

            if (ville is not null)
            {
                return new RequeteVilleDto
                {
                    Id = ville.Id,
                    Nom = ville.Nom,
                    Region = ville.Region,
                };
            }

            return null;
        }

        public RequeteVilleDto Add(EnvoiVilleDto ville)
        {
            var entite = new Ville
            {
                Nom = ville.Nom,
                Region = ville.Region
            };
            
            _context.Villes.Add(entite);
            _context.SaveChanges();

            return new RequeteVilleDto
            {
                Id = entite.Id,
                Nom = entite.Nom,
                Region = entite.Region,
            };
        }

        public void Update(RequeteVilleDto ville)
        {
            var villeToUpdate = _context.Villes.FirstOrDefault(c => c.Id == ville.Id);

            villeToUpdate.Nom = ville.Nom;
            villeToUpdate.Region = ville.Region;
            
            _context.Villes.Update(villeToUpdate);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Villes.FirstOrDefault(c => c.Id == id);

            _context.Villes.Remove(entity);
            _context.SaveChanges();
        }
    }
}
