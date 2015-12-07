using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstantDelivery.Domain.Entities;

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
