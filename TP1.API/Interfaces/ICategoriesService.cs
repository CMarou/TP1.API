using System.Collections.Generic;
using TP1.API.DTOs;

namespace TP1.API.Interfaces
{
    public interface ICategoriesService
    {
        IEnumerable<RequeteCategorieDto> GetList();
        RequeteCategorieDto GetById(int id);
        RequeteCategorieDto Add(string nomCategorie);
        void Update(int id, RequeteCategorieDto nomCategorie);
        void Delete(int id);
    }
}
