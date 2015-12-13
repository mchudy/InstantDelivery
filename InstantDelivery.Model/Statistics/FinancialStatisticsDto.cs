namespace InstantDelivery.Model.Statistics
{
    /// <summary>
    /// Obiekt DTO reprezentujący dane statystyczne dot. finansów systemu
    /// </summary>
    public class FinancialStatisticsDto
    {
        /// <summary>
        /// Wartość wszystkich paczek
        /// </summary>
        public decimal TotalPackagesValue { get; set; }
        /// <summary>
        /// Wartość wszystkich pensji
        /// </summary>
        public decimal TotalEmployeesSalaries { get; set; }
        /// <summary>
        /// Wartość wszystkich podatków
        /// </summary>
        public decimal TotalTaxes { get; set; }
    }
}
