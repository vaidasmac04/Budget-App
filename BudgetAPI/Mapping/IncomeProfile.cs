using AutoMapper;
using BudgetAPI.DTOs;
using BudgetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAPI.Mapping
{
    public class IncomeProfile : Profile
    {
        public IncomeProfile()
        {
            CreateMap<Income, IncomeDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.SourceName, opt => opt.MapFrom(src => src.Source.Name))
                .ReverseMap()
                .ForPath(src => src.Source.Name, opt => opt.Ignore());
        }
    }
}
