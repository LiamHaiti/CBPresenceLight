using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Security.Principal;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Cors;
using CBPresenceLight.Models;

namespace CBPresenceLight.Controllers
{
    [EnableCors("AllowAll")]
    [Route("apiv.01/[controller]/[action]")]
    [ApiController]
    public class ProprietairesController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly LinkGenerator _linkGenerator;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProprietairesController(IConfiguration config, LinkGenerator linkGenerator, IWebHostEnvironment hostingEnvironment)
        {
            _config = config;
            _linkGenerator = linkGenerator;
            _hostingEnvironment = hostingEnvironment;
        }


        //[Route("Login")]
        [HttpPost]
        public ActionResult<object> Login([FromBody] Login login)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var response = new HttpResponseMessage();
            if (login == null || string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                return NotFound(new { message = "Courriel ou Mot de passe incorrect" });
            }

            var db = new DataAccessController(_hostingEnvironment, _config);
            Proprietaire user = db.GetProprietaireByEmail(login.Email);
            if (user == null)
            {
                return NotFound(new { message = "Courriel ou Mot de passe incorrect"});
            }

            if (user.Password != new Functions(_config).Encrypt(login.Password))
            {
                return NotFound(new { message = "Courriel ou Mot de passe incorrect" });
            }

            ProprietaireVM proprietaire = db.GetProprietaireVMByProprietaireId(user.ProprietaireId);

