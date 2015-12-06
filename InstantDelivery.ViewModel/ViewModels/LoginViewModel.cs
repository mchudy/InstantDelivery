using Caliburn.Micro;
using InstantDelivery.Common.Enums;
using InstantDelivery.ViewModel.Proxies;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;

namespace InstantDelivery.ViewModel
{
    public class LoginViewModel : Screen
    {
        private readonly IEventAggregator eventAggregator;
        private readonly AccountServiceProxy accountService;
        private string message;

        public LoginViewModel(IEventAggregator eventAggregator, AccountServiceProxy accountService)
        {
            this.eventAggregator = eventAggregator;
            this.accountService = accountService;
        }

        public string UserName { get; set; }
        public SecureString Password { private get; set; }

        public async void Login()
        {
            string password = SecureStringToString(Password);
            if (string.IsNullOrEmpty(UserName) || password == null)
            {
                Message = "Podaj nazwę użytkownika i hasło";
            }
            else if (ServiceProxyBase.Login(UserName, password))
            {
                Message = "";
                Role[] roles = await accountService.GetRoles();
                switch (roles.FirstOrDefault())
                {
                    case Role.Admin:
                        eventAggregator.PublishOnUIThread(new ShowShell(typeof(AdministratorShellViewModel)));
                        break;
                    case Role.AdministrativeEmployee:
                        eventAggregator.PublishOnUIThread(new ShowShell(typeof(EmployeeShellViewModel)));
                        break;
                    case Role.Courier:
                        eventAggregator.PublishOnUIThread(new ShowShell(typeof(CourierShellViewModel)));
                        break;
                    default:
                        return;
                }
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
            Password?.Dispose();
            base.OnDeactivate(close);
        }

        private static string SecureStringToString(SecureString value)
        {
            if (value == null)
            {
                return null;
            }
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
