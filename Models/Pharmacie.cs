namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pharmacie
    {
        public int PharmacieId { get; set; }
        public int PharmacienId { get; set; }
        public int ProprietaireId { get; set; }
        public string Description { get; set; }
        public string NoPatente { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public Nullable<bool> Statut { get; set; }
    
    }
}
