using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Entities.Data.EF_Core.DatabaseEntities;
using Entities.Television.ViewModels;

namespace Entities.Configuration.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TelevisionShow, TelevisionShowViewModel>();
        }
    }
}
