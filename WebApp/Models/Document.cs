using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public int? DirectoryId { get; set; }
        public virtual Directory Directory { get; set; }
    }
}
