namespace CBPresenceLight.Models
{
    using System;
    
    public partial class Medicament
    {
        public int MedicamentId { get; set; }
        public int TypeMedicamentId { get; set; }
        public int MonnaieId { get; set; }
        public int EntrepriseId { get; set; }
        public string Description { get; set; }
        public string BNO { get; set; }
        public System.DateTime DateExpiration { get; set; }
        public Nullable<decimal> Prix { get; set; }
        public Nullable<int> QuantiteEnregistre { get; set; }
        public Nullable<int> QuantiteDisponible { get; set; }
    
    }
}
