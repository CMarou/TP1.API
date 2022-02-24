using System.Collections.Generic;
using TP1.API.Data.Models;

namespace TP1.API.Interfaces
{
    public interface ICategoriesService
    {
        IEnumerable<Categorie> GetList();
        Categorie GetById(int id);
        Categorie Add(Categorie categorie);
        Categorie Update(int id, Categorie categorie);
        void Delete(int id);
    }
}
