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

        public ManageUsersGroupsViewModel(UsersServiceProxy service)
        {
            this.service = service;
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

        public BindableCollection<UserDto> Users { get; set; }
    }
}