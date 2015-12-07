using Caliburn.Micro;
using InstantDelivery.Model.Employees;
using InstantDelivery.ViewModel.Proxies;

namespace InstantDelivery.ViewModel
{
    public class CourierProfileViewModel : Screen
    {
        // the logged Employee
        public EmployeeDto Employee { get; set; }

        public async void GetLoggedEmployeeData()
        {
            Employee = await service.GetLoggedData();

        }

        private EmployeesServiceProxy service { get; set; }

        // somehow load the employee
        public CourierProfileViewModel(EmployeesServiceProxy service)
        {
            this.service = service;
            GetLoggedEmployeeData();
        }
    }
}