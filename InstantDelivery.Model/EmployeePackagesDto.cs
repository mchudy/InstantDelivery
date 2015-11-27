using System.Collections.Generic;

namespace InstantDelivery.Model
{
    public class EmployeePackagesDto
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

        public List<PackageDto> Packages { get; set; }
    }
}
