using InstantDelivery.Core.Entities;

namespace InstantDelivery.Core
{
    public class PackageService
    {
        private InstantDeliveryContext context;

        public PackageService(InstantDeliveryContext context)
        {
            this.context = context;
        }

        //TODO czy powinno brać package, czy same dane
        public void RegisterPackage(Package package)
        {
            package.Status = PackageStatus.New;
            context.Packages.Add(package);
            context.SaveChanges();
        }
    }
}