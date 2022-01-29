namespace TP1.API.Models
{
    public enum Region
    {
        Aucune,
        BasStLaurent,
        Gaspesie,
        CapitaleNationale,
        Monteregie,
        Estrie,
        Saguenay,
        NordDuQuebec,
        ChaudiereAppalaches,
        Mauricie,
        CentreDuQuebec,
        IleMontreal,
        Laval,
        Lanaudiere,
        Laurentides,
        Outaouais,
        Abitibi,
        CoteNord
    }

    public class Ville
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public Region Region { get; set; }
    }
}
