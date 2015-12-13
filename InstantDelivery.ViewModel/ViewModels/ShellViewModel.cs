using Caliburn.Micro;
using InstantDelivery.ViewModel.Proxies;
using System;

namespace InstantDelivery.ViewModel
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive, IHandle<ShowShellEvent>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly AccountServiceProxy service;

        public ShellViewModel(IEventAggregator eventAggregator, AccountServiceProxy service)
        {
            this.eventAggregator = eventAggregator;
            this.service = service;
        }

        public void Handle(ShowShellEvent @event)
        {
            Type viewModel = @event.ViewModel;
            ActivateItem(IoC.GetInstance(viewModel, ""));
        }

        public void Logout()
        {
            service.Logout();
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
