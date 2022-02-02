using System.Collections.Generic;
using TP1.API.Models;

namespace TP1.API.Data
{
    public static class Repository
    {
        public static int IdSequenceVille = 1;
        public static int IdSequenceCategorie = 1;
        public static int IdSequenceEvenement = 1;
        public static int IdSequenceParticipation = 1;
        public static int IdSequenceCategorieEvenement = 1;

        public static ISet<Ville> Villes { get; private set; } = new HashSet<Ville>();
        public static ISet<Categorie> Categories { get; private set; } = new HashSet<Categorie>();
        public static ISet<Evenement> Evenements { get; private set; } = new HashSet<Evenement>();
        public static ISet<Participation> Participations { get; private set; } = new HashSet<Participation>();
        public static ISet<CategorieEvenement> CategorieEvenements { get; private set; } = new HashSet<CategorieEvenement>();
    }
}