            var userObj = new { Nom = proprietaire.Nom + " " + proprietaire.Prenom, Email = proprietaire.Email, AccessKey = new Functions(_config).Decrypt(""+ proprietaire.AccessKey), PhotoProfil = "data:image/jpeg;base64," + proprietaire.Photo};
            return Ok(userObj);
        }



        public ActionResult<object> CreateAccount([FromBody] Proprietaire proprietaire)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var response = new HttpResponseMessage();
            if (!string.IsNullOrWhiteSpace("" + proprietaire.Nom) && !string.IsNullOrWhiteSpace(proprietaire.Prenom) && !string.IsNullOrWhiteSpace(proprietaire.Sexe) && proprietaire.DateDeNaissance.Year <= System.DateTime.Now.AddYears(-6).Year)
            {
                var db = new DataAccessController(_hostingEnvironment, _config);
                string accessKey = string.Empty;
                int resulRequest = -1;
                accessKey = "";

                do
                {
                    accessKey = CreateKey(16);
                }
                while (db.GetAccesskeys().Count(m => m.AccessKey.Equals(accessKey, StringComparison.OrdinalIgnoreCase)) > 0);
                proprietaire.AccessKey = new Functions(_config).Encrypt(accessKey);
                proprietaire.Password = new Functions(_config).Encrypt(proprietaire.Password);
                resulRequest = db.InsertProprietaire(proprietaire);
                if (resulRequest!=-1)
                {
                    var userObj = new { Nom = proprietaire.Nom + " " + proprietaire.Prenom, AccessKey = new Functions(_config).Decrypt(proprietaire.AccessKey) };
                    return Ok(userObj);
                }
                else
                {
                    return NotFound(new { message = "Tous les champs sont requis!" });

                }

            }
            else if (string.IsNullOrWhiteSpace(proprietaire.Nom))
            {
                return NotFound(new { message = "Veuillez saisir votre nom!" });

            }
            else if (string.IsNullOrWhiteSpace(proprietaire.Prenom))
            {
                return NotFound(new { message = "Veuillez saisir votre Prenom!" });
            }

            else if (string.IsNullOrWhiteSpace(proprietaire.Sexe))
            {
                return NotFound(new { message = "Veuillez séléctionner votre sexe!" });
            }
            else
            {
                return NotFound(new { message = "Vous devez avoir au moins 6 ans!" });
            }
        }



        [HttpPut]
        public ActionResult<object> UpdateAccount([FromBody] Proprietaire proprietaire)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var response = new HttpResponseMessage();
            if (!string.IsNullOrWhiteSpace("" + proprietaire.Nom) && !string.IsNullOrWhiteSpace(proprietaire.Prenom) && !string.IsNullOrWhiteSpace(proprietaire.Sexe) && proprietaire.DateDeNaissance.Year <= System.DateTime.Now.AddYears(-6).Year)
            {
                var db = new DataAccessController(_hostingEnvironment, _config);
                var headers = Request.Headers;
                string accessKey = string.Empty;

                if (headers != null && headers.ContainsKey("AccessKey"))
                {
                    headers.TryGetValue("AccessKey", out Microsoft.Extensions.Primitives.StringValues value);
                    accessKey = (string)value;
                }
                accessKey = new Functions(_config).Encrypt(accessKey);
                Proprietaire proprietaireEdit = db.GetProprietaire(proprietaire.ProprietaireId, accessKey);
                int resulRequest = -1;
                if (proprietaireEdit != null)
                {
                    proprietaireEdit.Nom = proprietaire.Nom;
                    proprietaireEdit.Prenom = proprietaire.Prenom;
                    proprietaireEdit.CIN = proprietaire.CIN;
                    proprietaireEdit.CommuneId = proprietaire.CommuneId;
                    proprietaireEdit.PaysId = proprietaire.PaysId;
                    proprietaireEdit.LieuDeNaissance = proprietaire.LieuDeNaissance;
                    proprietaireEdit.NINU = proprietaire.NINU;
                    resulRequest = db.UpdateProprietaire(proprietaire);
                }
                
                var userObj = new { Nom = proprietaire.Nom + " " + proprietaire.Prenom, AccessKey = new Functions(_config).Decrypt(proprietaire.AccessKey) };
                return Ok(userObj);
            }
            else if (string.IsNullOrWhiteSpace(proprietaire.Nom))
            {
                return NotFound(new { message = "Veuillez saisir votre nom!" });

            }
            else if (string.IsNullOrWhiteSpace(proprietaire.Prenom))
            {
                return NotFound(new { message = "Veuillez saisir votre Prenom!" });
            }
            else if (string.IsNullOrWhiteSpace(proprietaire.Sexe))
            {
                return NotFound(new { message = "Veuillez séléctionner votre sexe!" });
            }
            else
            {
                return NotFound(new { message = "Vous devez avoir au moins 6 ans!" });
            }
        }


        public ActionResult<object> GetProprietaires()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var response = new HttpResponseMessage();
            var db = new DataAccessController(_hostingEnvironment, _config);
            var headers = Request.Headers;
            string accessKey = string.Empty;
            var tt = "";
            if (headers != null && headers.ContainsKey("AccessKey"))
            {
                headers.TryGetValue("AccessKey", out Microsoft.Extensions.Primitives.StringValues value);
                accessKey = (string)value;
                tt = (string)value;
                
            }

            accessKey = new Functions(_config).Encrypt(accessKey);
            var proprietaire = db.GetProprietaire(accessKey);
            if (proprietaire == null)
            {
                return NotFound(new { message = tt+"<=NotFound=>"+accessKey });
            }
            var proprietaires = db.GetProprietaires().Select(
                p => new
                {
                    ProprietaireId = p.ProprietaireId,
                    PaysId = p.PaysId,
                    Nom = p.Nom,
                    Prenom = p.Prenom,
                    Sexe = p.Sexe,
                    DateDeNaissance = p.DateDeNaissance,
                    LieuDeNaissance = p.LieuDeNaissance,
                    CommuneId = p.CommuneId,
                    Email = p.Email,
                    Telephone = p.Telephone
                }).Take(100).ToList();
            return Ok(proprietaires);

        }
        

        public ActionResult<object> GetProprietairePublications()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var response = new HttpResponseMessage();
            var db = new DataAccessController(_hostingEnvironment, _config);
            var headers = Request.Headers;
            string accessKey = string.Empty;
            if (headers != null && headers.ContainsKey("AccessKey"))
            {
                headers.TryGetValue("AccessKey", out Microsoft.Extensions.Primitives.StringValues value);
                accessKey = (string)value;
            }

            accessKey = new Functions(_config).Encrypt(accessKey);
            var proprietaire = db.GetProprietaire(accessKey);
            if (proprietaire == null)
            {
                return NotFound(new { message = "NotFound" });
            }

            var proprietaires = db.GetPublicationVM(proprietaire.ProprietaireId,null).Select(
                p => new
                {
                    Publication = p
                    
                }).ToList();
            return Ok(proprietaires);

        }

        public string CreateKey(int length)
        {
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();

            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
