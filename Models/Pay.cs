namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pay
    {

        public int PaysId { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool? PaysEtranger { get; set; }
        public bool? Statut { get; set; }
    
       
    }
}
