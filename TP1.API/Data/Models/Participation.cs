namespace TP1.API.Data.Models
{
    public class Participation
    {
        public int Id { get; set; }
        public string AdresseCourriel { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int NombrePlace { get; set; }
        public bool EstValide { get; set; }
        public int IdEvenement { get; set; }

    }   
}
