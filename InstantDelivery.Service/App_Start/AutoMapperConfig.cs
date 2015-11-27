using AutoMapper;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model;

namespace InstantDelivery.Service
{
    /// <summary>
    /// Klasa konfiguracyjna dla AutoMappera
    /// </summary>
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Package, PackageDto>();
            Mapper.CreateMap<Employee, EmployeeAddDto>().ReverseMap();
            Mapper.CreateMap<Employee, EmployeeDto>().ReverseMap();
            Mapper.CreateMap<Employee, EmployeePackagesDto>()
                .ForMember(s => s.Packages, c => c.MapFrom(m => m.Packages));
            Mapper.CreateMap<Vehicle, VehicleDto>();
        }
    }
}