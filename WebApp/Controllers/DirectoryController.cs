using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class DirectoryController : Controller
    {
        private readonly DirectoryRepository dr;
        private readonly IMapper mapper;
        private readonly DocumentRepository doc;

        public DirectoryController(DirectoryRepository _dr, IMapper _mapper, DocumentRepository _doc)
        {
            dr = _dr;
            mapper = _mapper;
            doc = _doc;
        }
        public IActionResult Explorers()
        {
            List<Directory> directories = dr.LocalData();
            List<ItemViewModel> result = mapper.Map<List<Directory>, List<ItemViewModel>>(directories);
            List<Document> documents = doc.LocalData();
            result.Add(mapper.Map<List<Document>, List<ItemViewModel>>(documents)[0]);
            return PartialView("_Explorer",result);
        }
    }
}