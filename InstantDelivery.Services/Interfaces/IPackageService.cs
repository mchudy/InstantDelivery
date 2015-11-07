using InstantDelivery.Core.Entities;

namespace InstantDelivery.Services
{
    public interface IPackageService
    {
        bool AssignPackage(Package package, Employee employee);
        void RegisterPackage(Package package);
    }
}