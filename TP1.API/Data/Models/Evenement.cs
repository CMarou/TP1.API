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
        public List<int> CategoriesId { get; set; }
        public int IdVille { get; set; }
        

        private bool Equals(Evenement autre)
        {
            return Titre == autre.Titre
                && DateDebut == autre.DateDebut
                && NomOrganisateur == autre.NomOrganisateur
                && AdresseCivique == autre.AdresseCivique
                && IdVille == autre.IdVille;
        }

        public override bool Equals(object obj)
        {
            return obj is Evenement evenement 
                && Equals(evenement);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
