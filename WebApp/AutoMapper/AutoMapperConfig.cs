using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Directory, ItemViewModel>()
                .ForMember(itm => itm.IconCss, opt => opt.NullSubstitute("folder"));
            CreateMap<Document, ItemViewModel>()
                .ForMember(itm => itm.ParentId, doc => doc.MapFrom(d => d.DirectoryId))
                .ForMember(itm => itm.IconCss, opt => opt.NullSubstitute("file"));                
        }
    }
}
