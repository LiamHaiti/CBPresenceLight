namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Proprietaire
    {
        public int ProprietaireId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NINU { get; set; }
        public string CIN { get; set; }
        public string Sexe { get; set; }
        public string AccessKey { get; set; }
        public System.DateTime DateDeNaissance { get; set; }
        public int? PaysId { get; set; }
        public string LieuDeNaissance { get; set; }
        public Nullable<int> CommuneId { get; set; }
        public string Password { get; set; }
        public int? PasswordAttempt { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Adresse{ get; set; }
        public bool? Statut { get; set; }
    }
}
