namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TypeEntreprise
    {
        public int TypeEntrepriseId { get; set; }
        public string Description { get; set; }
        public string ModifierPar { get; set; }
        public Nullable<System.DateTime> ModierDate { get; set; }
        }
}
