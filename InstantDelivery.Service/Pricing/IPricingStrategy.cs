using InstantDelivery.Model;

namespace InstantDelivery.Service.Pricing
{
    public interface IPricingStrategy
    {
        /// <summary>
        /// Metoda oblicza koszt paczki na podstawie jej wymiarów i ciężaru. 
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        decimal GetCost(PackageDto package);
    }
}