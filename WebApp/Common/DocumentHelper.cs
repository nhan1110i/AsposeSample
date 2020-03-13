using Aspose.Html;
using Aspose.Html.Converters;
using Aspose.Pdf;
using Aspose.Words;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Constant;
using WebApp.Models;
using Directory = System.IO.Directory;
using Document = WebApp.Models.Document;

namespace WebApp.Common
{
    public static class DocumentHelper
    {
        public static string GetPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents");
        }
        public static void ToWord(this Document docConvert)
        {
            var pathSave = Path.Combine(GetPath(), "Convert - " + docConvert.Name + ".docx");
            if (docConvert.FileType == FileType.PDF)
            {
                var pdfFile = new Aspose.Pdf.Document(Path.Combine(GetPath(), docConvert.Name + "." + docConvert.FileType));
                DocSaveOptions opt = new DocSaveOptions()
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };
                pdfFile.Save(pathSave, opt);

            }
            else if (docConvert.FileType == FileType.HTML)
            {
                var htmlFile = new HTMLDocument(Path.Combine(GetPath(), docConvert.Name + "." + docConvert.FileType));
                //TODO: convert
            }
        }
        public static void ToHtml(this Document docConvert)
        {
            var pathSave = Path.Combine(GetPath(), "Convert - " + docConvert.Name + ".html");
            if (docConvert.FileType == FileType.PDF)
            {
                var pdfFile = new Aspose.Pdf.Document(Path.Combine(GetPath(), docConvert.Name + "." + docConvert.FileType));
                pdfFile.Save(pathSave, Aspose.Pdf.SaveFormat.Html);
            }
            else if (docConvert.FileType == FileType.DOCX || docConvert.FileType == FileType.DOC)
            {
                var docFile = new Aspose.Words.Document(Path.Combine(GetPath(), docConvert.Name + "." + docConvert.FileType));
                docFile.Save(pathSave, Aspose.Words.SaveFormat.Html);
            }
        }
        public static void ToPdf(this Document docConvert)
        {
            var pathSave = Path.Combine(GetPath(), "Convert - " + docConvert.Name + ".pdf");
            if (docConvert.FileType == FileType.DOCX || docConvert.FileType == FileType.DOC)
            {
                var wordFile = new Aspose.Words.Document(Path.Combine(GetPath(), docConvert.Name + "." + docConvert.FileType));
                wordFile.Save(pathSave, Aspose.Words.SaveFormat.Pdf);
            }
            else if (docConvert.FileType == FileType.HTML)
            {
                HTMLDocument htmlFile = new HTMLDocument(Path.Combine(GetPath(),docConvert.Name + "." + docConvert.FileType));

                Converter.ConvertHTML(htmlFile, new Aspose.Html.Saving.PdfSaveOptions { JpegQuality = 100 }, pathSave);
            }
        }
        public static string CompareDocument(this Document doc1, Document doc2)
        {
            if(doc1.FileType != "docx" || doc2.FileType != "docx")
            {
                return "Cant Compare";
            }
            var document1 = new Aspose.Words.Document(Path.Combine(GetPath(), doc1.Name + "." + doc1.FileType));
            var document2 = new Aspose.Words.Document(Path.Combine(GetPath(), doc2.Name + "." + doc2.FileType));
            string compareFileName = "Compare " + doc1.Name + " - " + doc2.Name + ".docx";
            var documentCompare = new Aspose.Words.Document(Path.Combine(GetPath(), doc1.Name + "." + doc1.FileType));
            DocumentBuilder builder = new DocumentBuilder(documentCompare); 
            //TODO: extends compare (use Revision Document)
            document1.Compare(document2, "nhan1110i", DateTime.Now);
            document1.Save(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Temp", compareFileName));
            return compareFileName;
        }
    }
}
