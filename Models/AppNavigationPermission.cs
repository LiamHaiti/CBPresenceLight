
namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AppNavigationPermission
    {
        public int AppNavigationPermissionId { get; set; }
        public int AppNavigationId { get; set; }
        public int PermissionId { get; set; }
        public Nullable<int> PermissionOrder { get; set; }
    
        public virtual AppNavigation AppNavigation { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
