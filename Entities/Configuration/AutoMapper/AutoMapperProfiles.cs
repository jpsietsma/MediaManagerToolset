using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Entities.Data.EF_Core.DatabaseEntities;
using Entities.Television;
using Entities.Television.ViewModels;

namespace Entities.Configuration.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Data.EF_Core.DatabaseEntities.TelevisionShow, TelevisionShowViewModel>();
            CreateMap<TelevisionLibraryScannedDirectory, TelevisionShowViewModel>();
            CreateMap<TelevisionShowViewModel, Data.EF_Core.DatabaseEntities.TelevisionShow>();
        }
    }
}
