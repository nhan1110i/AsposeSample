using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class DirectoryRepository
    {
        private readonly docLanDbcontext db;

        public DirectoryRepository(docLanDbcontext _db)
        {
            db = _db;
        }
        public List<Directory> LocalData()
        {
            List<Directory> data = new List<Directory>();
            data.Add(new Directory
            {
                Id = 1,
                Name = "Root"
            });
            data.Add(new Directory
            {
                Id = 2,
                ParentId = 1,
                Name = "Folder 1",
            });
            data.Add(new Directory
            {
                Id = 3,
                ParentId = 1,
                Name = "Folder 2",
            });
            data.Add(new Directory
            {
                Id = 4,
                ParentId = 2,
                Name = "Folder 1.1",
            });
            data.Add(new Directory
            {
                Id = 5,
                ParentId = 2,
                Name = "Folder 1.2",
            });
            data.Add(new Directory
            {
                Id = 6,
                ParentId = 4,
                Name = "Folder 1.1.1",
            });
            data.Add(new Directory
            {
                Id = 7,
                ParentId = 4,
                Name = "Folder 1.1.2",
            }); 
            data.Add(new Directory
            {
                Id = 8,
                ParentId = 3,
                Name = "Folder 2.1"
            });
            data.Add(new Directory
            {
                Id = 9,
                ParentId = 3,
                Name = "Folder 2.2"
            });
            return data;
        }
        public async Task<IReadOnlyList<Directory>> ListAsync()
        {
            return await db.Directories.Include(dir => dir.SubDirectories).ToListAsync();
        }
        public async Task<Directory> AddAsync(Directory directory)
        {
            db.Directories.Add(directory);
            await db.SaveChangesAsync();
            return directory;
        }
    }
}
