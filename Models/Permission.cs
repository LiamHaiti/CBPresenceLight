namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Permission
    {

        public int PermissionId { get; set; }
        public string ObjectName { get; set; }
        public string ParentName { get; set; }
        public string Label { get; set; }
    }
}
