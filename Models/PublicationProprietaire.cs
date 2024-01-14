namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PublicationProprietaire
    {
        public int PublicationProprietaireId { get; set; }
        public Nullable<int> PublicationId { get; set; }
        public Nullable<int> EntrepriseId { get; set; }
    
    }
}
