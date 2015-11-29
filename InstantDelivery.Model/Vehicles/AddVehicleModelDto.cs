namespace InstantDelivery.Model.Vehicles
{
    public class AddVehicleModelDto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public double Payload { get; set; }
        public double AvailableSpaceX { get; set; }
        public double AvailableSpaceY { get; set; }
        public double AvailableSpaceZ { get; set; }
    }
}
