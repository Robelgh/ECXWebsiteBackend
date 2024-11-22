using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Web;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;

namespace ECX.Website.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {


        // GET: api/<AboutEcxController>
        [HttpGet("{filenameorginal}/{filenamenew}")]
        public async Task<ActionResult> Get(string filenameorginal, string filenamenew)
        {
            string fileName = filenameorginal;

            string pdflocation = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\pdf\" + fileName);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(pdflocation, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(pdflocation);
            return File(bytes, contentType, Path.GetFileName(filenamenew + ".pdf"));



        }
    }
}
