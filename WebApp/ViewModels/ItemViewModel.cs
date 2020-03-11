using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class ItemViewModel
    {
        public ItemViewModel()
        {
            HasChild = true;
            Expanded = true;
            Items = new HashSet<ItemViewModel>();
        }
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string FileType { get; set; }
        public string Name { get; set; }
        public bool HasChild { get; set; }
        public bool Expanded { get; set; }
        public string IconCss { get; set; }
        public string Image { get; set; }
        public virtual ICollection<ItemViewModel> Items { get; set; }
    }
}
