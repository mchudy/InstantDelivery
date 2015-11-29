namespace InstantDelivery.Model
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public int VehicleModelId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public double Payload { get; set; }
        public double AvailableSpaceX { get; set; }
        public double AvailableSpaceY { get; set; }
        public double AvailableSpaceZ { get; set; }
    }
}
