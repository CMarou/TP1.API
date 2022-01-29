using System;

namespace TP1.API.Models
{
    public class Evenement
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string NomOrganisateur { get; set; }
        //public List<Categorie> Categories { get; set; }
        public string Description { get; set; }
        public string AdresseCivique { get; set; }
        public int IdVille { get; set; }
    }
}
