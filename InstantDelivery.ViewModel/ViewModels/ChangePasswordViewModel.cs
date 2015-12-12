using Caliburn.Micro;
using InstantDelivery.Model;
using InstantDelivery.ViewModel.Proxies;
using System;

namespace InstantDelivery.ViewModel
{
    public class ChangePasswordViewModel : Screen
    {
        private readonly AccountServiceProxy service;

        public ChangePasswordViewModel(AccountServiceProxy service)
        {
            this.service = service;
        }

        public ChangePasswordDto ChangePasswordDto { get; set; } = new ChangePasswordDto();

        public async void ChangePassword()
        {
            await service.ChangePassword(ChangePasswordDto);
        }

        public void Close()
        {
            TryClose(false);
        }

        public override void CanClose(Action<bool> callback)
        {
            callback(true);
        }
    }
}
