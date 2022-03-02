using System.Collections.Generic;

namespace TP1.API.Data.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public ICollection<Evenement> Evenements { get; set; }
    }
}
