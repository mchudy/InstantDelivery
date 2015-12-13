namespace InstantDelivery.Model.Statistics
{
    /// <summary>
    /// Obiekt DTO reprezentujący ogólne dane statystyczne systemu
    /// </summary>
    public class GeneralStatisticsDto
    {
        /// <summary>
        /// Liczba pracowników
        /// </summary>
        public int EmployeesCount { get; set; }
        /// <summary>
        /// Liczba pojazdów
        /// </summary>
        public int AllVehiclesCount { get; set; }
        /// <summary>
        /// Liczba aktywnych przesyłek
        /// </summary>
        public int AllPackagesCount { get; set; }
        /// <summary>
        /// Liczba obsługiwanych przesyłek
        /// </summary>
        public int AssignedPackages { get; set; }
        /// <summary>
        /// Liczba nieobsługiwanych przesyłek
        /// </summary>
        public int UnassignedPackages { get; set; }
        /// <summary>
        /// Liczba używanych pojazdów
        /// </summary>
        public int UsedVehicles { get; set; }
        /// <summary>
        /// Liczba nieeużywanych pojazdów
        /// </summary>
        public int UnusedVehicles { get; set; }
    }
}
