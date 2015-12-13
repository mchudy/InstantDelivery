using InstantDelivery.Model.Packages;
using System;

namespace InstantDelivery.Service.Pricing
{
    public class RegularPricingStrategy : IPricingStrategy
    {
        private const decimal dimensionalWeightFactor = 20000;
        private const decimal largePackageFactor = 1.5M;

        /// <summary>
        /// Metoda oblicza koszt danej paczki
        /// </summary>
        /// <remarks>
        /// Koszt jest obliczany na podstawie następującego wzoru:
        ///     c = (Length * Width * Height) / 20000 * Weight
        /// Ponadto, koszt przesyłek zakwalifikowanych jako duże (tj. przekraczających
        /// 50 cm w co najmniej jednym z wymiarów) jest mnożony przez 1.5.
        /// </remarks>
        /// <param name="package"></param>
        /// <returns></returns>
        public decimal GetCost(PackageDto package)
        {
            if (package == null)
                return 0;
            decimal result = DimensionalWeight(package) * package.Weight;
            if (IsSmall(package))
            {
                result *= largePackageFactor;
            }
            return decimal.Round(result, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Metoda zwraca informację, czy paczka jest mała.
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        private bool IsSmall(PackageDto package)
        {
            return package.Height <= 50 && package.Length <= 50 &&
                   package.Width <= 50;
        }

        /// <summary>
        /// Metoda oblicza wielkość wymiarową.
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        private decimal DimensionalWeight(PackageDto package)
        {
            if (package == null)
                return 0;
            return ((decimal)package.Length * (decimal)package.Width * (decimal)package.Height) /
                    dimensionalWeightFactor;
        }
    }
}