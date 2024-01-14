namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Institution
    {

        public int InstitutionId { get; set; }
        public string NomInstitution { get; set; }
        public string SigleInstitution { get; set; }
        public string MinistereTutelle { get; set; }
        public string Telephone { get; set; }
        public string AdresseElectronique { get; set; }
        public string NomResponsable { get; set; }
        public string ModifiePar { get; set; }
        public Nullable<System.DateTime> ModifieDate { get; set; }
    }
}
