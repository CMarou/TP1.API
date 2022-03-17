using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TP1.API.Data.Models;
using TP1.API.DTOs;

namespace TP1.API.Data.Repository
{
    public class CategorieRepository : ICategorieRepository
    {
        private readonly ApplicationContext _context;

        public CategorieRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<RequeteCategorieDto> GetList()
        {
            return _context.Categories
                .AsNoTracking()
                .Select(c => new RequeteCategorieDto { Id = c.Id, Nom = c.Nom });
        }

        public IEnumerable<RequeteCategorieDto> GetList(Expression<Func<RequeteCategorieDto, bool>> predicate)
        {
            return _context.Categories
                .AsNoTracking()
                .Select(c => new RequeteCategorieDto { Id = c.Id, Nom = c.Nom })
                .Where(predicate);
        }

        public RequeteCategorieDto GetById(int id)
        {
            var categorie = _context.Categories
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);
            
            if (categorie is not null)
            {
                return new RequeteCategorieDto
                {
                    Id = categorie.Id,
                    Nom = categorie.Nom
                };
            }

            return null;
        }

        public RequeteCategorieDto Add(string nomCategorie)
        {
            var entite = new Categorie { Nom = nomCategorie };

            _context.Categories.Add(entite);
            _context.SaveChanges();

            return new RequeteCategorieDto
            {
                Id = entite.Id,
                Nom = entite.Nom
            };
        }

        public void Update(RequeteCategorieDto categorie)
        {
            var categorieToUpdate = _context.Categories.FirstOrDefault(c => c.Id == categorie.Id);

            categorieToUpdate.Nom = categorie.Nom;

            _context.Categories.Update(categorieToUpdate);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Categories.FirstOrDefault(c => c.Id == id);

            _context.Categories.Remove(entity);
            _context.SaveChanges();
        }

    }
}
