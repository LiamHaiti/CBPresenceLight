namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class InternautCommentaire
    {
        public int InternautCommentaireId { get; set; }
        public int CommentaireId { get; set; }
        public Nullable<int> InteranautId { get; set; }
        public Nullable<int> EntrepriseId { get; set; }
        public Nullable<int> ProprietaireId { get; set; }
    
    }
}
