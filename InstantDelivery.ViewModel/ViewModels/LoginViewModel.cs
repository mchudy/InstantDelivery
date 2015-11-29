using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace InstantDelivery.ViewModel
{
    public class LoginViewModel : Screen
    {
        public void Cancel()
        {
            TryClose(false);
        }

        public void Login()
        {
            if (/*proxyService.AttemptLogin(username, password)==*/true)
            {
                TryClose(true);
                // ?
            }
        }

        public Visibility DisplayForgotPasswordCredentials { get; set; } = Visibility.Collapsed;

        public void ForgotPassword()
        {
            DisplayForgotPasswordCredentials = DisplayForgotPasswordCredentials == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            DisplaySendEmailConfirmation = Visibility.Collapsed;
        }

        public Visibility DisplaySendEmailConfirmation { get; set; } = Visibility.Collapsed;



        public void SendPassword()
        {
            if ( /*/proxyService.SendPassword(emailAddress);==*/true)
                DisplaySendEmailConfirmation = Visibility.Visible;
        }
        //public SplashScreenViewModel(/*ISchoolTestMakerServiceProxy serviceProxy*/)
        //{
        //    //this.serviceProxy = serviceProxy;
        //}
    }
}
