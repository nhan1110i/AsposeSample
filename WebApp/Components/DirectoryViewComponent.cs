using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.AutoMapper;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.ViewModels;

namespace WebApp.ViewComponents
{
    public class DirectoryViewComponent : ViewComponent
    {

        private readonly DirectoryRepository dr;
        private readonly IMapper mapper;
        private readonly DocumentRepository doc;

        public DirectoryViewComponent(DirectoryRepository _dr, IMapper _mapper, DocumentRepository _doc)
        {
            dr = _dr;
            mapper = _mapper;
            doc = _doc;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Directory> directories = (await dr.GetListAsync()).ToList();
            List<Document> documents = (await doc.ListAllAsync()).ToList();
            List<ItemViewModel> result = new MapperToItemViewModel(mapper).ToItemViewModel(directories, documents);
            return View(result);
        }

    }
}
