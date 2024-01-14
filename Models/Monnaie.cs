
namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Monnaie
    {

        public int MonnaieId { get; set; }
        public bool MonnaieDeBase { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
