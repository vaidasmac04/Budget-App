using AutoMapper;
using Budget.Application.Incomes;
using Budget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Application.Common
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Income, IncomeDTO>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
             .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
             .ForMember(dest => dest.SourceName, opt => opt.MapFrom(src => src.Source.Name))
             .ReverseMap();
        }
    }
}
