using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TP1.API.Data.Models;


namespace TP1.API.Data.Repository
{

    public class VilleRepository : IVilleRepository
    {
        private readonly ApplicationContext _context;

        public VilleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Ville> GetList()
        {
            return _context.Villes.AsNoTracking();
        }

        public IEnumerable<Ville> GetList(Expression<Func<Ville, bool>> predicate)
        {
            return _context.Villes
                .AsNoTracking()
                .Where(predicate);
        }

        public IEnumerable<Ville> GetCitiesByEventCountDescending()
        {
            var countList = _context.Evenements
                .AsNoTracking()
                .Include(e => e.Ville)
                .GroupBy(e => e.Ville.Id)
                .Select(g => new { Count = g.Count(), VilleId = g.ElementAt(g.Key).Ville.Id });

            var test = _context.Villes
                .AsNoTracking()
                .Join(countList, v => v.Id, c => c.VilleId, x => new { c.Count, v.Id, v.Nom, v.Region })
                .OrderByDescending(v => v.Count);

            throw new NotImplementedException();
        }

        public IEnumerable<Evenement> GetEventsForCity(int cityId)
        {
            return _context.Evenements
                .AsNoTracking()
                .Include(e => e.Ville)
                .Where(e => e.Ville.Id == cityId);
        }

        public Ville GetById(int id)
        {
            return _context.Villes
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);
        }

        public void Add(Ville entity)
        {
            _context.Villes.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Ville entity)
        {
            _context.Villes.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Villes.FirstOrDefault(c => c.Id == id);

            if (entity is null)
                throw new DbUpdateException("Entity to delete was not found.");

            _context.Villes.Remove(entity);
            _context.SaveChanges();
        }
    }
}
