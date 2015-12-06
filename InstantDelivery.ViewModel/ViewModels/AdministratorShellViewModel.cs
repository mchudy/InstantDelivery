using Caliburn.Micro;

namespace InstantDelivery.ViewModel
{
    public class AdministratorShellViewModel : Conductor<object>.Collection.OneActive
    {
        public AdministratorShellViewModel()
        {
            ActivateItem(IoC.Get<StartViewModel>());
        }

        /// <summary>
        /// Widok pracowników
        /// </summary>
        public void ManageUsersGroups()
        {
            ActivateItem(IoC.Get<ManageUsersGroupsViewModel>());
        }

    }
}