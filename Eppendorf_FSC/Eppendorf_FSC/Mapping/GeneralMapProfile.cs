using AutoMapper;
using Eppendorf_FSC.Core.Dto;
using Eppendorf_FSC.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eppendorf_FSC.Mapping
{
    public class GeneralMapProfile : Profile
    {
        public GeneralMapProfile()
        {
            CreateMap<FileSeedDevice, Device>()
                .ForMember(d=> d.Id,f=> f.MapFrom(src=> src.id))
                .ForMember(d => d.Location, f => f.MapFrom(src => src.location))
                .ForMember(d => d.Type, f => f.MapFrom(src => src.type))
                .ForMember(d => d.DeviceHealth, f=> f.MapFrom(src => src.device_health))
                .ForMember(d => d.LastUsed, f => f.ConvertUsing(new FileDateConverter(),s=> s.last_used)) //use fileDateConverter to parse the file format to DateTime
                .ForMember(d => d.Price, f => f.ConvertUsing(new FilePriceConverter(),s => s.price)) // use FilePriceConverter to parse value correct
                .ForMember(d => d.Color, f => f.MapFrom(src => src.color))
                .ReverseMap();
        }

    }
}
