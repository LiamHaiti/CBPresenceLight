
namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Commentaire
    {

        public int CommentaireId { get; set; }
        public int PublicationId { get; set; }
        public string Description { get; set; }
        public System.DateTime DateComment { get; set; }
     }
}
