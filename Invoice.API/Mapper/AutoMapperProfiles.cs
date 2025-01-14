using AutoMapper;
using Invoice.Data.Dto;
using Invoice.Data.Entity;
using Invoice.Models;

namespace Invoice.Data.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<InvoiceDto, InvoiceModel>().ReverseMap();
            CreateMap<CreateInvoiceDto, InvoiceModel>().ReverseMap();
            CreateMap<InvoiceEntity, InvoiceModel>().ReverseMap();
            CreateMap<InvoiceEntity, InvoiceModel>().ReverseMap();
            CreateMap<InvoiceAddedDto, InvoiceModel>().ReverseMap();
            CreateMap<InvoiceModel, InvoiceDto>()
                .ForMember(dest => dest.Due_date, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Due_date)));
            CreateMap<InvoiceDto, InvoiceModel>()
                .ForMember(dest => dest.Due_date, opt => opt.MapFrom(src => src.Due_date.ToDateTime(TimeOnly.MinValue)));
            
            CreateMap<InvoiceModel, CreateInvoiceDto>()
                .ForMember(dest => dest.Due_date, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Due_date)));
            
            CreateMap<CreateInvoiceDto, InvoiceModel>()
                .ForMember(dest => dest.Due_date, opt => opt.MapFrom(src => src.Due_date.ToDateTime(TimeOnly.MinValue)));
        }
    }
}
