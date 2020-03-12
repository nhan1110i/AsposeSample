using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Repositories;
using WebApp.Models;
using WebApp.Constant;

namespace WebApp.Controllers
{
    public class DocumentController : Controller
    {
        private readonly DocumentRepository dr;

        public DocumentController(DocumentRepository _dr)
        {
            dr = _dr;
        }
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> AddDocument()
        {
            string[] fileExtentions = { ".doc", ".docx", ".pdf", ".html" };
            var document = Request.Form.Files[0];
            if(Array.IndexOf(fileExtentions,Path.GetExtension(document.FileName)) != -1)
            {
                string FileType = Path.GetExtension(document.FileName).Split('.')[1];
                string FileName = document.FileName.Split('.')[0];
                await dr.AddAsync(new Document
                {
                    DirectoryId = 1,
                    FileType = FileType,
                    Name = FileName
                });
                var pathSave = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "documents", document.FileName);
                using (var steam = new FileStream(pathSave, FileMode.Create))
                {
                    await document.CopyToAsync(steam);
                }
            }
           
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> ConvertDocument(int fileId, string ToFileType)
        {
            Document docConvert = await
        }
    }
}