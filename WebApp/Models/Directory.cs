using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Directory
    {
        public Directory()
        {
            SubDirectories = new HashSet<Directory>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public virtual Directory ParentDirectory { get; set; }
        public virtual ICollection<Directory> SubDirectories { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
