using System;
using System.Collections.Generic;

namespace TP1.API.DTOs
{
    public class RequeteEvenementDto
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string NomOrganisateur { get; set; }
        public string Description { get; set; }
        public string AdresseCivique { get; set; }
        public string Ville { get; set; }
        public string Region { get; set; }
        public List<string> Categories { get; set; }
        public double? Prix { get; set; }
    }

    public class EvenementParticipationDto
    {
        public int Id { get; set; }
        public string AdresseCourriel { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Places { get; set; }
    }

    public class EnvoiEvenementDto
    {
        public string Titre { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string NomOrganisateur { get; set; }
        public string Description { get; set; }
        public string AdresseCivique { get; set; }
        public int VilleId { get; set; }
        public List<int> CategoriesId { get; set; }
        public double? Prix { get; set; }
    }
}