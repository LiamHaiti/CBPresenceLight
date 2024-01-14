namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Publication
    {
        public int PublicationId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public bool? StatutDelete { get; set; }
        public System.DateTime? DateDelete { get; set; }
        public System.DateTime? DatePoste { get; set; }
     }
}
