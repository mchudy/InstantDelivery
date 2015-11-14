using InstantDelivery.Domain.Entities;

namespace InstantDelivery.Core.Pricing
{
    public interface IPricingStrategy
    {
        /// <summary>
        /// Metoda oblicza koszt paczki na podstawie jej wymiarów i ciężaru. 
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        decimal GetCost(Package package);
    }
}