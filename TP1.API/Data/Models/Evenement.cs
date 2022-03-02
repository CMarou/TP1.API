using System;
using System.Collections.Generic;

namespace TP1.API.Data.Models
{
    public class Evenement
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string NomOrganisateur { get; set; }
        public string Description { get; set; }
        public string AdresseCivique { get; set; }
        public double? Prix { get; set; }
        
        public Ville Ville { get; set; }
        
        public ICollection<Categorie> Categories { get; set; }
        public ICollection<Participation> Participations { get; set; }

    }
    
}
