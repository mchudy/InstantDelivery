using Caliburn.Micro;
using InstantDelivery.ViewModel.Proxies;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace InstantDelivery.ViewModel
{
    public class LoginViewModel : Screen
    {
        private readonly IEventAggregator eventAggregator;
        private string message;

        public LoginViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public string UserName { get; set; }
        public SecureString Password { private get; set; }

        public void Login()
        {
            if (ServiceProxyBase.Login(UserName, SecureStringToString(Password)))
            {
                Message = "";
                eventAggregator.PublishOnUIThread(new ShowEmployeesShellEvent());
            }
            else
            {
                Message = "Niepoprawna nazwa użytkownika lub hasło";
            }
        }

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                NotifyOfPropertyChange();
            }
        }

        protected override void OnDeactivate(bool close)
        {
            Password.Dispose();
            base.OnDeactivate(close);
        }

        private static string SecureStringToString(SecureString value)
        {
            IntPtr bstr = Marshal.SecureStringToBSTR(value);
            try
            {
                return Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                Marshal.FreeBSTR(bstr);
            }
        }
    }
}
