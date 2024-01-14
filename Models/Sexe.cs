namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sexe
    {
        public int SexeId { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string ModifierPar { get; set; }
        public Nullable<System.DateTime> ModifierDate { get; set; }
    }
}
