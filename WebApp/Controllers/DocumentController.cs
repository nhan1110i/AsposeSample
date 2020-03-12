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
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents");
            var pathSave = Path.Combine(path, "Convert - " + docConvert.Name + ".pdf");
            if (docConvert.FileType == FileType.DOCX || docConvert.FileType == FileType.DOC)
            {
                var wordFile = new Aspose.Words.Document(GetPath(docConvert.Name + "." + docConvert.FileType));
                wordFile.Save(pathSave, Aspose.Words.SaveFormat.Pdf);
            }
            else if (docConvert.FileType == FileType.HTML)
            {
                HTMLDocument htmlFile = new HTMLDocument(GetPath(docConvert.Name + "." + docConvert.FileType));

                Converter.ConvertHTML(htmlFile, new Aspose.Html.Saving.PdfSaveOptions { JpegQuality = 100 }, pathSave);
            }
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
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents");
            var pathSave = Path.Combine(path, "Convert - " + docConvert.Name + ".docx");
            if (docConvert.FileType == FileType.PDF)
            {
                var pdfFile = new Aspose.Pdf.Document(GetPath(docConvert.Name + "." + docConvert.FileType));
                DocSaveOptions opt = new DocSaveOptions()
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };
                pdfFile.Save(pathSave, opt);

            }
            else if (docConvert.FileType == FileType.HTML)
            {
                var htmlFile = new HTMLDocument(GetPath(docConvert.Name + "." + docConvert.FileType));
                //TODO: convert
            }
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
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents");
            var pathSave = Path.Combine(path, "Convert - " + docConvert.Name + ".html");
            if(docConvert.FileType == FileType.PDF)
            {
                var pdfFile = new Aspose.Pdf.Document(GetPath(docConvert.Name + "." + docConvert.FileType));
                pdfFile.Save(pathSave, Aspose.Pdf.SaveFormat.Html);
            }else if(docConvert.FileType == FileType.DOCX || docConvert.FileType == FileType.DOC)
            {
                var docFile = new Aspose.Words.Document(GetPath(docConvert.Name + "." + docConvert.FileType));
                docFile.Save(pathSave, Aspose.Words.SaveFormat.Html);
            }
            await dr.AddAsync(new Document
            {
                Name = "Convert - " + docConvert.Name,
                FileType = FileType.HTML,
                DirectoryId = 1
            });
            return RedirectToAction("Index","Home");
        }
        private string GetPath(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents", fileName);
        }
    }
}