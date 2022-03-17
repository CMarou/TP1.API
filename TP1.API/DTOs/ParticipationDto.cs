namespace TP1.API.DTOs
{
    public class RequeteParticipationDto
    {
        public int Id { get; set; }
        public string AdresseCourriel { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Places { get; set; }
        public int EvenementId { get; set; }
        public string TitreEvenement { get; set; }
    }

    public class EnvoiParticipationDto
    {
        public string AdresseCourriel { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int NombrePlace { get; set; }
        public int EvenementId { get; set; }
    }
}
