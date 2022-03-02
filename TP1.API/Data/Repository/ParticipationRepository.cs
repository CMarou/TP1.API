using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TP1.API.Data.Models;

namespace TP1.API.Data.Repository
{
    public class ParticipationRepository : IParticipationRepository
    {
        private readonly ApplicationContext _context;

        public ParticipationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Participation> GetList()
        {
            return _context.Participations
                .AsNoTracking()
                .Include(p => p.Evenement);
        }

        public IEnumerable<Participation> GetList(Expression<Func<Participation, bool>> predicate)
        {
            return _context.Participations
                .AsNoTracking()
                .Include(p => p.Evenement)
                .Where(predicate);
        }

        public Participation GetById(int id)
        {
            return _context.Participations
                .AsNoTracking()
                .Include(p => p.Evenement)   
                .FirstOrDefault(p => p.Id == id);
        }

        public void Add(Participation entity)
        {
            _context.Participations.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Participation entity)
        {
            _context.Participations.Update(entity);
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
    }
}
