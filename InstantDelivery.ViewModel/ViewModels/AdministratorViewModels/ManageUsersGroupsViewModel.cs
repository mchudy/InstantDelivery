using Caliburn.Micro;
using InstantDelivery.Model;
using InstantDelivery.ViewModel.Proxies;
using PropertyChanged;

namespace InstantDelivery.ViewModel
{
    [ImplementPropertyChanged]
    public class ManageUsersGroupsViewModel : PagingViewModel
    {
        private UsersServiceProxy service;
        private readonly IWindowManager windowManager;
        private readonly ChangeUserRoleViewModel changeUserRoleViewModel;

        public ManageUsersGroupsViewModel(UsersServiceProxy service, IWindowManager windowManager,
            ChangeUserRoleViewModel changeUserRoleViewModel)
        {
            this.service = service;
            this.windowManager = windowManager;
            this.changeUserRoleViewModel = changeUserRoleViewModel;
        }

        protected override async void UpdateData()
        {
            var query = GetPageQuery();
            var pageDto = await service.Page(query);
            if (pageDto != null)
            {
                PageCount = pageDto.PageCount;
                Users = new BindableCollection<UserDto>(pageDto.PageCollection);
            }
        }

        public async void EditUser()
        {
            if (SelectedUser == null)
            {
                return;
            }
            changeUserRoleViewModel.User = SelectedUser;
            var result = windowManager.ShowDialog(changeUserRoleViewModel);
            if (result == true)
            {
                await service.ChangeRole(SelectedUser.UserName, SelectedUser.Role);
            }
            UpdateData();
        }

        public UserDto SelectedUser { get; set; }
        public BindableCollection<UserDto> Users { get; set; }
    }
}