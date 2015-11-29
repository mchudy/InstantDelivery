namespace InstantDelivery.Model
{
    public class EmployeeVehicleDto
    {
        /// <summary>
        /// Id pracownika
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Imię
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Nazwisko
        /// </summary>
        public string LastName { get; set; }

        public VehicleDto Vehicle { get; set; }
    }
}
