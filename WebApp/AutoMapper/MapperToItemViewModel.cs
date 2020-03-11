using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.AutoMapper
{
    public class MapperToItemViewModel
    {
        private readonly IMapper _map;
        public MapperToItemViewModel(IMapper _mapper)
        {
            this._map = _mapper;
        }
        public List<ItemViewModel> ToItemViewModel(List<Directory> directories, List<Document> documents)
        {
            List<ItemViewModel> result = new List<ItemViewModel>();
            result.AddRange(_map.Map<List<Directory>, List<ItemViewModel>>(directories));
            result.AddRange(_map.Map<List<Document>, List<ItemViewModel>>(documents));
            return result;
        }
    }
}
