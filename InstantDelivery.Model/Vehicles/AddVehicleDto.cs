using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Model.Vehicles
{
    public class AddVehicleDto : ValidationBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        public string RegistrationNumber { get; set; }

        public int VehicleModelId { get; set; }
    }
}
