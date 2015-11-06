using InstantDelivery.Core.Entities;

namespace InstantDelivery.Services
{
    public interface IPricingStrategy
    {
        decimal GetCost(Package package);
    }
}