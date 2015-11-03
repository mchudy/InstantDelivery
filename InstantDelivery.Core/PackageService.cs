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

        public void RegisterPackage(Package package)
        {
            throw new System.NotImplementedException();
        }
    }
}