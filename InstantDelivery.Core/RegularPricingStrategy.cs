using InstantDelivery.Core.Entities;

namespace InstantDelivery.Core
{
    public class RegularPricingStrategy : IPricingStrategy
    {
        private const decimal dimensionalWeightFactor = 20000;
        private const decimal largePackageFactor = 1.5M;

        /// <summary>
        /// Oblicza koszt danej paczki
        /// </summary>
        /// <remarks>
        /// Koszt jest obliczany na podstawie następującego wzoru:
        ///     c = (Length * Width * Height) / 20000 * Weight
        /// Ponadto, koszt przesyłek zakwalifikowanych jako duże (tj. przekraczających
        /// 50 cm w co najmniej jednym z wymiarów) jest mnożony przez 1.5.
        /// </remarks>
        /// <param name="package"></param>
        /// <returns></returns>
        public decimal GetCost(Package package)
        {
            decimal result = DimensionalWeight(package) * package.Weight;
            if (IsSmall(package))
            {
                result *= largePackageFactor;
            }
            return result;
        }

        private bool IsSmall(Package package)
        {
            return package.Height <= 50 && package.Length <= 50 &&
                   package.Width <= 50;
        }

        private decimal DimensionalWeight(Package package)
        {
            return ((decimal)package.Length * (decimal)package.Width * (decimal)package.Height) /
                    dimensionalWeightFactor;
        }
    }
}