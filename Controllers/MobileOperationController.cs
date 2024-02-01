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
    public class MobileOperationController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly LinkGenerator _linkGenerator;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public MobileOperationController(IConfiguration config, LinkGenerator linkGenerator, IWebHostEnvironment hostingEnvironment)
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
            MobileUser user = db.GetMobileUser(login.Email).FirstOrDefault();
            if (user == null)
            {
                return NotFound(new { message = "Courriel ou Mot de passe incorrect" });
            }

            if (user.Password != new Functions(_config).Encrypt(login.Password))
            {
                return NotFound(new { message = "Courriel ou Mot de passe incorrect" });
            }

            var photo = db.GetPhotoProfile(user.MobileUserId) != null ? db.GetPhotoProfile(user.MobileUserId).Image : "";
            var userObj = new { Nom = user.FirstName + " " + user.LastName, Email = user.Email, AccessKey = new Functions(_config).Decrypt(user.AccessCode), PhotoProfil = !string.IsNullOrWhiteSpace(photo) ? "data:image/jpeg;base64," + photo : null };
            return Ok(userObj);
        }



/*


        //[Route("AddEtapeDossier")]
        [HttpPost]
        public ActionResult<object> AddEtapeDossier([FromBody] EtapeDossierList ed)
        {

            string accessKey = string.Empty;
            var db = new DataAccessController(_hostingEnvironment, _config);

            var headers = Request.Headers;

            if (headers != null && headers.ContainsKey("AccessKey"))
            {
                headers.TryGetValue("AccessKey", out Microsoft.Extensions.Primitives.StringValues value);
                accessKey = (string)value;
            }

            accessKey = new Functions(_config).Encrypt(accessKey);
            var user = db.GetUserAccess(accessKey) != -1;
            if (!user)
            {
                return NotFound(new { message = "Pas autorisation!" });
            }

            if (ed == null || ed.EtapeDossier == null)
            {
                return NotFound(new { message = "Pas d'élément à ajouter!" });
            }

            int i = 0;

            List<object> idList = new List<object>();
            int resultRequest = -1;
            List<EtapeDossierApi> nEtapes = new List<EtapeDossierApi>();

            foreach (var etapeDossier in ed.EtapeDossier.OrderBy(e => e.OrdreEtape).ThenBy(e => e.DossierId))
            {
                List<int> oldEtape = new List<int>();
                if (!idList.Contains(etapeDossier.Id))
                {
                    idList.Add(etapeDossier.Id);

                }

                EtapeDossierVM dossierEncours = db.GetEtapeDossierEncour(etapeDossier.DossierId).FirstOrDefault();
                if (dossierEncours != null && dossierEncours.DossierId == etapeDossier.DossierId)
                {
                    oldEtape.Add(dossierEncours.EtapeId);
                }

                EtapeDossierVM etapeEnCours = db.GetEtapeDossier(dossierEncours != null ? dossierEncours.EtapeDossierId : 0).FirstOrDefault();
                if (etapeEnCours != null)
                {
                    DateTime.TryParse(System.DateTime.Today.ToString(), out DateTime dateFin);
                    etapeEnCours.DateFin = dateFin;
                    etapeEnCours.ModifierPar = user.LastName + " " + user.FirstName;
                    etapeEnCours.ModifierDate = System.DateTime.Now;
                    resultRequest = db.UpdateEtapeDossier(etapeEnCours);
                    if (resultRequest != -1)
                    {
                        if (!string.IsNullOrWhiteSpace(etapeDossier.DocumentFile))
                        {
                            db.InsertFichierDocumentEtapeDossier(resultRequest, etapeDossier.DocumentFile, System.DateTime.Now, user.LastName + " " + user.FirstName);
                        }

                        i++;
                    }
                }

                var m = ed.EtapeDossier.Where(e => e.DossierId == etapeDossier.DossierId).Max(r => r.OrdreEtape);
                var maxEtape = ed.EtapeDossier.FirstOrDefault(e => e.OrdreEtape == m && e.DossierId == etapeDossier.DossierId);
               
                if (maxEtape != null && etapeDossier.OrdreEtape == maxEtape.OrdreEtape)
                {
                    EtapeDossierVM newetape = db.GetEtapeDossier(etapeDossier.DossierId, etapeDossier.EtapeId).FirstOrDefault();
                    if (newetape != null)
                    {
                        DateTime.TryParse(etapeDossier.DateDebut, out DateTime dateDebut);
                        DateTime.TryParse(etapeDossier.DateFin, out DateTime dateFin);
                        newetape.DossierId = etapeDossier.DossierId;
                        newetape.DateDebut = dateDebut;
                        if (!string.IsNullOrWhiteSpace(etapeDossier.DateFin))
                        {
                            newetape.DateFin = dateFin;
                        }
                        newetape.Montant = etapeDossier.Montant;
                        newetape.Commentaire = etapeDossier.Commentaire;
                        newetape.ModifierDate = System.DateTime.Now;
                        newetape.ModifierPar = user.LastName + " " + user.FirstName;
                        resultRequest = db.UpdateEtapeDossier(newetape);
                        if (resultRequest != -1)
                        {
                            if (!string.IsNullOrWhiteSpace(etapeDossier.DocumentFile))
                            {
                                resultRequest = db.InsertFichierDocumentEtapeDossier(resultRequest, etapeDossier.DocumentFile, System.DateTime.Now, user.LastName + " " + user.FirstName);
                            }
                            i++;
                        }
                    }
                }
                else
                {
                    EtapeDossierVM newetape = db.GetEtapeDossier(etapeDossier.DossierId, etapeDossier.EtapeId).FirstOrDefault();
                    if (newetape != null)
                    {
                        DateTime.TryParse(System.DateTime.Today.ToString(), out DateTime dateFin);
                        DateTime.TryParse(etapeDossier.DateDebut, out DateTime dateDebut);
                        newetape.DossierId = etapeDossier.DossierId;
                        newetape.DateDebut = dateDebut;
                        newetape.DateFin = dateFin;
                        newetape.Montant = etapeDossier.Montant;
                        newetape.Commentaire = etapeDossier.Commentaire;
                        newetape.ModifierDate = System.DateTime.Now;
                        newetape.ModifierPar = user.LastName + " " + user.FirstName;
                        resultRequest = db.UpdateEtapeDossier(newetape);
                        if (resultRequest != -1)
                        {
                            if (!string.IsNullOrWhiteSpace(etapeDossier.DocumentFile))
                            {
                                resultRequest = db.InsertFichierDocumentEtapeDossier(resultRequest, etapeDossier.DocumentFile, System.DateTime.Now, user.LastName + " " + user.FirstName);

                            }
                            i++;
                        }
                    }
                }
            }



            if (i == 0)
            {
                return NotFound(new { message = "Aucun élément n'est ajouté!" });
            }
            else
            {
                return Ok(new { ok = idList });
            }

        }
*/
    }
}
