using Caliburn.Micro;
using InstantDelivery.ViewModel.Proxies;

namespace InstantDelivery.ViewModel
{
    public class ManageUsersGroupsViewModel : Screen
    {
        private EmployeesServiceProxy service;

        public ManageUsersGroupsViewModel(EmployeesServiceProxy service)
        {
            this.service = service;
        }

    }
}