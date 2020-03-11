using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Repositories;
using WebApp.ViewModels;
using WebApp.Models;
namespace WebApp.Components
{
    public class DocumentViewComponent : ViewComponent
    {
        private readonly DocumentRepository doc;
        private readonly IMapper map;

        public DocumentViewComponent(DocumentRepository doc, IMapper map)
        {
            this.doc = doc;
            this.map = map;
        }
        public IViewComponentResult Invoke()
        {
            List<ItemViewModel> result = map.Map<List<Document>, List<ItemViewModel>>(doc.LocalData());
            return View(result);
        }
    }
}
