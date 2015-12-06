using InstantDelivery.Model;
using InstantDelivery.ViewModel.Proxies;

namespace InstantDelivery.ViewModel.CourierViewModels
{
    public class ShowCourierProfileViewModel
    {
        // the logged Employee
        public EmployeeDto Employee { get; set; }

        private EmployeesServiceProxy service { get; set; }

        // somehow load the employee
        public ShowCourierProfileViewModel(EmployeesServiceProxy service)
        {
            this.service = service;
        }

    }
}