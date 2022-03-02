using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TP1.API.Data.Models;

namespace TP1.API.Data.Repository
{
    public class CategorieRepository : ICategorieRepository
    {
        private readonly ApplicationContext _context;

        public CategorieRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Categorie> GetList()
        {
            return _context.Categories.AsNoTracking();
        }

        public IEnumerable<Categorie> GetList(Expression<Func<Categorie, bool>> predicate)
        {
            return _context.Categories
                .AsNoTracking()
                .Where(predicate);
        }

        public Categorie GetById(int id)
        {
            return _context.Categories
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);
        }

        public void Add(Categorie entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Categorie entity)
        {
            _context.Categories.Update(entity);
            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            var entity = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (entity is null)
                throw new DbUpdateException("Entity to delete was not found.");

            _context.Categories.Remove(entity);
            _context.SaveChanges();
        }
    }
}
