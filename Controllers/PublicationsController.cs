using CBPresenceLight.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CBPresenceLight.Controllers
{
    [EnableCors("AllowAll")]
    [Route("apiv.01/[controller]/[action]")]
    [ApiController]
    public class PublicationsController : ControllerBase
    {
        ///apiv.01/Publications/GetPublications
        private readonly IConfiguration _config;
        private readonly LinkGenerator _linkGenerator;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PublicationsController(IConfiguration config, LinkGenerator linkGenerator, IWebHostEnvironment hostingEnvironment)
        {
            _config = config;
            _linkGenerator = linkGenerator;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult<object> GetPublications(string latitude, string longitude, string adresse, string pays)
        {
            var db = new DataAccessController(_hostingEnvironment, _config);
            string accessKey = string.Empty;
           /* var proprietaire = db.GetProprietaire(accessKey);
            var headers = Request.Headers;
            if (headers != null && headers.ContainsKey("AccessKey"))
            {
                headers.TryGetValue("AccessKey", out Microsoft.Extensions.Primitives.StringValues value);
                accessKey = (string)value;
            }

            accessKey = new Functions(_config).Encrypt(accessKey);
            if (proprietaire == null)
            {
                return NotFound(new { message = "NotFound" });
            }*/
            var pay = db.GetPays().FirstOrDefault(p => (p.Description + "Haiti").Trim().ToLower() == (""+pays).Trim().ToLower());

            int quantite = db.GetPublicationVM( pay?.PaysId,null, ""+adresse).Distinct().Count();
            double latit = 0D;
            double longit = 0D;
            if (!string.IsNullOrWhiteSpace(latitude)) {
                latit = double.Parse(latitude);
            }
            if (!string.IsNullOrWhiteSpace(longitude))
            {
                longit = double.Parse(longitude);
            }
            

            var publications = db.GetPublicationVM(null).Distinct().AsEnumerable().OrderBy(c => c.DatePoste).ThenBy(c => new Functions(_config).Distance(c.Latitude!=null?double.Parse(c.Latitude):0D, latit, c.Longitude != null ? double.Parse( c.Longitude) :0, longit)).ThenBy(c => (""+c.Adresse).Trim().ToLower() == (""+adresse).Trim().ToLower()).ThenBy(c => c.CommuneId).ToList();

            if (publications.Count() == 0)
            {
                return NotFound(new { message = "Il y a toujours une nouvelle chose à voir !" });
            }
            
            
            return Ok(publications);

        }


        [HttpGet]
        public ActionResult<object> GetPublication(string latitude, string longitude, string adresse, string pays, int? publicationId)
        {
            var db = new DataAccessController(_hostingEnvironment, _config);
            string accessKey = string.Empty;
            /* var proprietaire = db.GetProprietaire(accessKey);
             var headers = Request.Headers;
             if (headers != null && headers.ContainsKey("AccessKey"))
             {
                 headers.TryGetValue("AccessKey", out Microsoft.Extensions.Primitives.StringValues value);
                 accessKey = (string)value;
             }

             accessKey = new Functions(_config).Encrypt(accessKey);
             if (proprietaire == null)
             {
                 return NotFound(new { message = "NotFound" });
             }*/
            var pay = db.GetPays().FirstOrDefault(p => (p.Description + "Haiti").Trim().ToLower() == ("" + pays).Trim().ToLower());

            int quantite = db.GetPublicationVM(pay?.PaysId, null, "" + adresse).Distinct().Count();
            double latit = 0D;
            double longit = 0D;
            if (!string.IsNullOrWhiteSpace(latitude)) {
                latit = double.Parse(latitude);
            }
            if (!string.IsNullOrWhiteSpace(longitude))
            {
                longit = double.Parse(longitude);
            }

            var publications = new List<PublicationVM>() { db.GetPublicationVMByPublicationId(publicationId)};
            if (publications.Count() == 0)
            {
                return NotFound(new { message = "Il y a toujours une nouvelle chose à voir !" });
            }
            
            
            return Ok(publications);

        }

        
        [HttpGet]
        public ActionResult<object> GetPublicationTypeEntrepriseId(int ?typeEntrepriseId, string latitude, string longitude, string adresse, string pays)
        {
            var db = new DataAccessController(_hostingEnvironment, _config);
            string accessKey = string.Empty;
           /* var proprietaire = db.GetProprietaire(accessKey);
            var headers = Request.Headers;
            if (headers != null && headers.ContainsKey("AccessKey"))
            {
                headers.TryGetValue("AccessKey", out Microsoft.Extensions.Primitives.StringValues value);
                accessKey = (string)value;
            }

            accessKey = new Functions(_config).Encrypt(accessKey);
            if (proprietaire == null)
            {
                return NotFound(new { message = "NotFound" });
            }*/
            var pay = db.GetPays().FirstOrDefault(p => (p.Description + "Haiti").Trim().ToLower() == (""+pays).Trim().ToLower());

            int quantite = db.GetPublicationVM(pay?.PaysId,null, ""+adresse).Distinct().Count();
            double latit = 0D;
            double longit = 0D;
            if (!string.IsNullOrWhiteSpace(latitude)) {
                latit = double.Parse(latitude);
            }
            if (!string.IsNullOrWhiteSpace(longitude))
            {
                longit = double.Parse(longitude);
            }
            

            var publications = db.GetPublicationVM(typeEntrepriseId).Distinct().AsEnumerable().OrderBy(c => c.DatePoste).OrderBy(c => new Functions(_config).Distance(c.Latitude!=null?double.Parse(c.Latitude):0D, latit, c.Longitude != null ? double.Parse( c.Longitude) :0, longit)).ThenBy(c => (""+c.Adresse).Trim().ToLower() == (""+adresse).Trim().ToLower()).OrderBy(c => c.CommuneId).ToList();

            if (publications.Count() == 0)
            {
                return NotFound(new { message = "Il y a toujours une nouvelle chose à voir !" });
            }
            
            
            return Ok(publications);

        }



        /*
        [HttpGet]
        public ActionResult<object> GetPublications(string latitude, string longitude, string adresse, string pays)
        {
            var db = new DataAccessController(_hostingEnvironment, _config);
            string accessKey = string.Empty;
           *//* var proprietaire = db.GetProprietaire(accessKey);
            var headers = Request.Headers;
            if (headers != null && headers.ContainsKey("AccessKey"))
            {
                headers.TryGetValue("AccessKey", out Microsoft.Extensions.Primitives.StringValues value);
                accessKey = (string)value;
            }

            accessKey = new Functions(_config).Encrypt(accessKey);
            if (proprietaire == null)
            {
                return NotFound(new { message = "NotFound" });
            }*//*
            var pay = db.GetPays().FirstOrDefault(p => (p.Description + "Haiti").Trim().ToLower() == (pays + "").Trim().ToLower());

            int quantite = db.GetPublicationVM(pay != null ? pay.PaysId : 0, adresse).Distinct().Count();
            double latit = 0D;
            double longit = 0D;
            if (!string.IsNullOrWhiteSpace(latitude)) {
                latit = double.Parse(latitude);
            }
            if (!string.IsNullOrWhiteSpace(longitude))
            {
                longit = double.Parse(longitude);
            }
            

            var publications = db.GetPublicationVM(pay != null ? pay.PaysId : 0, adresse).Distinct().AsEnumerable().Select(c => new
            PublicationList
            {
                Publication = c,
                PublicationFichier = db.GetPublicationFichierVM(c.PublicationId).ToList(),
                Commentaire = db.GetCommentaireVM(c.PublicationId),
                QuantiteCommentaire = db.GetCommentaireVM(c.PublicationId).Count(),
                QuantiteShared = db.GetQuantitePartage(c.PublicationId),
                Quantite = quantite,
                QuantiteLiked = db.GetQuantiteLike(c.PublicationId),
                QuantiteDisLiked = db.GetQuantiteDisLike(c.PublicationId)

            }).OrderBy(c => c.Publication.DatePoste).OrderBy(c => new Functions(_config).Distance(double.Parse(c.Publication.Latitude), latit, double.Parse(c.Publication.Longitude), longit)).ThenBy(c => (c.Publication.Adresse).Trim().ToLower() == (adresse).Trim().ToLower()).OrderBy(c => c.Publication.CommuneId).ToList();

            if (publications.Count() == 0)
            {
                return NotFound(new { message = "Il y a toujours une nouvelle chose à voir !" });
            }
            return Ok(publications);

        }
*/

        [HttpPost]
        public ActionResult<object> CreatePublication([FromBody] Publications publication)
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
            // accessKey = new Functions(_config).Encrypt(accessKey);
            accessKey = new Functions(_config).Encrypt(accessKey);
            var proprietaireExist = db.GetProprietaire(accessKey);
            if (proprietaireExist == null)
            {
                return NotFound(new { message = "Veuillez connecter" });
            }

            if (publication.Publication != null && publication.Publication.Count() > 0)
            {
                List<int> id = new List<int>();
                int resultRequest = -1;
                foreach (var item in publication.Publication)
                {
                    if (item == null)
                    {
                        Publication addPublication = new Publication
                        {
                            DatePoste = DateTime.Today,
                            Description = item.Description
                        };

                        resultRequest = db.InsertPublication(addPublication);
                        if (resultRequest != -1)
                        {
                            PublicationProprietaire publicationProprietaireProprietaire = new PublicationProprietaire
                            {
                                PublicationId = resultRequest,
                                EntrepriseId = item?.EntrepriseId,
                                ProprietaireId = item?.ProprietaireId,
                            };

                            db.InsertPublicationProprietaire(publicationProprietaireProprietaire);
                        }

                        string[] photos = item.Image.Split(",");
                        string[] videos = item.Image.Split(",");
                        if (photos != null)
                        {
                            foreach (var photo in photos)
                            {
                                db.InsertinsertPublicationFichier(resultRequest, photo, "");
                            }
                        }

                        if (videos != null)
                        {
                            foreach (var video in videos)
                            {
                                db.InsertinsertPublicationFichier(resultRequest, "", video);
                            }
                        }
                        id.Add(item.Id);
                    }
                }

                return Ok(id);
            }
            else
            {
                return NotFound(new { message = "Veuillez publier quelque choses!" });
            }

        }

        
        [HttpPost]
        public ActionResult<object> CreatePublicationLikeOrDislike([FromBody] LikeOrDislikes likeOrDislike)
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
            var proprietaireExist = db.GetProprietaire(accessKey);
        

            if (likeOrDislike.LikeOrDislike != null && likeOrDislike.LikeOrDislike.Count() > 0)
            {
                List<int> id = new List<int>();
                int resultRequest = -1;
                foreach (var item in likeOrDislike.LikeOrDislike)
                {
                    if (item == null)
                    {
                        PublicationVM publication = db.GetPublicationVMByPublicationId(item.PublicationId);
                        if (publication!=null)
                        {
                            LikeOrDislike likeOrDislik = new LikeOrDislike
                            {
                                Statut = item.Statut,
                                DateLike = DateTime.Now
                            };
                            resultRequest = db.InsertLikeOrDislike(likeOrDislik);
                        }
                       
                        if (resultRequest != -1)
                        {
                            InternautLikeDislike internautLikeDislike = new InternautLikeDislike
                            {
                                LikeOrDislikeId = resultRequest,
                                EntrepriseId = publication?.EntrepriseId,
                                ProprietaireId = publication?.ProprietaireId,
                                InternautId = proprietaireExist?.ProprietaireId,
                            };

                            db.InsertInternautLikeDislike(internautLikeDislike);
                        }
                       
                        id.Add(item.Id);
                    }
                }

                return Ok(id);
            }
            else
            {
                return NotFound(new { message = "Veuillez publier quelque choses!" });
            }

        }



        [HttpDelete]
        public ActionResult<object> DeletePublication(int publicationId)
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
            PublicationVM publication = db.GetPublicationVMByPublicationId(publicationId);

            if (publication != null && publication.ProprietaireId.HasValue)
            {
                Proprietaire proprietaire = db.GetProprietaire(publication.ProprietaireId ?? 0, accessKey);
                if (proprietaire != null)
                {
                    publication.StatutDelete = true;
                    publication.DateDelete = DateTime.Now;
                    int request = db.UpdateDeletePublication(publication);
                }
            }
            else if (publication != null && publication.EntrepriseId.HasValue)
            {
                Entreprise entreprise = db.GetEntreprise(publication.EntrepriseId ?? 0);
                if (entreprise != null)
                {
                    publication.StatutDelete = true;
                    publication.DateDelete = DateTime.Now;
                    int request = db.UpdateDeletePublication(publication);

                }
            }

            return Ok();

        }


    }
}
