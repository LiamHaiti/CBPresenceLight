namespace CBPresenceLight.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string LoweredEmail { get; set; }
        public bool IsLockedOut { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public string ProfilPhoto { get; set; }
        public bool SuperUser { get; set; }
        public string SessionId { get; set; }
        public string OTP { get; set; }
        public Nullable<bool> Authenticated { get; set; }
        public Nullable<System.DateTime> AuthenticationDate { get; set; }
        public string ModifiePar { get; set; }
        public Nullable<System.DateTime> ModifieDate { get; set; }
    }
}
