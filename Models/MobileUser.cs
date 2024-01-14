using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBPresenceLight.Models
{
    public class MobileUser
    {
        public int MobileUserId { get; set; }
        public int InstitutionId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String AccessCode { get; set; }
        public String NoSerie { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }
        public String ModifierPar { get; set; }
        public String ModifierDate { get; set; }
        public Institution Institution { get; set;}
    }
}
