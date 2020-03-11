using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
namespace WebApp.Repositories
{
    public class DocumentRepository
    {
        private readonly docLanDbcontext db;

        public DocumentRepository(docLanDbcontext _db)
        {
            db = _db;
        }
        public List<Document> LocalData()
        {
            List<Document> data = new List<Document>();
            data.Add(new Document { 
                Id = 100,
                DirectoryId = 2,
                FileType = ".pdf",
                Name = "Lorem 1"
            });
            data.Add(new Document
            {
                Id = 100,
                DirectoryId = 2,
                FileType = ".pdf",
                Name = "Lorem 2"
            });
            data.Add(new Document
            {
                Id = 100,
                DirectoryId = 2,
                FileType = ".pdf",
                Name = "Lorem 3"
            });
            data.Add(new Document
            {
                Id = 100,
                DirectoryId = 3,
                FileType = ".pdf",
                Name = "Lorem 4"
            });
            data.Add(new Document
            {
                Id = 100,
                DirectoryId = 3,
                FileType = ".pdf",
                Name = "Lorem 5"
            });
            data.Add(new Document
            {
                Id = 100,
                DirectoryId = 4,
                FileType = ".pdf",
                Name = "Lorem 6"
            });
            data.Add(new Document
            {
                Id = 100,
                DirectoryId = 4,
                FileType = ".pdf",
                Name = "Lorem 7"
            });
            data.Add(new Document
            {
                Id = 100,
                DirectoryId = 4,
                FileType = ".pdf",
                Name = "Lorem 8"
            });
            data.Add(new Document
            {
                Id = 100,
                DirectoryId = 5,
                FileType = ".pdf",
                Name = "Lorem 9"
            });
            data.Add(new Document
            {
                Id = 100,
                DirectoryId = 5,
                FileType = ".pdf",
                Name = "Lorem 10"
            });
            return data;
        }
    }
}
