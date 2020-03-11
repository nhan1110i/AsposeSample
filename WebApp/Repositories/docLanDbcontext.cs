using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class docLanDbcontext : DbContext
    {
        public docLanDbcontext(DbContextOptions opt) : base(opt)
        {

        }
        public DbSet<Directory> Directories { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder model) {
            base.OnModelCreating(model);
            model.Entity<Directory>()
                .HasMany(dir => dir.SubDirectories)
                .WithOne(dir => dir.ParentDirectory)
                .HasForeignKey(dir => dir.ParentId);
            model.Entity<Document>(doc => {
                doc.HasOne(d => d.Directory)
                    .WithMany(dir => dir.Documents)
                    .HasForeignKey(doc => doc.DirectoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            model.Entity<Directory>().HasData(
                new Directory
                {
                    Id = 1,
                    Name = "Root",
                }
                );
        }
    }
}
