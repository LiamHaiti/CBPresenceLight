namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TypePersonnel
    {
        public int TypePersonnelId { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Statut { get; set; }
        public string ModifierPar { get; set; }
        public Nullable<System.DateTime> ModifierDate { get; set; }
    }
}
