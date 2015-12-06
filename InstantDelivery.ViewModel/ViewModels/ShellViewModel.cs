using Caliburn.Micro;

namespace InstantDelivery.ViewModel
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive, IHandle<ShowEmployeesShellEvent>
    {
        private readonly IEventAggregator eventAggregator;

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public void Handle(ShowEmployeesShellEvent @event)
        {
            ActivateItem(IoC.Get<EmployeeShellViewModel>());
        }

        public void Logout()
        {
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        protected override void OnInitialize()
        {
            eventAggregator.Subscribe(this);
            DisplayName = "Instant Delivery";
            ActivateItem(IoC.Get<LoginViewModel>());
        }
    }
}
