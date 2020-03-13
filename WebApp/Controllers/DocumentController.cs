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
using Directory = System.IO.Directory;
using Aspose.Html;
using Aspose.Html.Saving;
using Aspose.Html.Converters;
using Aspose.Pdf;
using Document = WebApp.Models.Document;
using WebApp.Common;

namespace WebApp.Controllers
{
    public class DocumentController : Controller
    {
        private readonly DocumentRepository dr;

        public DocumentController(DocumentRepository _dr)
        {
            dr = _dr;
        }
        [HttpGet]
        public async Task<IActionResult> CompareDocument(int doc1, int doc2)
        {
            
            Document firstDocument = await dr.GetByIdAsync(doc1);
            Document secondDocument = await dr.GetByIdAsync(doc2);
            //var compareWord = new Aspose.Words.Document(firstDocument.CompareDocument(secondDocument));
            var result = new Document
            {
                Name = firstDocument.CompareDocument(secondDocument),
                FileType = FileType.DOCX,
                Id = 0
            };
            return View(result);
        }
        public async Task<IActionResult> ViewDocumentById(int id)
        {
            var docView = await dr.GetByIdAsync(id);
            return View(docView);
        }
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> AddDocument()
        {
            string[] fileExtentions = { ".doc", ".docx", ".pdf", ".html" };
            var document = Request.Form.Files[0];
            if (Array.IndexOf(fileExtentions, Path.GetExtension(document.FileName)) != -1)
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

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ConvertToPdf(int id)
        {
            Document docConvert = await dr.GetByIdAsync(id);
            
            await dr.AddAsync(new Document
            {
                Name = "Convert - " + docConvert.Name,
                FileType = FileType.PDF,
                DirectoryId = 1
            });
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ConvertToWord(int id)
        {
            Document docConvert = await dr.GetByIdAsync(id);
            docConvert.ToWord();
            await dr.AddAsync(new Document
            {
                Name = "Convert - " + docConvert.Name,
                FileType = FileType.DOCX,
                DirectoryId = 1
            });
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ConvertToHtml(int id)
        {
            Document docConvert = await dr.GetByIdAsync(id);
            docConvert.ToHtml();
            await dr.AddAsync(new Document
            {
                Name = "Convert - " + docConvert.Name,
                FileType = FileType.HTML,
                DirectoryId = 1
            });
            return RedirectToAction("Index","Home");
        }
        
    }
}