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

        /// <summary>
        /// Nazwa użytkownika
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Hasło
        /// </summary>
        public SecureString Password { private get; set; }

        /// <summary>
        /// Metoda wywoływana na zalogowanie.
        /// </summary>
        public async void Login()
        {
            string password = SecureStringToString(Password);
            if (string.IsNullOrEmpty(UserName) || password == null)
            {
                Message = "Podaj nazwę użytkownika i hasło";
            }
            else if (accountService.Login(UserName, password))
            {
                Message = "";
                Role[] roles = await accountService.GetRoles();
                switch (roles.FirstOrDefault())
                {
                    case Role.Admin:
                        eventAggregator.PublishOnUIThread(new ShowShellEvent(typeof(AdministratorShellViewModel)));
                        break;
                    case Role.AdministrativeEmployee:
                        eventAggregator.PublishOnUIThread(new ShowShellEvent(typeof(EmployeeShellViewModel)));
                        break;
                    case Role.Courier:
                        eventAggregator.PublishOnUIThread(new ShowShellEvent(typeof(CourierShellViewModel)));
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

        /// <summary>
        /// Wiadomość
        /// </summary>
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Metoda wywoływana na deaktywację okna
        /// </summary>
        /// <param name="close"></param>
        protected override void OnDeactivate(bool close)
        {
            Password?.Dispose();
            base.OnDeactivate(close);
        }

        /// <summary>
        /// Konwertuje secure string do string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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
