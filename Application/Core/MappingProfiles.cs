using Application.Apartment;
using Application.Categories;
using Application.Excel;
using Application.Groups;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Group, Group>();
            CreateMap<Group, GroupDTO>();

            CreateMap<Category, CategoryDTO>();
            CreateMap<Domain.Apartment, ApartmentDTO>()
                .ForMember(obj => obj.StatusName, opt => opt.MapFrom(src => MapStatus(src.Status) ));
            CreateMap<ApartmentDTO, Domain.Apartment>();

            CreateMap<DataApartmentResult, Domain.Apartment>();

        }

        static string MapStatus(int status)
        {
            return status == 1 ? "Activo" : "Inactivo";
        }
    }
}