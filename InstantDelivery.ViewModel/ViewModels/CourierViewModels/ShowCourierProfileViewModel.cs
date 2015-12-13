using Caliburn.Micro;
using InstantDelivery.Model.Employees;
using InstantDelivery.ViewModel.Proxies;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku z danymi zalogowanego kuriera
    /// </summary>
    public class CourierProfileViewModel : Screen
    {
        private readonly EmployeesServiceProxy service;

        public CourierProfileViewModel(EmployeesServiceProxy service)
        {
            this.service = service;
            GetLoggedEmployeeData();
        }

        /// <summary>
        /// Zalogowany pracownik
        /// </summary>
        public EmployeeDto Employee { get; set; }

        /// <summary>
        /// Wczytuje dane zalogowanego użytkownika
        /// </summary>
        public async void GetLoggedEmployeeData()
        {
            Employee = await service.GetLoggedData();

        }
    }
}