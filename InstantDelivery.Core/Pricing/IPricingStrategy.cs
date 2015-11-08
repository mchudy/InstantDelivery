using InstantDelivery.Core.Entities;

namespace InstantDelivery.Services
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