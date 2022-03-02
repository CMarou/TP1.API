﻿using System;
using System.Collections.Generic;

namespace TP1.API.DTOs
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

    public class RequeteVilleDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public Region Region { get; set; }
    }

    public class VilleEvenementDto
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string NomOrganisateur { get; set; }
        public string Description { get; set; }
        public string AdresseCivique { get; set; }
        public List<string> Categories { get; set; }
        public double? Prix { get; set; }
    }

    public class EnvoiVilleDto
    {
        public string Nom { get; set; }
        public Region Region { get; set; }
    }
}
