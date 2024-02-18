using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CBPresenceLight.Models
{

    public class Login
    {
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Password { get; set; }
    }


    public class EntrepriseList
    {
        public TypeEntreprise TypeEntreprise { get; set; }
        public ICollection<EntrepriseApi> Entreprise { get; set; }
    }

    public class PublicationList
    {
       public PublicationVM Publication { get; set; }
       // public ICollection<Commentaire> Commentaire { get; set; }
       // public ICollection<PublicationFichierVM> PublicationFichier { get; set; }
        //public int QuantiteCommentaire { get; set; }
        //public int QuantiteShared { get; set; }
//public int Quantite { get; set; }
       // public int QuantiteLiked { get; set; }
       // public int QuantiteDisLiked { get; set; }
        
    }
     
    public class EntrepriseApi
    {

        public int EntrepriseId { get; set; }
        public int ProprietaireId { get; set; }
        public int TypeEntrepriseId { get; set; }
        public Nullable<int> CommuneId { get; set; }
        public int PaysId { get; set; }
        public string Description { get; set; }
        public string NoPatente { get; set; }
        public string CourrierElectronique { get; set; }
        public string Telephone { get; set; }
        public string Longetude { get; set; }
        public string Latitude { get; set; }
        public string Statut { get; set; }
        public string Adresse { get; set; }
        public string ModifierPar { get; set; }
        public Nullable<System.DateTime> modifierDate { get; set; }
    }

    public class PublicationApi
    {
        public int Id { get; set; }
        public int PublicationId { get; set; }
        public int? ProprietaireId { get; set; }
        public int? EntrepriseId { get; set; }
        public int TypeProprietaire { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
    }

    public class LikeOrDislikeApi
    {
        public int Id { get; set; }
        public int LikeOrDislikeId { get; set; }
        public int PublicationId { get; set; }
        public bool Statut { get; set; }   
    }

    public class LikeOrDislikes
    {
        public ICollection<LikeOrDislikeApi> LikeOrDislike { get; set; }
    }
    
    public class Publications
    {
        public ICollection<PublicationApi> Publication { get; set; }
    }
}