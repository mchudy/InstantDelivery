using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantDelivery.Model.Vehicles
{
    public class AddVehicleDto
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public int VehicleModelId { get; set; }
    }
}
