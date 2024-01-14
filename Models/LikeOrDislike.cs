
namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LikeOrDislike
    {

        public int LikeOrDislikeId { get; set; }
        public Nullable<bool> Statut { get; set; }
        public System.DateTime DateLike { get; set; }
    }
}
