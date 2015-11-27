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
            Mapper.CreateMap<Employee, EmployeeDto>();
        }
    }
}