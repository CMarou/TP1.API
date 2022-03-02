using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TP1.API.Data.Models;

namespace TP1.API.Data.Repository
{
    public class EvenementRepository : IEvenementRepository
    {
        private readonly ApplicationContext _context;

        public EvenementRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Evenement> GetList(int pageIndex, int pageSize)
        {
            int skipAmount = (pageIndex - 1) * pageSize;

            return _context.Evenements
                .AsNoTracking()
                .Include(e => e.Ville)
                .Include(e => e.Categories)
                .OrderBy(e => e.DateDebut)
                .Skip(skipAmount)
                .Take(pageSize);
        }

        public IEnumerable<Evenement> GetList(Expression<Func<Evenement, bool>> predicate, int pageIndex, int pageSize)
        {
            int skipAmount = (pageIndex - 1) * pageSize;

            return _context.Evenements
                .AsNoTracking()
                .Include(e => e.Ville)
                .Include(e => e.Categories)
                .OrderBy(e => e.DateDebut)
                .Where(predicate)
                .Skip(skipAmount)
                .Take(pageSize);
        }

        public IEnumerable<Participation> GetParticipationsForEvent(int eventId)
        {
            return _context.Participations
                .AsNoTracking()
                .Include(p => p.Evenement)
                .Where(e => e.Id == eventId);      
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
              
            total *= myEvent.Prix ?? 0;

            return total;
        }

        public Evenement GetById(int id)
        {
            return _context.Evenements
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);
        }

        public void Add(Evenement entity)
        {
            _context.Evenements.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Evenement entity)
        {
            _context.Evenements.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Evenements.FirstOrDefault(c => c.Id == id);

            if (entity is null)
                throw new DbUpdateException("Entity to delete was not found.");

            _context.Evenements.Remove(entity);
            _context.SaveChanges();
        }
    }
}