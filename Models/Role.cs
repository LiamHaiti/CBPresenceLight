namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Role
    {
        public int RoleId { get; set; }
        public int ApplicationId { get; set; }
        public string RoleName { get; set; }
        public string LoweredRoleName { get; set; }
        public string Description { get; set; }
        public bool SuperRole { get; set; }
        public string ModifiePar { get; set; }
        public Nullable<System.DateTime> ModifieDate { get; set; }
    
    }
}
