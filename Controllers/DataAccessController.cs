﻿using CBPresenceLight.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CBPresenceLight.Controllers
{
    public class DataAccessController : Controller
    {

        //private readonly IHostingEnvironment _webHostingEnvironment;
        private readonly IWebHostEnvironment _webHostingEnvironment;
        private readonly IConfiguration _config;
        private readonly string _dataSource;


        public DataAccessController(IWebHostEnvironment webHostingEnvironment, IConfiguration config)
        {
            _webHostingEnvironment = webHostingEnvironment;
            _config = config;
            _dataSource = Convert.ToString(_config.GetValue<string>("CBPresence"));
        }


        public List<User> GetUsers()
        {


            var data = new List<User>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();

                    String sql = @"SELECT UserId,
RoleId,
FirstName,
LastName,
Password,
Email	,
LoweredEmail,
IsLockedOut,
CreateDate,
LastLoginDate,
FailedPasswordAttemptCount,
SuperUser,
SessionId,
OTP	,
Authenticated,
AuthenticationDate,
ModifiePar,
ModifieDate FROM [dbo].[User]";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data.Add(new User
                                {
                                    UserId = (int)reader["UserId"],
                                    RoleId = (int)reader["RoleId"],
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"],
                                    Password = (string)reader["Password"],
                                    Email = (string)reader["Email"],
                                    LoweredEmail = !Convert.IsDBNull(reader["LoweredEmail"]) ? (string)reader["LoweredEmail"] : null,
                                    IsLockedOut = (bool)reader["IsLockedOut"],
                                    CreateDate = (DateTime)reader["CreateDate"],
                                    LastLoginDate = !Convert.IsDBNull(reader["LastLoginDate"]) ? (DateTime?)reader["LastLoginDate"] : null,
                                    FailedPasswordAttemptCount = (int)reader["FailedPasswordAttemptCount"],
                                    SuperUser = (bool)reader["SuperUser"],
                                    SessionId = !Convert.IsDBNull(reader["SessionId"]) ? (string)reader["SessionId"] : null,
                                    OTP = !Convert.IsDBNull(reader["OTP"]) ? (string)reader["OTP"] : null,
                                    Authenticated = !Convert.IsDBNull(reader["Authenticated"]) ? (bool?)reader["Authenticated"] : null,
                                    AuthenticationDate = !Convert.IsDBNull(reader["AuthenticationDate"]) ? (DateTime?)reader["AuthenticationDate"] : null,
                                    ModifieDate = !Convert.IsDBNull(reader["ModifieDate"]) ? (DateTime?)reader["ModifieDate"] : null,
                                    ModifiePar = !Convert.IsDBNull(reader["ModifiePar"]) ? (string)reader["ModifiePar"] : null,


                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }


        public List<UserRoleVM> GetUsers(int compagnieId)
        {

            var data = new List<UserRoleVM>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();

                    String sql = @"
SELECT UserId,
RoleId,
FirstName,
LastName,
Password,
UserName,
LoweredUserName	,
Email	,
LoweredEmail,
IsLockedOut,
CreateDate,
LastLoginDate,
FailedPasswordAttemptCount,
SuperUser,
SessionId,
OTP	,
Authenticated,
AuthenticationDate,
ModifiePar,
ModifieDate,RoleName 
FROM [dbo].[vwUserRole] u WHERE (SELECT COUNT(*) FROM UserCompagnie uc WHERE uc.UserId=u.UserId AND uc.CompagnieId=@CompagnieId)>0";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("CompagnieId", compagnieId);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data.Add(new UserRoleVM
                                {
                                    UserId = (int)reader["UserId"],
                                    RoleId = (int)reader["RoleId"],
                                    UserName = (string)reader["UserName"],
                                    LoweredUserName = !Convert.IsDBNull(reader["LoweredUserName"]) ? (string)reader["LoweredUserName"] : null,
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"],
                                    Password = (string)reader["Password"],
                                    Email = (string)reader["Email"],
                                    LoweredEmail = (string)reader["LoweredEmail"],
                                    IsLockedOut = (bool)reader["IsLockedOut"],
                                    CreateDate = (DateTime)reader["CreateDate"],
                                    LastLoginDate = !Convert.IsDBNull(reader["LastLoginDate"]) ? (DateTime?)reader["LastLoginDate"] : null,
                                    FailedPasswordAttemptCount = (int)reader["FailedPasswordAttemptCount"],
                                    SuperUser = (bool)reader["SuperUser"],
                                    SessionId = !Convert.IsDBNull(reader["SessionId"]) ? (string)reader["SessionId"] : null,
                                    OTP = !Convert.IsDBNull(reader["OTP"]) ? (string)reader["OTP"] : null,
                                    Authenticated = !Convert.IsDBNull(reader["Authenticated"]) ? (bool?)reader["Authenticated"] : null,
                                    AuthenticationDate = !Convert.IsDBNull(reader["AuthenticationDate"]) ? (DateTime?)reader["AuthenticationDate"] : null,
                                    ModifiePar = !Convert.IsDBNull(reader["ModifiePar"]) ? (string)reader["ModifiePar"] : null,
                                    ModifieDate = !Convert.IsDBNull(reader["ModifieDate"]) ? (DateTime?)reader["ModifieDate"] : null,
                                    RoleName = (string)reader["RoleName"],

                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }


        public List<User> GetUserSession(string sessionId)
        {


            var data = new List<User>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();

                    String sql = @"SELECT SessionId FROM [dbo].[User] where LOWER(SessionId)=LOWER(@SessionId)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@SessionId", sessionId);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data.Add(new User
                                {
                                    SessionId = !Convert.IsDBNull(reader["SessionId"]) ? (string)reader["SessionId"] : null,

                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }


        public List<MobileUser> GetMobileUser(string email)
        {

            var data = new List<MobileUser>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();

                    String sql = @"SELECT Email FROM [dbo].[MobileUser] where LOWER(Email)=LOWER(@Email)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data.Add(new MobileUser
                                {
                                    Email = !Convert.IsDBNull(reader["Email"]) ? (string)reader["Email"] : null,

                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }


        public Proprietaire GetProprietaireByEmail(string email)
        {

            Proprietaire data = null;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();

                    String sql = @"SELECT * FROM [dbo].[Proprietaire] where (LOWER(Email)=LOWER(@Email) OR (Telephone =@Email))";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.Read())
                            {
                                data = new Proprietaire
                                {
                                    ProprietaireId = (int)reader["ProprietaireId"],
                                    Email = !Convert.IsDBNull(reader["Email"]) ? (string)reader["Email"] : null,
                                    Telephone = !Convert.IsDBNull(reader["Telephone"]) ? (string)reader["Telephone"] : null,
                                    Password = !Convert.IsDBNull(reader["Password"]) ? (string)reader["Password"] : null,
                                    Nom = !Convert.IsDBNull(reader["Nom"]) ? (string)reader["Nom"] : null,
                                    Prenom = !Convert.IsDBNull(reader["Prenom"]) ? (string)reader["Prenom"] : null,
                                    AccessKey = !Convert.IsDBNull(reader["AccessKey"]) ? (string)reader["AccessKey"] : null,

                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }




        public List<User> GetUser(int userId)
        {


            var data = new List<User>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();

                    String sql = @"SELECT UserId,
RoleId,
FirstName,
LastName,
Password,
Email	,
LoweredEmail,
IsLockedOut,
CreateDate,
LastLoginDate,
FailedPasswordAttemptCount,
SuperUser,
SessionId,
OTP	,
Authenticated,
AuthenticationDate,
ModifiePar,
ModifieDate FROM [dbo].[User] Where UserId =@UserId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data.Add(new User
                                {
                                    UserId = (int)reader["UserId"],
                                    RoleId = (int)reader["RoleId"],
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"],
                                    Password = (string)reader["Password"],
                                    Email = (string)reader["Email"],
                                    LoweredEmail = (string)reader["LoweredEmail"],
                                    IsLockedOut = (bool)reader["IsLockedOut"],
                                    CreateDate = (DateTime)reader["CreateDate"],
                                    LastLoginDate = !Convert.IsDBNull(reader["LastLoginDate"]) ? (DateTime?)reader["LastLoginDate"] : null,
                                    FailedPasswordAttemptCount = (int)reader["FailedPasswordAttemptCount"],
                                    SuperUser = (bool)reader["SuperUser"],
                                    SessionId = !Convert.IsDBNull(reader["SessionId"]) ? (string)reader["SessionId"] : null,
                                    OTP = !Convert.IsDBNull(reader["OTP"]) ? (string)reader["OTP"] : null,
                                    Authenticated = !Convert.IsDBNull(reader["Authenticated"]) ? (bool?)reader["Authenticated"] : null,
                                    AuthenticationDate = !Convert.IsDBNull(reader["AuthenticationDate"]) ? (DateTime?)reader["AuthenticationDate"] : null,
                                    ModifiePar = !Convert.IsDBNull(reader["ModifiePar"]) ? (string)reader["ModifiePar"] : null,
                                    ModifieDate = !Convert.IsDBNull(reader["ModifieDate"]) ? (DateTime?)reader["ModifieDate"] : null,


                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }


        public List<User> GetUser(string email)
        {

            var data = new List<User>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();

                    String sql = @"SELECT
UserId,
RoleId,
FirstName,
LastName,
Password,
Email	,
LoweredEmail,
IsLockedOut,
CreateDate,
LastLoginDate,
FailedPasswordAttemptCount,
SuperUser,
SessionId,
OTP	,
Authenticated,
AuthenticationDate,
ModifiePar,
ModifieDate FROM [dbo].[User] AS us WHERE  LOWER(us.Email) =LOWER(@Email)
";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data.Add(new User
                                {
                                    UserId = (int)reader["UserId"],
                                    RoleId = (int)reader["RoleId"],
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"],
                                    Password = (string)reader["Password"],
                                    Email = (string)reader["Email"],
                                    LoweredEmail = (string)reader["LoweredEmail"],
                                    IsLockedOut = (bool)reader["IsLockedOut"],
                                    CreateDate = (DateTime)reader["CreateDate"],
                                    LastLoginDate = !Convert.IsDBNull(reader["LastLoginDate"]) ? (DateTime?)reader["LastLoginDate"] : null,
                                    FailedPasswordAttemptCount = (int)reader["FailedPasswordAttemptCount"],
                                    SuperUser = (bool)reader["SuperUser"],
                                    SessionId = !Convert.IsDBNull(reader["SessionId"]) ? (string)reader["SessionId"] : null,
                                    OTP = !Convert.IsDBNull(reader["OTP"]) ? (string)reader["OTP"] : null,
                                    Authenticated = !Convert.IsDBNull(reader["Authenticated"]) ? (bool?)reader["Authenticated"] : null,
                                    AuthenticationDate = !Convert.IsDBNull(reader["AuthenticationDate"]) ? (DateTime?)reader["AuthenticationDate"] : null,
                                    ModifiePar = !Convert.IsDBNull(reader["ModifiePar"]) ? (string)reader["ModifiePar"] : null,
                                    ModifieDate = !Convert.IsDBNull(reader["ModifieDate"]) ? (DateTime?)reader["ModifieDate"] : null,


                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }
            return data;

        }



        public List<User> GetCompagnieUsers(int compagnieId)
        {


            var data = new List<User>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();

                    String sql = @"

SELECT distinct u.UserId,
u.FirstName,
u.LastName,
u.Password,
u.Email	,
u.LoweredEmail,
u.IsLockedOut,
u.CreateDate,
u.LastLoginDate,
u.FailedPasswordAttemptCount,
u.SuperUser,
u.SessionId,
u.OTP	,
u.Authenticated,
u.AuthenticationDate,
u.ModifiePar,
u.ModifieDate FROM [dbo].[User] u INNER JOIN  UserApplication ua on ua.UserId=u.UserId INNER JOIN Application a on a.ApplicationId=ua.ApplicationId
and a.CompagnieId=@CompagnieId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CompagnieId", compagnieId);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data.Add(new User
                                {
                                    UserId = (int)reader["UserId"],
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"],
                                    Password = (string)reader["Password"],
                                    Email = (string)reader["Email"],
                                    LoweredEmail = (string)reader["LoweredEmail"],
                                    IsLockedOut = (bool)reader["IsLockedOut"],
                                    CreateDate = (DateTime)reader["CreateDate"],
                                    LastLoginDate = !Convert.IsDBNull(reader["LastLoginDate"]) ? (DateTime?)reader["LastLoginDate"] : null,
                                    FailedPasswordAttemptCount = (int)reader["FailedPasswordAttemptCount"],
                                    SuperUser = (bool)reader["SuperUser"],
                                    SessionId = !Convert.IsDBNull(reader["SessionId"]) ? (string)reader["SessionId"] : null,
                                    OTP = !Convert.IsDBNull(reader["OTP"]) ? (string)reader["OTP"] : null,
                                    Authenticated = !Convert.IsDBNull(reader["Authenticated"]) ? (bool?)reader["Authenticated"] : null,
                                    AuthenticationDate = !Convert.IsDBNull(reader["AuthenticationDate"]) ? (DateTime?)reader["AuthenticationDate"] : null,
                                    ModifiePar = !Convert.IsDBNull(reader["ModifiePar"]) ? (string)reader["ModifiePar"] : null,
                                    ModifieDate = !Convert.IsDBNull(reader["ModifieDate"]) ? (DateTime?)reader["ModifieDate"] : null,
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }



        public UserProfileVM GetPhotoProfile(int mobileUserId)
        {
            UserProfileVM data = null;
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"SELECT MobileUserId,UserProfileId, [Image]
                             FROM [dbo].[vwUserProfile] WhERE MobileUserId =@MobileUserId AND Statut =1";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@MobileUserId", mobileUserId);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data = new UserProfileVM
                                {
                                    MobileUserId = (int)reader["MobileUserId"],
                                    UserProfileId = (int)reader["UserProfileId"],
                                    Image = (string)reader["Image"]
                                };

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }



        public int GetUserAccess(string accessCode)
        {

            int data = -1;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"SELECT COUNT(*) UserExist
                             FROM [dbo].[MobileUser] WhERE LOWER(AccessCode) =LOWER(@AccessCode) AND Statut =1";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@AccessCode", accessCode);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data = (int)reader["UserExist"];

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }




        public List<PublicationVM> GetPublicationVM(int? paysId, int? typeEntrepriseId, string adresse)
        {

            var data = new List<PublicationVM>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    String sql = @"SELECT PublicationId
      ,DatePoste
      ,EntrepriseId
      ,ProprietaireId
      ,TypeEntrepriseId
      ,CommuneId
      ,PaysId
      ,Telephone
      ,CourrierElectronique
      ,Longitude
      ,Latitude
      ,Statut
      ,Adresse
      ,TypeEntrepriseDescription
      ,Description
      ,Proprietaire
  FROM dbo.vwPublication ";
                    if (paysId.HasValue && typeEntrepriseId.HasValue && !string.IsNullOrWhiteSpace(adresse))
                    {
                        sql += @" WHERE Statut IS NULL  AND PublicationId IS NOT NULL AND TypeEntrepriseId =@TypeEntrepriseId  ORDER BY DatePoste, 
  CASE
   WHEN PaysId IS NOT NULL AND PaysId = @PaysId THEN 1
   WHEN Adresse IS NOT NULL AND Adresse = @Adresse THEN 2
    ELSE 4
    END ";
                    }
                    else if (!typeEntrepriseId.HasValue)
                    {
                        sql += @" WHERE Statut IS NULL AND PublicationId IS NOT NULL  ORDER BY DatePoste, 
  CASE
   WHEN PaysId IS NOT NULL AND PaysId = @PaysId THEN 1
   WHEN Adresse IS NOT NULL AND Adresse = @Adresse THEN 2
    ELSE 4
    END ";
                    }


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PaysId", paysId.HasValue ? (object)paysId : DBNull.Value);
                        command.Parameters.AddWithValue("@Adresse", !string.IsNullOrWhiteSpace(adresse) ? (object)adresse : DBNull.Value);
                        command.Parameters.AddWithValue("@TypeEntrepriseId", typeEntrepriseId.HasValue ? (object)typeEntrepriseId : DBNull.Value);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data.Add(new PublicationVM
                                {
                                    PublicationId = (int)reader["PublicationId"],
                                    DatePoste = (DateTime)reader["DatePoste"],
                                    EntrepriseId = !Convert.IsDBNull(reader["EntrepriseId"]) ? (int?)reader["EntrepriseId"] : null,
                                    ProprietaireId = !Convert.IsDBNull(reader["ProprietaireId"]) ? (int?)reader["ProprietaireId"] : null,
                                    TypeEntrepriseId = !Convert.IsDBNull(reader["TypeEntrepriseId"]) ? (int?)reader["TypeEntrepriseId"] : null,
                                    CommuneId = !Convert.IsDBNull(reader["CommuneId"]) ? (int?)reader["CommuneId"] : null,
                                    PaysId = !Convert.IsDBNull(reader["PaysId"]) ? (int?)reader["PaysId"] : null,
                                    Telephone = !Convert.IsDBNull(reader["Telephone"]) ? (string)reader["Telephone"] : null,
                                    CourrierElectronique = !Convert.IsDBNull(reader["CourrierElectronique"]) ? (string)reader["CourrierElectronique"] : null,
                                    Longitude = !Convert.IsDBNull(reader["Longitude"]) ? (string)reader["Longitude"] : null,
                                    Latitude = !Convert.IsDBNull(reader["Latitude"]) ? (string)reader["Latitude"] : null,
                                    Statut = !Convert.IsDBNull(reader["Statut"]) ? (bool?)reader["Statut"] : null,
                                    Adresse = !Convert.IsDBNull(reader["Adresse"]) ? (string)reader["Adresse"] : null,
                                    TypeEntrepriseDescription = !Convert.IsDBNull(reader["TypeEntrepriseDescription"]) ? (string)reader["TypeEntrepriseDescription"] : null,
                                    Description = !Convert.IsDBNull(reader["Description"]) ? (string)reader["Description"] : null,
                                    Proprietaire = !Convert.IsDBNull(reader["Proprietaire"]) ? (string)reader["Proprietaire"] : null,
                                }); ;

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }
            return data;
        }




        public List<PublicationVM> GetPublicationVM(int? typeEntrepriseId)
        {

            var data = new List<PublicationVM>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"SELECT PublicationId
      ,DatePoste
      ,EntrepriseId
      ,ProprietaireId
      ,TypeEntrepriseId
      ,CommuneId
      ,PaysId
      ,Telephone
      ,CourrierElectronique
      ,Longitude
      ,Latitude
      ,Statut
      ,Adresse
      ,TypeEntrepriseDescription
      ,Description
      ,Proprietaire
      ,Photo
  FROM dbo.vwPublication";

                    if (typeEntrepriseId.HasValue && typeEntrepriseId.Value > 0)
                    {
                        sql += " WHERE TypeEntrepriseId =@TypeEntrepriseId";
                    }

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TypeEntrepriseId", typeEntrepriseId.HasValue ? (object)typeEntrepriseId : DBNull.Value);
                        var tempItem = new List<PublicationVM>();
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                tempItem.Add(new PublicationVM
                                {
                                    PublicationId = (int)reader["PublicationId"],
                                    DatePoste = (DateTime)reader["DatePoste"],
                                    EntrepriseId = !Convert.IsDBNull(reader["EntrepriseId"]) ? (int?)reader["EntrepriseId"] : null,
                                    ProprietaireId = !Convert.IsDBNull(reader["ProprietaireId"]) ? (int?)reader["ProprietaireId"] : null,
                                    TypeEntrepriseId = !Convert.IsDBNull(reader["TypeEntrepriseId"]) ? (int?)reader["TypeEntrepriseId"] : null,
                                    CommuneId = !Convert.IsDBNull(reader["CommuneId"]) ? (int?)reader["CommuneId"] : null,
                                    PaysId = !Convert.IsDBNull(reader["PaysId"]) ? (int?)reader["PaysId"] : null,
                                    Telephone = !Convert.IsDBNull(reader["Telephone"]) ? (string)reader["Telephone"] : null,
                                    CourrierElectronique = !Convert.IsDBNull(reader["CourrierElectronique"]) ? (string)reader["CourrierElectronique"] : null,
                                    Longitude = !Convert.IsDBNull(reader["Longitude"]) ? (string)reader["Longitude"] : null,
                                    Latitude = !Convert.IsDBNull(reader["Latitude"]) ? (string)reader["Latitude"] : null,
                                    Statut = !Convert.IsDBNull(reader["Statut"]) ? (bool?)reader["Statut"] : null,
                                    Adresse = !Convert.IsDBNull(reader["Adresse"]) ? (string)reader["Adresse"] : null,
                                    TypeEntrepriseDescription = !Convert.IsDBNull(reader["TypeEntrepriseDescription"]) ? (string)reader["TypeEntrepriseDescription"] : null,
                                    Description = !Convert.IsDBNull(reader["Description"]) ? (string)reader["Description"] : null,
                                    Proprietaire = !Convert.IsDBNull(reader["Proprietaire"]) ? (string)reader["Proprietaire"] : null,
                                    Photo = !Convert.IsDBNull(reader["Photo"]) ? (string)reader["Photo"] : null,
                                    //PublicationFichier = GetPublicationFichierVM((int)reader["PublicationId"]),
                                    // Commentaire = GetCommentaires((int)reader["PublicationId"]),
                                    // QuantiteShared = GetQuantitePartage((int)reader["PublicationId"]),
                                    // QuantiteCommentaire = GetCommentaires((int)reader["PublicationId"]).Count(),
                                    //QuantiteLiked = GetQuantiteLike((int)reader["PublicationId"]),
                                    //QuantiteDisLiked = GetQuantiteDisLike((int)reader["PublicationId"]),
                                });

                            }

                            if (tempItem != null && tempItem.Count() > 0)
                            {
                                foreach (var item in tempItem)
                                {
                                    item.PublicationFichier = GetPublicationFichierVM(item.PublicationId);
                                    item.Commentaire = GetCommentaires(item.PublicationId);
                                    item.QuantiteShared = GetQuantitePartage(item.PublicationId);
                                    item.QuantiteCommentaire = GetCommentaires(item.PublicationId).Count();
                                    item.QuantiteLiked = GetQuantiteLike(item.PublicationId);
                                    item.QuantiteDisLiked = GetQuantiteLike(item.PublicationId);
                                    data.Add(item);


                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }
            return data;
        }



        public PublicationVM GetPublicationVMByPublicationId(int? publicationId)
        {

            PublicationVM data = null;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"SELECT PublicationId
      ,DatePoste
      ,EntrepriseId
      ,ProprietaireId
      ,TypeEntrepriseId
      ,CommuneId
      ,PaysId
      ,Telephone
      ,CourrierElectronique
      ,Longitude
      ,Latitude
      ,Statut
      ,Adresse
      ,TypeEntrepriseDescription
      ,Description
      ,Proprietaire
      ,Photo
  FROM dbo.vwPublication WHERE PublicationId =@PublicationId";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PublicationId", publicationId);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data = new PublicationVM
                                {
                                    PublicationId = (int)reader["PublicationId"],
                                    DatePoste = (DateTime)reader["DatePoste"],
                                    EntrepriseId = !Convert.IsDBNull(reader["EntrepriseId"]) ? (int?)reader["EntrepriseId"] : null,
                                    ProprietaireId = !Convert.IsDBNull(reader["ProprietaireId"]) ? (int?)reader["ProprietaireId"] : null,
                                    TypeEntrepriseId = !Convert.IsDBNull(reader["TypeEntrepriseId"]) ? (int?)reader["TypeEntrepriseId"] : null,
                                    CommuneId = !Convert.IsDBNull(reader["CommuneId"]) ? (int?)reader["CommuneId"] : null,
                                    PaysId = !Convert.IsDBNull(reader["PaysId"]) ? (int?)reader["PaysId"] : null,
                                    Telephone = !Convert.IsDBNull(reader["Telephone"]) ? (string)reader["Telephone"] : null,
                                    CourrierElectronique = !Convert.IsDBNull(reader["CourrierElectronique"]) ? (string)reader["CourrierElectronique"] : null,
                                    Longitude = !Convert.IsDBNull(reader["Longitude"]) ? (string)reader["Longitude"] : null,
                                    Latitude = !Convert.IsDBNull(reader["Latitude"]) ? (string)reader["Latitude"] : null,
                                    Statut = !Convert.IsDBNull(reader["Statut"]) ? (bool?)reader["Statut"] : null,
                                    Adresse = !Convert.IsDBNull(reader["Adresse"]) ? (string)reader["Adresse"] : null,
                                    TypeEntrepriseDescription = !Convert.IsDBNull(reader["TypeEntrepriseDescription"]) ? (string)reader["TypeEntrepriseDescription"] : null,
                                    Description = !Convert.IsDBNull(reader["Description"]) ? (string)reader["Description"] : null,
                                    Proprietaire = !Convert.IsDBNull(reader["Proprietaire"]) ? (string)reader["Proprietaire"] : null,
                                    Photo = !Convert.IsDBNull(reader["Photo"]) ? (string)reader["Photo"] : null,
                                    PublicationFichier = GetPublicationFichierVM((int)reader["PublicationId"]),
                                    Commentaire = GetCommentaires((int)reader["PublicationId"]),
                                    QuantiteShared = GetQuantitePartage((int)reader["PublicationId"]),
                                    QuantiteCommentaire = GetCommentaires((int)reader["PublicationId"]).Count(),
                                    QuantiteLiked = GetQuantiteLike((int)reader["PublicationId"]),
                                    QuantiteDisLiked = GetQuantiteDisLike((int)reader["PublicationId"]),
                                };

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }
            return data;
        }


        public List<PublicationVM> GetPublicationVM(int? proprietaireId, int? entrepriseId)
        {
            var data = new List<PublicationVM>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"SELECT PublicationId
      ,Image
      ,Video
      ,DatePoste
      ,EntrepriseId
      ,ProprietaireId
      ,TypeEntrepriseId
      ,CommuneId
      ,PaysId
      ,Telephone
      ,CourrierElectronique
      ,Longitude
      ,Latitude
      ,Statut
      ,Adresse
      ,TypeEntrepriseDescription
      ,Description
      ,Proprietaire
  FROM dbo.vwPublication";

                    if (proprietaireId.HasValue)
                    {
                        sql += " WHERE dbo.vwPublication.ProprietaireId =@ProprietaireId";
                    }
                    else if (entrepriseId.HasValue)
                    {
                        sql += " WHERE dbo.vwPublication.EntrepriseId =@EntrepriseId";

                    }
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProprietaireId", proprietaireId.HasValue ? (object)proprietaireId : DBNull.Value);
                        command.Parameters.AddWithValue("@EntrepriseId", entrepriseId.HasValue ? (object)entrepriseId : DBNull.Value);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data.Add(new PublicationVM
                                {
                                    PublicationId = (int)reader["PublicationId"],
                                    DatePoste = (DateTime)reader["DatePoste"],
                                    EntrepriseId = !Convert.IsDBNull(reader["EntrepriseId"]) ? (int?)reader["EntrepriseId"] : null,
                                    ProprietaireId = !Convert.IsDBNull(reader["ProprietaireId"]) ? (int?)reader["ProprietaireId"] : null,
                                    TypeEntrepriseId = !Convert.IsDBNull(reader["TypeEntrepriseId"]) ? (int?)reader["TypeEntrepriseId"] : null,
                                    CommuneId = !Convert.IsDBNull(reader["CommuneId"]) ? (int?)reader["CommuneId"] : null,
                                    PaysId = !Convert.IsDBNull(reader["PaysId"]) ? (int?)reader["PaysId"] : null,
                                    Telephone = !Convert.IsDBNull(reader["Telephone"]) ? (string)reader["Telephone"] : null,
                                    CourrierElectronique = !Convert.IsDBNull(reader["CourrierElectronique"]) ? (string)reader["CourrierElectronique"] : null,
                                    Longitude = !Convert.IsDBNull(reader["Longitude"]) ? (string)reader["Longitude"] : null,
                                    Latitude = !Convert.IsDBNull(reader["Latitude"]) ? (string)reader["Latitude"] : null,
                                    Statut = !Convert.IsDBNull(reader["Statut"]) ? (bool?)reader["Statut"] : null,
                                    Adresse = !Convert.IsDBNull(reader["Adresse"]) ? (string)reader["Adresse"] : null,
                                    TypeEntrepriseDescription = !Convert.IsDBNull(reader["TypeEntrepriseDescription"]) ? (string)reader["TypeEntrepriseDescription"] : null,
                                    Description = !Convert.IsDBNull(reader["Description"]) ? (string)reader["Description"] : null,
                                    Proprietaire = !Convert.IsDBNull(reader["Proprietaire"]) ? (string)reader["Proprietaire"] : null,
                                });

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }
            return data;
        }



        public List<Commentaire> GetCommentaires(int publicationId)
        {

            var data = new List<Commentaire>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"
SELECT *
  FROM dbo.Commentaire WHERE PublicationId =@PublicationId ORDER BY commentaireId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PublicationId", publicationId);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data.Add(new Commentaire
                                {
                                    PublicationId = (int)reader["PublicationId"],
                                    Description = (string)reader["Description"],
                                    CommentaireId = (int)reader["CommentaireId"],
                                    DateComment = (DateTime)reader["DateComment"],
                                });

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }



        public List<Proprietaire> GetProprietaires()
        {

            var data = new List<Proprietaire>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"SELECT *FROM dbo.Proprietaire WHERE Statut IS NULL";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data.Add(new Proprietaire
                                {
                                    ProprietaireId = (int)reader["ProprietaireId"],
                                    PaysId = !Convert.IsDBNull(reader["PaysId"]) ? (int?)reader["PaysId"] : null,
                                    Nom = (string)reader["Nom"],
                                    Prenom = (string)reader["Prenom"],
                                    NINU = !Convert.IsDBNull(reader["NINU"]) ? (string)reader["NINU"] : null,
                                    CIN = !Convert.IsDBNull(reader["NINU"]) ? (string)reader["CIN"] : null,
                                    Sexe = (string)reader["Sexe"],
                                    DateDeNaissance = (DateTime)reader["DateDeNaissance"],
                                    LieuDeNaissance = !Convert.IsDBNull(reader["NINU"]) ? (string)reader["LieuDeNaissance"] : null,
                                    CommuneId = !Convert.IsDBNull(reader["CommuneId"]) ? (int?)reader["CommuneId"] : null,
                                    AccessKey = (string)reader["AccessKey"],
                                    Password = !Convert.IsDBNull(reader["Password"]) ? (string)reader["Password"] : null,
                                    PasswordAttempt = !Convert.IsDBNull(reader["PasswordAttempt"]) ? (int?)reader["PasswordAttempt"] : null,
                                    Email = !Convert.IsDBNull(reader["Email"]) ? (string)reader["Email"] : null,
                                    Telephone = !Convert.IsDBNull(reader["Telephone"]) ? (string)reader["Telephone"] : null,
                                    Statut = !Convert.IsDBNull(reader["Statut"]) ? (bool?)reader["Statut"] : null,

                                });

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }



        public List<ProprietaireVM> GetProprietaireVM()
        {

            var data = new List<ProprietaireVM>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"
SELECT
    [ProprietaireId],
    [Nom],
    [Prenom],
    [Sexe],
    [PaysId],
    [LieuDeNaissance],
    [Telephone],
    [PaysDescription],
    [Code],
    [DateDeNaissance],
    [Email],
    [CommuneId],
    [CommuneDescription],
    [CodeCommune],
    [ArrondissementId],
    [ArrondissementDescription],
    [DepartementDescription],
    [Statut]
    [Photo]
FROM
    [dbo].[vwProprietaire]
ORDER BY 
    CASE
        WHEN [LieuDeNaissance] IS NOT NULL AND [LieuDeNaissance] = @LieuDeNaissance THEN 1
        WHEN [PaysId] IS NOT NULL AND [PaysId] = @PaysId THEN 2
        WHEN [CommuneId] IS NOT NULL THEN 3
        ELSE 4
    END;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data.Add(new ProprietaireVM
                                {
                                    ProprietaireId = (int)reader["ProprietaireId"],
                                    PaysId = !Convert.IsDBNull(reader["PaysId"]) ? (int?)reader["PaysId"] : null,
                                    Nom = (string)reader["Nom"],
                                    Prenom = (string)reader["Prenom"],
                                    Sexe = (string)reader["Sexe"],
                                    LieuDeNaissance = !Convert.IsDBNull(reader["NINU"]) ? (string)reader["LieuDeNaissance"] : null,
                                    CommuneId = !Convert.IsDBNull(reader["CommuneId"]) ? (int?)reader["CommuneId"] : null,
                                    Email = !Convert.IsDBNull(reader["Email"]) ? (string)reader["Email"] : null,
                                    Telephone = !Convert.IsDBNull(reader["Telephone"]) ? (string)reader["Telephone"] : null,
                                    PaysDescription = !Convert.IsDBNull(reader["PaysDescription"]) ? (string)reader["PaysDescription"] : null,
                                    Code = !Convert.IsDBNull(reader["Code"]) ? (string)reader["Code"] : null,
                                    CommuneDescription = !Convert.IsDBNull(reader["CommuneDescription"]) ? (string)reader["CommuneDescription"] : null,
                                    CodeCommune = !Convert.IsDBNull(reader["CodeCommune"]) ? (string)reader["CodeCommune"] : null,
                                    ArrondissementId = !Convert.IsDBNull(reader["ArrondissementId"]) ? (int?)reader["ArrondissementId"] : null,
                                    ArrondissementDescription = !Convert.IsDBNull(reader["ArrondissementDescription"]) ? (string)reader["ArrondissementDescription"] : null,
                                    DepartementDescription = !Convert.IsDBNull(reader["DepartementDescription"]) ? (string)reader["DepartementDescription"] : null,
                                    DateDeNaissance = (DateTime)reader["DateDeNaissance"],
                                    Statut = !Convert.IsDBNull(reader["Statut"]) ? (bool?)reader["Statut"] : null,
                                    ModifierDate = !Convert.IsDBNull(reader["ModifierDate"]) ? (DateTime?)reader["ModifierDate"] : null,
                                    Photo = !Convert.IsDBNull(reader["Photo"]) ? (string)reader["Photo"] : null,

                                });

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }



        public ProprietaireVM GetProprietaireVMByProprietaireId(int proprietaireId)
        {

            ProprietaireVM data = new ProprietaireVM();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"SELECT *FROM
    [dbo].[vwProprietaire] WHERE ProprietaireId =@ProprietaireId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProprietaireId", proprietaireId);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.Read())
                            {
                                data = new ProprietaireVM
                                {
                                    ProprietaireId = (int)reader["ProprietaireId"],
                                    PaysId = !Convert.IsDBNull(reader["PaysId"]) ? (int?)reader["PaysId"] : null,
                                    Nom = (string)reader["Nom"],
                                    Prenom = (string)reader["Prenom"],
                                    Sexe = (string)reader["Sexe"],
                                    AccessKey = (string)reader["AccessKey"],
                                    LieuDeNaissance = !Convert.IsDBNull(reader["LieuDeNaissance"]) ? (string)reader["LieuDeNaissance"] : null,
                                    CommuneId = !Convert.IsDBNull(reader["CommuneId"]) ? (int?)reader["CommuneId"] : null,
                                    Email = !Convert.IsDBNull(reader["Email"]) ? (string)reader["Email"] : null,
                                    Telephone = !Convert.IsDBNull(reader["Telephone"]) ? (string)reader["Telephone"] : null,
                                    PaysDescription = !Convert.IsDBNull(reader["PaysDescription"]) ? (string)reader["PaysDescription"] : null,
                                    Code = !Convert.IsDBNull(reader["Code"]) ? (string)reader["Code"] : null,
                                    CommuneDescription = !Convert.IsDBNull(reader["CommuneDescription"]) ? (string)reader["CommuneDescription"] : null,
                                    CodeCommune = !Convert.IsDBNull(reader["CodeCommune"]) ? (string)reader["CodeCommune"] : null,
                                    ArrondissementId = !Convert.IsDBNull(reader["ArrondissementId"]) ? (int?)reader["ArrondissementId"] : null,
                                    ArrondissementDescription = !Convert.IsDBNull(reader["ArrondissementDescription"]) ? (string)reader["ArrondissementDescription"] : null,
                                    DepartementDescription = !Convert.IsDBNull(reader["DepartementDescription"]) ? (string)reader["DepartementDescription"] : null,
                                    DateDeNaissance = (DateTime)reader["DateDeNaissance"],
                                    Statut = !Convert.IsDBNull(reader["Statut"]) ? (bool?)reader["Statut"] : null,
                                    Photo = !Convert.IsDBNull(reader["Photo"]) ? (string)reader["Photo"] : null,

                                };

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }


        public List<Proprietaire> GetAccesskeys()
        {

            var data = new List<Proprietaire>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"SELECT AccessKey FROM dbo.Proprietaire";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data.Add(new Proprietaire
                                {

                                    AccessKey = (string)reader["AccessKey"],

                                });

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }



        public Proprietaire GetProprietaire(int proprietaireId, string accessKey)
        {

            Proprietaire data = null;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"SELECT *FROM dbo.Proprietaire Where ProprietaireId =@ProprietaireId AND AccessKey=@AccessKey";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProprietaireId", proprietaireId);
                        command.Parameters.AddWithValue("@AccessKey", accessKey);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.Read())
                            {
                                data = (new Proprietaire
                                {
                                    ProprietaireId = (int)reader["ProprietaireId"],
                                    PaysId = !Convert.IsDBNull(reader["PaysId"]) ? (int?)reader["PaysId"] : null,
                                    Nom = (string)reader["Nom"],
                                    Prenom = (string)reader["Prenom"],
                                    NINU = !Convert.IsDBNull(reader["NINU"]) ? (string)reader["NINU"] : null,
                                    CIN = !Convert.IsDBNull(reader["CIN"]) ? (string)reader["CIN"] : null,
                                    Sexe = (string)reader["Sexe"],
                                    DateDeNaissance = (DateTime)reader["DateDeNaissance"],
                                    LieuDeNaissance = !Convert.IsDBNull(reader["NINU"]) ? (string)reader["LieuDeNaissance"] : null,
                                    CommuneId = !Convert.IsDBNull(reader["CommuneId"]) ? (int?)reader["CommuneId"] : null,
                                    AccessKey = (string)reader["AccessKey"],
                                    Password = !Convert.IsDBNull(reader["Password"]) ? (string)reader["Password"] : null,
                                    PasswordAttempt = !Convert.IsDBNull(reader["PasswordAttempt"]) ? (int?)reader["PasswordAttempt"] : null,
                                    Email = !Convert.IsDBNull(reader["Email"]) ? (string)reader["Email"] : null,
                                    Telephone = !Convert.IsDBNull(reader["Telephone"]) ? (string)reader["Telephone"] : null,

                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }


        public Proprietaire GetProprietaire(string accessKey)
        {

            Proprietaire data = null;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"SELECT *FROM dbo.Proprietaire Where AccessKey=@AccessKey";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@AccessKey", accessKey);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.Read())
                            {
                                data = (new Proprietaire
                                {
                                    ProprietaireId = (int)reader["ProprietaireId"],
                                    PaysId = !Convert.IsDBNull(reader["PaysId"]) ? (int?)reader["PaysId"] : null,
                                    Nom = (string)reader["Nom"],
                                    Prenom = (string)reader["Prenom"],
                                    NINU = !Convert.IsDBNull(reader["NINU"]) ? (string)reader["NINU"] : null,
                                    CIN = !Convert.IsDBNull(reader["CIN"]) ? (string)reader["CIN"] : null,
                                    Sexe = (string)reader["Sexe"],
                                    DateDeNaissance = (DateTime)reader["DateDeNaissance"],
                                    LieuDeNaissance = !Convert.IsDBNull(reader["NINU"]) ? (string)reader["LieuDeNaissance"] : null,
                                    CommuneId = !Convert.IsDBNull(reader["CommuneId"]) ? (int?)reader["CommuneId"] : null,
                                    AccessKey = (string)reader["AccessKey"],
                                    Password = !Convert.IsDBNull(reader["Password"]) ? (string)reader["Password"] : null,
                                    PasswordAttempt = !Convert.IsDBNull(reader["PasswordAttempt"]) ? (int?)reader["PasswordAttempt"] : null,
                                    Email = !Convert.IsDBNull(reader["Email"]) ? (string)reader["Email"] : null,
                                    Telephone = !Convert.IsDBNull(reader["Telephone"]) ? (string)reader["Telephone"] : null,
                                    Adresse = !Convert.IsDBNull(reader["Adresse"]) ? (string)reader["Adresse"] : null,
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }



        public Entreprise GetEntreprise(int entrepriseId)
        {

            Entreprise data = null;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"
SELECT EntrepriseId
      ,ProprietaireId
      ,TypeEntrepriseId
      ,CommuneId
      ,PaysId
      ,Description
      ,NoPatente
      ,CourrierElectronique
      ,Telephone
      ,Longitude
      ,Latitude
      ,Statut
      ,StatutDelete
      ,DateDelete
      ,Adresse
  FROM dbo.Entreprise WHERE EntrepriseId = @EntrepriseId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@EntrepriseId", entrepriseId);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.Read())
                            {
                                data = (new Entreprise
                                {
                                    EntrepriseId = (int)reader["EntrepriseId"],
                                    ProprietaireId = (int)reader["ProprietaireId"],
                                    TypeEntrepriseId = (int)reader["TypeEntrepriseId"],
                                    CommuneId = !Convert.IsDBNull(reader["CommuneId"]) ? (int?)reader["CommuneId"] : null,
                                    PaysId = (int)reader["PaysId"],
                                    Description = (string)reader["Description"],
                                    NoPatente = !Convert.IsDBNull(reader["NoPatente"]) ? (string)reader["NoPatente"] : null,
                                    CourrierElectronique = !Convert.IsDBNull(reader["CourrierElectronique"]) ? (string)reader["CourrierElectronique"] : null,
                                    Telephone = !Convert.IsDBNull(reader["Telephone"]) ? (string)reader["Telephone"] : null,
                                    Longitude = !Convert.IsDBNull(reader["Longitude"]) ? (string)reader["Longitude"] : null,
                                    Latitude = !Convert.IsDBNull(reader["Latitude"]) ? (string)reader["Latitude"] : null,
                                    Statut = !Convert.IsDBNull(reader["Statut"]) ? (bool?)reader["Statut"] : null,
                                    StatutDelete = !Convert.IsDBNull(reader["StatutDelete"]) ? (bool?)reader["StatutDelete"] : null,
                                    DateDelete = !Convert.IsDBNull(reader["DateDelete"]) ? (DateTime?)reader["DateDelete"] : null,
                                    Adresse = !Convert.IsDBNull(reader["Adresse"]) ? (string)reader["Adresse"] : null,


                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }



        public List<Pay> GetPays()
        {

            var data = new List<Pay>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"SELECT *FROM Pays WHERE (Statut <>0 OR Statut IS NULL)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data.Add(new Pay
                                {
                                    PaysId = (int)reader["PaysId"],
                                    Code = (string)reader["Code"],
                                    Description = (string)reader["Description"],
                                    PaysEtranger = !Convert.IsDBNull(reader["PaysEtranger"]) ? (bool?)reader["PaysEtranger"] : null,
                                    Statut = !Convert.IsDBNull(reader["Statut"]) ? (bool?)reader["Statut"] : null,
                                });

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }


        public List<PublicationFichierVM> GetPublicationFichierVM(int publicationId)
        {

            var data = new List<PublicationFichierVM>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"SELECT PublicationFichierId,PublicationId,Image, Video FROM vwPublicationFile WHERE PublicationId =@PublicationId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PublicationId", publicationId);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data.Add(new PublicationFichierVM
                                {
                                    PublicationFichierId = (int)reader["PublicationFichierId"],
                                    PublicationId = (int)reader["PublicationId"],
                                    Image = !Convert.IsDBNull(reader["Image"]) ? (string)reader["Image"] : null,
                                    Video = !Convert.IsDBNull(reader["Video"]) ? (string)reader["Video"] : null,
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }



        public List<TypeEntreprise> GetTypeEntreprises()
        {

            var data = new List<TypeEntreprise>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"SELECT * FROM TypeEntreprise ORDER BY Description DESC";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                data.Add(new TypeEntreprise
                                {
                                    TypeEntrepriseId = (int)reader["TypeEntrepriseId"],
                                    Description = (string)reader["Description"],
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }


        public TypeEntreprise GetTypeEntreprise(int typeEntrepriseId)
        {

            TypeEntreprise data = null;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"SELECT * FROM TypeEntreprise WHERE TypeEntrepriseId =@TypeEntrepriseId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TypeEntrepriseId", typeEntrepriseId);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.Read())
                            {
                                data = new TypeEntreprise
                                {
                                    TypeEntrepriseId = (int)reader["TypeEntrepriseId"],
                                    Description = (string)reader["Description"],
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }



        //end list
        //start function


        public int GetQuantiteLike(int publicationId)
        {

            int data = -1;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"SELECT [dbo].[QuantiteLikePublication] (@PublicationId) As Quantite";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PublicationId", publicationId);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.Read())
                            {
                                data = (int)reader["Quantite"];

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }



        public int GetQuantiteDisLike(int publicationId)
        {

            int data = -1;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"SELECT [dbo].[QuantiteDisLikePublication] (@PublicationId) As Quantite";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PublicationId", publicationId);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.Read())
                            {
                                data = (int)reader["Quantite"];

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }



        public int GetQuantitePartage(int publicationId)
        {

            int data = -1;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    String sql = @"SELECT [dbo].[QuantitePartage] (@PublicationId) As Quantite";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PublicationId", publicationId);
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.Read())
                            {
                                data = (int)reader["Quantite"];

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());

            }

            return data;

        }
        //end list

        //end fonction

        //start procedure



        public int insertPhotoProfile(int mobileUserId, string image, bool statut, string lieuPublication, string latitude, string longitude, DateTime datePublication, DateTime modifierDate, string modifierPar)
        {
            int id = -1;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();
                    string sql = "insertPhotoProfile";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MobileUserId", mobileUserId);
                        command.Parameters.AddWithValue("@Image", image + "");
                        command.Parameters.AddWithValue("@Statut", statut);
                        command.Parameters.AddWithValue("@LieuPublication", lieuPublication);
                        command.Parameters.AddWithValue("@Latitude", latitude);
                        command.Parameters.AddWithValue("@Longitude", longitude);
                        command.Parameters.AddWithValue("@DatePublication", datePublication);
                        command.Parameters.AddWithValue("@ModifierDate", modifierDate);
                        command.Parameters.AddWithValue("@ModifierPar", modifierPar);
                        command.ExecuteNonQuery();
                        id = 1;

                    }



                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }


            return id;

        }



        public int InsertinsertPublicationFichier(int publicationId, string image, string video)
        {
            int id = -1;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();
                    string sql = "insertPublicationFichier";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PublicationId", publicationId);
                        command.Parameters.AddWithValue("@Image", image + "");
                        command.Parameters.AddWithValue("@Video", video);
                        command.ExecuteNonQuery();
                        id = 1;

                    }



                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }


            return id;

        }



        //end procedure

        //Beginning api Insert



        public int InsertProprietaire(Proprietaire proprietaire)
        {
            int id = -1;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();

                    string sql = @"INSERT INTO dbo.Proprietaire
           (Nom
           ,Prenom
           ,Sexe
           ,DateDeNaissance
           ,PaysId
           ,LieuDeNaissance
           ,CommuneId
           ,AccessKey
		   ,Password
		   ,PasswordAttempt
		   ,Email
		   ,Telephone)
     VALUES
           (@Nom
           ,@Prenom
           ,@Sexe
           ,@DateDeNaissance
           ,@PaysId
           ,@LieuDeNaissance
           ,@CommuneId
           ,@AccessKey
		   ,@Password
		   ,@PasswordAttempt
		   ,@Email
		   ,@Telephone);SELECT SCOPE_IDENTITY()";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nom", proprietaire.Nom);
                        command.Parameters.AddWithValue("@Prenom", proprietaire.Prenom);
                        command.Parameters.AddWithValue("@Sexe", proprietaire.Sexe);
                        command.Parameters.AddWithValue("@DateDeNaissance", proprietaire.DateDeNaissance.Year > 1900 ? (object)proprietaire.DateDeNaissance : DBNull.Value);
                        command.Parameters.AddWithValue("@PaysId", proprietaire.PaysId.HasValue ? (object)proprietaire.PaysId : DBNull.Value);
                        command.Parameters.AddWithValue("@AccessKey", proprietaire.AccessKey);
                        command.Parameters.AddWithValue("@LieuDeNaissance", !string.IsNullOrWhiteSpace(proprietaire.LieuDeNaissance) ? (object)proprietaire.LieuDeNaissance : DBNull.Value);
                        command.Parameters.AddWithValue("@CommuneId", proprietaire.CommuneId.HasValue ? (object)proprietaire.CommuneId : DBNull.Value);
                        command.Parameters.AddWithValue("@Password", !string.IsNullOrWhiteSpace(proprietaire.Password) ? (object)proprietaire.Password : DBNull.Value);
                        command.Parameters.AddWithValue("@PasswordAttempt", proprietaire.PasswordAttempt.HasValue ? (object)proprietaire.PasswordAttempt : DBNull.Value);
                        command.Parameters.AddWithValue("@Email", !string.IsNullOrWhiteSpace(proprietaire.Email) ? (object)proprietaire.Email : DBNull.Value);
                        command.Parameters.AddWithValue("@Telephone", !string.IsNullOrWhiteSpace(proprietaire.Telephone) ? (object)proprietaire.Telephone : DBNull.Value);

                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            id = Convert.ToInt32(result);
                        }

                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return id;

        }




        public int InsertPublication(Publication publication)
        {
            int id = -1;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();

                    string sql = @"INSERT INTO dbo.Publication
           (Description
           ,DatePoste
          )
     VALUES
           (@Description
           ,@DatePoste);SELECT SCOPE_IDENTITY()";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Description", !string.IsNullOrWhiteSpace(publication.Description) ? (object)publication.Description : DBNull.Value);
                        command.Parameters.AddWithValue("@DatePoste", publication.DatePoste.HasValue ? (object)publication.DatePoste : DBNull.Value);

                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            id = Convert.ToInt32(result);
                        }

                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return id;

        }



        public int InsertPublicationProprietaire(PublicationProprietaire publicationProprietaire)
        {
            int id = -1;
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();

                    string sql = @"INSERT INTO dbo.PublicationProprietaire
           (PublicationId
           ,EntrepriseId
           ,ProprietaireId
          )
     VALUES
           (@PublicationId
           ,@EntrepriseId,
           ,@ProprietaireId
);SELECT SCOPE_IDENTITY()";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PublicationId", publicationProprietaire.PublicationId);
                        command.Parameters.AddWithValue("@ProprietaireId", publicationProprietaire.ProprietaireId.HasValue ? (object)publicationProprietaire.ProprietaireId : DBNull.Value);
                        command.Parameters.AddWithValue("@EntrepriseId", publicationProprietaire.EntrepriseId.HasValue ? (object)publicationProprietaire.EntrepriseId : DBNull.Value);
                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            id = Convert.ToInt32(result);
                        }

                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return id;

        }


        public int InsertLikeOrDislike(LikeOrDislike likeOrDislike)
        {
            int id = -1;
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();

                    string sql = @"INSERT INTO dbo.LikeOrDislike
           (Statut
           ,DateLike
          )
     VALUES
           (@Statut
           ,@DateLike
);SELECT SCOPE_IDENTITY()";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Statut", likeOrDislike.Statut);
                        command.Parameters.AddWithValue("@DateLike", likeOrDislike.DateLike);
                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            id = Convert.ToInt32(result);
                        }

                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return id;

        }



        public int InsertInternautLikeDislike(InternautLikeDislike internautLikeDislike)
        {
            int id = -1;
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();

                    string sql = @"INSERT INTO dbo.InternautLikeDislike
           (InternautId
           ,LikeOrDislikeId
           ,EntrepriseId
           ,ProprietaireId
           ,PublicationId
          )
     VALUES
           (@InternautId
           ,@LikeOrDislikeId,
           ,@EntrepriseId
           ,@ProprietaireId
           ,@PublicationId
);SELECT SCOPE_IDENTITY()";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@InternautId", internautLikeDislike.InternautId.HasValue ? (object)internautLikeDislike.InternautId : DBNull.Value);
                        command.Parameters.AddWithValue("@LikeOrDislikeId", internautLikeDislike.LikeOrDislikeId);
                        command.Parameters.AddWithValue("@EntrepriseId", internautLikeDislike.EntrepriseId.HasValue ? (object)internautLikeDislike.EntrepriseId : DBNull.Value);
                        command.Parameters.AddWithValue("@ProprietaireId", internautLikeDislike.ProprietaireId.HasValue ? (object)internautLikeDislike.ProprietaireId : DBNull.Value);
                        command.Parameters.AddWithValue("@PublicationId", internautLikeDislike.PublicationId.HasValue ? (object)internautLikeDislike.PublicationId : DBNull.Value);
                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            id = Convert.ToInt32(result);
                        }

                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return id;

        }



        //end insert
        //start update api
        public int UpdateProprietaire(Proprietaire proprietaire)
        {
            int id = -1;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();

                    string sql = @"UPDATE dbo.Proprietaire
   SET Nom = @Nom
      ,Prenom = @Prenom
      ,NINU = @NINU
      ,CIN = @CIN
      ,Sexe = @Sexe
      ,DateDeNaissance = @DateDeNaissance
      ,PaysDeNaissanceId = @PaysDeNaissanceId
      ,LieuDeNaissance = @LieuDeNaissance
      ,CommuneId = @CommuneId
      ,AccessKey = @AccessKey
      ,Password = @Password
      ,PasswordAttempt = @PasswordAttempt
      ,Email = @Email
      ,Telephone = @Telephone
 WHERE ProprietaireId =@ProprietaireId";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProprietaireId", proprietaire.ProprietaireId);
                        command.Parameters.AddWithValue("@Nom", proprietaire.Nom);
                        command.Parameters.AddWithValue("@Prenom", proprietaire.Prenom);
                        command.Parameters.AddWithValue("@Sexe", proprietaire.Sexe);
                        command.Parameters.AddWithValue("@DateDeNaissance", proprietaire.DateDeNaissance);
                        command.Parameters.AddWithValue("@PaysDeNaissanceId", proprietaire.PaysId.HasValue ? (object)proprietaire.PaysId : DBNull.Value);
                        command.Parameters.AddWithValue("@AccessKey", proprietaire.AccessKey);
                        command.Parameters.AddWithValue("@LieuDeNaissance", !string.IsNullOrWhiteSpace(proprietaire.LieuDeNaissance) ? (object)proprietaire.LieuDeNaissance : DBNull.Value);
                        command.Parameters.AddWithValue("@CommuneId", proprietaire.CommuneId.HasValue ? (object)proprietaire.CommuneId : DBNull.Value);
                        command.Parameters.AddWithValue("@Password", !string.IsNullOrWhiteSpace(proprietaire.Password) ? (object)proprietaire.Password : DBNull.Value);
                        command.Parameters.AddWithValue("@PasswordAttempt", proprietaire.PasswordAttempt.HasValue ? (object)proprietaire.PasswordAttempt : DBNull.Value);
                        command.Parameters.AddWithValue("@Email", !string.IsNullOrWhiteSpace(proprietaire.Email) ? (object)proprietaire.Email : DBNull.Value);
                        command.Parameters.AddWithValue("@Telephone", !string.IsNullOrWhiteSpace(proprietaire.Telephone) ? (object)proprietaire.Telephone : DBNull.Value);

                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            id = Convert.ToInt32(result);
                        }

                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return id;

        }


        public int UpdateDeletePublication(PublicationVM publication)
        {
            int id = -1;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = _dataSource;
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "CBPresence";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();

                    string sql = @"UPDATE dbo.Publication
   SET StatutDelete = @StatutDelete
      ,DateDelete = @DateDelete
 WHERE PublicationId =@PublicationId";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PublicationId", publication.PublicationId);
                        command.Parameters.AddWithValue("@StatutDelete", publication.StatutDelete.HasValue ? (object)publication.StatutDelete : null);
                        command.Parameters.AddWithValue("@DateDelete", publication.DateDelete.HasValue ? (object)publication.DateDelete : null);

                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            id = Convert.ToInt32(result);
                        }

                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return id;

        }

    }


    //end update api
}

