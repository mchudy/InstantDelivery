using Caliburn.Micro;

namespace InstantDelivery.ViewModel
{
    public class LoginViewModel : Screen
    {
        private readonly IEventAggregator eventAggregator;

        public LoginViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public string UserName { get; set; }
        public string Password { get; set; }

        public void Login()
        {
            eventAggregator.PublishOnUIThread(new ShowEmployeesShellEvent());
        }
    }
}
