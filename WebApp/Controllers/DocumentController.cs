using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class DocumentController : Controller
    {
        [HttpPost]
        public IActionResult Upload(IFormFile document)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents", document.FileName);
            var stream = new FileStream(path, FileMode.Create);
            document.CopyToAsync(stream);
            return NotFound();
        }
    }
}