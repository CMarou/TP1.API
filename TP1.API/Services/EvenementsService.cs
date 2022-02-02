using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TP1.API.Interfaces;
using TP1.API.Models;

namespace TP1.API.Services
{
    public class EvenementsServices : IEvenementsService
    {
        private readonly IHttpExceptionThrower _exceptionThrower;

        public EvenementsServices(IHttpExceptionThrower exceptionThrower)
        {
            _exceptionThrower = exceptionThrower;
        }

        public IEnumerable<Evenement> GetList()
        {
            //return Repository
        }

        public IEnumerable<Evenement> GetList(Expression<Func<Evenement, bool>> predicat)
        {
            throw new NotImplementedException();
        }

        public Evenement GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Evenement Add(Evenement evenement)
        {
            throw new NotImplementedException();
        }

        public Evenement Update(int id, Evenement evenement)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        private List<string> Valider(Evenement evenement)
        {
            var erreurs = new List<string>();

            return erreurs;
        }
    }
}