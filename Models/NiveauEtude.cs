namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NiveauEtude
    {

        public int NiveauEtudeId { get; set; }
        public string Description { get; set; }
        public Nullable<int> Ordre { get; set; }
        public Nullable<bool> Statut { get; set; }
        public string ModifierPar { get; set; }
        public Nullable<System.DateTime> ModifierDate { get; set; }
    
    }
}
