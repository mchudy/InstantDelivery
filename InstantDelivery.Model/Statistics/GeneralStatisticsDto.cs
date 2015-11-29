namespace InstantDelivery.Model.Statistics
{
    public class GeneralStatisticsDto
    {
        public int EmployeesCount { get; set; }
        public int AllVehiclesCount { get; set; }
        public int AllPackagesCount { get; set; }
        public int AssignedPackages { get; set; }
        public int UnassignedPackages { get; set; }
        public int UsedVehicles { get; set; }
        public int UnusedVehicles { get; set; }
    }
}
