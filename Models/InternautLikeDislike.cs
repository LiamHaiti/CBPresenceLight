namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class InternautLikeDislike
    {
        public int InternautLikeDislikeId { get; set; }
        public Nullable<int> InternautId { get; set; }
        public int LikeOrDislikeId { get; set; }
        public Nullable<int> EntrepriseId { get; set; }
        public Nullable<int> ProprietaireId { get; set; }
    
    }
}
