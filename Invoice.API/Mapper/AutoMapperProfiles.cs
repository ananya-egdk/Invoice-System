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
        }
    }
}
