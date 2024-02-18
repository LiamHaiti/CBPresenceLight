using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBPresenceLight.Models
{

    public class UserVM
    {
        public User User { get; set; }
        public List<RolePermissionVM> Permissions { get; set; }
        public List<AppNavigation> AppNavigations { get; set; }
        public List<AppNavigationPermissionVM> AppNavigationPermissions { get; set; }
    }

    public class RolePermissionVM
    {
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public bool FullPermission { get; set; }
        public int ApplicationId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool SuperRole { get; set; }
        public string ObjectName { get; set; }
        public string ParentName { get; set; }
        public string Label { get; set; }
        public string ApplicationName { get; set; }
    }

    public class AppNavigationPermissionVM
    {
        public int AppNavigationPermissionId { get; set; }
        public int AppNavigationId { get; set; }
        public int PermissionId { get; set; }
        public Nullable<int> PermissionOrder { get; set; }
        public string ObjectName { get; set; }
        public string ParentName { get; set; }
        public string Label { get; set; }
        public string NavigationLabel { get; set; }
        public int NavigationLevel { get; set; }
        public int NavigationOrder { get; set; }
    }


    public class AppNavigationApplicationVM
    {
        public int AppNavigationApplicationId { get; set; }
        public int ApplicationId { get; set; }
        public int AppNavigationId { get; set; }
        public string ApplicationName { get; set; }
        public int CompagnieId { get; set; }
        public string Description { get; set; }
        public string NomCompagnie { get; set; }
        public string NIF { get; set; }
        public string NavigationLabel { get; set; }
        public int NavigationLevel { get; set; }
        public int NavigationOrder { get; set; }
    }
    
    public class ApplicationCompagnieVM
    {
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public int CompagnieId { get; set; }
        public string Description { get; set; }
        public string NomCompagnie { get; set; }
        public string NIF { get; set; }
        
    }

    
    public class UserAppNavigationPermissionVM
    {
        public int AppNavigationId { get; set; }
        public String NavigationLabel { get; set; }
          
    }
     public class UserRoleVM
    {

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string LoweredUserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string LoweredEmail { get; set; }
        public bool IsLockedOut { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilPhoto { get; set; }
        public bool SuperUser { get; set; }
        public string SessionId { get; set; }
        public string OTP { get; set; }
        public Nullable<bool> Authenticated { get; set; }
        public Nullable<System.DateTime> AuthenticationDate { get; set; }
        public string ModifiePar { get; set; }
        public Nullable<System.DateTime> ModifieDate { get; set; }
        public string RoleName { get; set; }


    }
    public class UserProfileVM
    {
        public int UserProfileId { get; set; }
        public int MobileUserId { get; set; }
        public string Image { get; set; }
        public Nullable<bool> Statut { get; set; }
        public string LieuPublication { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public System.DateTime DatePublication { get; set; }
        public string ModifierPar { get; set; }
        public Nullable<System.DateTime> ModifierDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccessCode { get; set; }
        public int InstitutionId { get; set; }
    }

    public partial class UserPublicationVM
    {
        public int UserPublicationId { get; set; }
        public int MobileUserId { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string LieuPublication { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public System.DateTime DatePublication { get; set; }
        public string ModifierPar { get; set; }
        public Nullable<System.DateTime> ModifierDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccessCode { get; set; }
        public int InstitutionId { get; set; }
    }


    public partial class PublicationVM
    {
        public int PublicationId { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public System.DateTime DatePoste { get; set; }
        public Nullable<int> EntrepriseId { get; set; }
        public Nullable<int> ProprietaireId { get; set; }
        public Nullable<int> TypeEntrepriseId { get; set; }
        public Nullable<int> CommuneId { get; set; }
        public Nullable<int> PaysId { get; set; }
        public string Telephone { get; set; }
        public string CourrierElectronique { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public bool? Statut { get; set; }
        public string Adresse { get; set; }
        public string TypeEntrepriseDescription { get; set; }
        public string Description { get; set; }
        public string Proprietaire { get; set; }
        public bool? StatutDelete { get; set; }
        public DateTime? DateDelete { get; set; }
        public ICollection<PublicationFichierVM> PublicationFichier { get; set; }
        public ICollection<Commentaire> Commentaire { get; set; }
        public int QuantiteShared { get; set; }
        public int QuantiteCommentaire { get; set; }
        public int QuantiteLiked { get; set; }
        public int QuantiteDisLiked { get; set; }

    }


    public partial class ProprietaireVM
    {

        public int ProprietaireId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Sexe { get; set; }
        public string AccessKey { get; set; }
        public Nullable<int> PaysId { get; set; }
        public string LieuDeNaissance { get; set; }
        public string Telephone { get; set; }
        public string PaysDescription { get; set; }
        public string Code { get; set; }
        public System.DateTime DateDeNaissance { get; set; }
        public string Email { get; set; }
        public Nullable<int> CommuneId { get; set; }
        public string CommuneDescription { get; set; }
        public string CodeCommune { get; set; }
        public Nullable<int> ArrondissementId { get; set; }
        public string ArrondissementDescription { get; set; }
        public string DepartementDescription { get; set; }
        public bool? Statut { get; set; }
        public DateTime? ModifierDate { get; set; }
        public string Photo { get; set; }
    }
    
    public partial class PublicationFichierVM
    {
        public int PublicationFichierId { get; set; }
        public int PublicationId { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
       
    }

}
