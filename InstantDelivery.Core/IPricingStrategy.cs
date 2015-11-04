using InstantDelivery.Core.Entities;

namespace InstantDelivery.Core
{
    public interface IPricingStrategy
    {
        decimal GetCost(Package package);
    }
}