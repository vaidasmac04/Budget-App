using AutoMapper;
using Budget.Application.Incomes;
using Budget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Application
{
    /*
     * Maps incomeDTO to income and vice versa
     * 
     * Note: income has complex property Source which is mapped to 
     *       incomeDTO as IncomeDTO.SourceName = Income.Source.Name
     *       when mapping IncomeDTO back to Income, Income.Source 
     *       property is not mapped backed
    */
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
                .ForPath(src => src.Source.Name, opt => opt.Ignore()); ;
        }
    }
}
