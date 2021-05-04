using AutoMapper;
using System;
using techAPI.Models.Models;
using techAPI.Models.ModelsDto;

namespace techAPI.Models.SettingClass
{
    class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AddDeviceDto, Device>().AfterMap((src, dest) => dest.Id = Guid.NewGuid());

            CreateMap<UpdateDeviceDto, Device>().ReverseMap();
            
            CreateMap<DeviceDto, Device>().ReverseMap();

        }
    }
}
