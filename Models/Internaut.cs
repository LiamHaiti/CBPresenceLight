namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Internaut
    {
    
        public int InternautId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Sexe { get; set; }
        public Nullable<System.DateTime> DateDeNaissance { get; set; }
        public Nullable<int> PaysDeNaissanceId { get; set; }
        public string LieuDeNaissance { get; set; }
        public Nullable<int> CommuneId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    
    }
}
