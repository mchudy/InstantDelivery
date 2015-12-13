using Caliburn.Micro;
using InstantDelivery.Model;
using InstantDelivery.ViewModel.Proxies;
using System;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku zmiany hasła
    /// </summary>
    public class ChangePasswordViewModel : Screen
    {
        private readonly AccountServiceProxy service;

        public ChangePasswordViewModel(AccountServiceProxy service)
        {
            this.service = service;
        }

        /// <summary>
        /// Obiekt zawierający dane konieczne do zmiany hasła
        /// </summary>
        public ChangePasswordDto ChangePasswordDto { get; set; } = new ChangePasswordDto();

        /// <summary>
        /// Metoda wywoływana po wciśnięciu przez użytkownika przycisku
        /// zmiany hasła
        /// </summary>
        public async void ChangePassword()
        {
            await service.ChangePassword(ChangePasswordDto);
            Close();
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
