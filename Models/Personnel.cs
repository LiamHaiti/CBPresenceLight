namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Personnel
    {

        public int PersonnelId { get; set; }
        public int EntrepriseId { get; set; }
        public int TypePersonnelId { get; set; }
        public Nullable<int> NiveauEtudeId { get; set; }
        public string Specialite { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NINU { get; set; }
        public string CIN { get; set; }
        public string CourrierElectronique { get; set; }
        public Nullable<int> SexeId { get; set; }
        public System.DateTime DateDeNaissance { get; set; }
        public int PaysId { get; set; }
        public string LieuDeNaissance { get; set; }
        public Nullable<int> CommuneId { get; set; }
        public string Adresse { get; set; }
        public string ModifierPar { get; set; }
        public Nullable<System.DateTime> ModifierDate { get; set; }
    
    }
}
