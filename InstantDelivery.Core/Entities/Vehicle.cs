namespace InstantDelivery.Core.Entities
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string RegistrationNumber { get; set; }
        public virtual VehicleModel VehicleModel { get; set; }
    }
}
