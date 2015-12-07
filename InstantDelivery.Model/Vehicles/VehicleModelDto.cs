using System.ComponentModel.DataAnnotations;
using InstantDelivery.Domain.Entities;

namespace InstantDelivery.Model
{
    public class VehicleModelDto : ValidationBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Model { get; set; }
    }
}