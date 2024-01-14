namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PersonnelHoraire
    {
        public int PersonnelHoraireId { get; set; }
        public int HoraireId { get; set; }
        public int Personneld { get; set; }
        public string Description { get; set; }
    
    }
}
