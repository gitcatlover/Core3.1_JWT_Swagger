using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core3._1_JWT_Swagger.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string[]>> Articles()
        {
            return Ok(new string[] { "C#本质论", "全球知名双标" });
        }

        [HttpPost]
        [Authorize]
        public ActionResult<IEnumerable<string[]>> AddArticle()
        {
            return Ok(new string[] { "Success" });
        }
    }
}
