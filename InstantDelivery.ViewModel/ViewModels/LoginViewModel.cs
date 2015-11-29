using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using InstantDelivery.ViewModel.Proxies;

namespace InstantDelivery.ViewModel
{
    public class LoginViewModel : Screen
    {
        public void Cancel()
        {
            TryClose(false);
        }

        private LoginServiceProxy proxy;

        public LoginViewModel(LoginServiceProxy proxy)
        {
            this.proxy = proxy;
        }

        public string login { get; set; }
        public SecureString password { get; set; }

        public string email { get; set; }

        public async void Login()
        {
            try
            {
                await proxy.Login(login, password);
                TryClose(true);
                // ?
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public Visibility DisplayForgotPasswordCredentials { get; set; } = Visibility.Collapsed;

        public void ForgotPassword()
        {
            DisplayForgotPasswordCredentials = DisplayForgotPasswordCredentials == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            DisplaySendEmailConfirmation = Visibility.Collapsed;
        }

        public Visibility DisplaySendEmailConfirmation { get; set; } = Visibility.Collapsed;

        public async void SendPassword()
        {
            try
            {
                await proxy.SendPassword(email);
                DisplaySendEmailConfirmation = Visibility.Visible;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
