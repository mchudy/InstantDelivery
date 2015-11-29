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
            Mapper.CreateMap<Employee, EmployeeAddDto>().ReverseMap();
            Mapper.CreateMap<Employee, EmployeeDto>().ReverseMap();

            Mapper.CreateMap<Package, PackageDto>();
            Mapper.CreateMap<Employee, EmployeePackagesDto>()
                .ForMember(s => s.Packages, c => c.MapFrom(m => m.Packages));

            Mapper.CreateMap<Employee, EmployeeVehicleDto>();
            Mapper.CreateMap<Vehicle, VehicleDto>()
                .ForMember(s => s.Brand, c => c.MapFrom(m => m.VehicleModel.Brand))
                .ForMember(s => s.Model, c => c.MapFrom(m => m.VehicleModel.Model))
                .ForMember(s => s.Payload, c => c.MapFrom(m => m.VehicleModel.Payload))
                .ForMember(s => s.AvailableSpaceX, c => c.MapFrom(m => m.VehicleModel.AvailableSpaceX))
                .ForMember(s => s.AvailableSpaceY, c => c.MapFrom(m => m.VehicleModel.AvailableSpaceY))
                .ForMember(s => s.AvailableSpaceZ, c => c.MapFrom(m => m.VehicleModel.AvailableSpaceZ));
            Mapper.CreateMap<Vehicle, VehicleDto>();
        }
    }
}