
namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Entreprise
    {

        public int EntrepriseId { get; set; }
        public int ProprietaireId { get; set; }
        public int TypeEntrepriseId { get; set; }
        public Nullable<int> CommuneId { get; set; }
        public int PaysId { get; set; }
        public string Description { get; set; }
        public string NoPatente { get; set; }
        public string CourrierElectronique { get; set; }
        public string Telephone { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public bool? Statut { get; set; }
        public string Adresse { get; set; }
        public bool? StatutDelete { get; set; }
        public DateTime? DateDelete { get; set; }
        public string ModifierPar { get; set; }
        public Nullable<System.DateTime> modifierDate { get; set; }
    
    }
}
