using Caliburn.Micro;

namespace InstantDelivery.ViewModel
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive
    {
        private readonly IEventAggregator eventAggregator;

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);
            DisplayName = "Instant Delivery";

            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(object @event)
        {
            ActivateItem(IoC.Get<EmployeeShellViewModel>());
        }
    }
}
