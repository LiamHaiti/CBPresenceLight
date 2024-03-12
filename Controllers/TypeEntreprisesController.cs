using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CBPresenceLight.Controllers
{
    [EnableCors("AllowAll")]
    [Route("apiv.01/[controller]/[action]")]
    [ApiController]
    public class TypeEntreprisesController : Controller
    {

        private readonly IConfiguration _config;
        private readonly LinkGenerator _linkGenerator;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public TypeEntreprisesController(IConfiguration config, LinkGenerator linkGenerator, IWebHostEnvironment hostingEnvironment)
        {
            _config = config;
            _linkGenerator = linkGenerator;
            _hostingEnvironment = hostingEnvironment;
        }


        public ActionResult<object> GetTypeEntreprises()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var response = new HttpResponseMessage();

            var db = new DataAccessController(_hostingEnvironment, _config);

            var typeEntreprises = db.GetTypeEntreprises().Select(
                p => new
                {
                    TypeEntrepriseId = p.TypeEntrepriseId,
                    Description = p.Description,

                }).ToList();
            return Ok(typeEntreprises);

        }

        public ActionResult<object> GetTypeEntreprise(int? id)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var response = new HttpResponseMessage();

            var db = new DataAccessController(_hostingEnvironment, _config);

            var typeEntreprise = db.GetTypeEntreprise(id??0);

            return Ok(typeEntreprise);

        }

    }
}
