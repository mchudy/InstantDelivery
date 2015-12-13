using Caliburn.Micro;
using InstantDelivery.ViewModel.Proxies;
using System;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Główny model widoku
    /// </summary>
    public class ShellViewModel : Conductor<object>.Collection.OneActive, IHandle<ShowShellEvent>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly AccountServiceProxy service;

        public ShellViewModel(IEventAggregator eventAggregator, AccountServiceProxy service)
        {
            this.eventAggregator = eventAggregator;
            this.service = service;
        }

        /// <summary>
        /// Invoked on handling event.
        /// </summary>
        /// <param name="@event"></param>
        public void Handle(ShowShellEvent @event)
        {
            Type viewModel = @event.ViewModel;
            ActivateItem(IoC.GetInstance(viewModel, ""));
        }

        /// <summary>
        /// Invoked on logout.
        /// </summary>
        public void Logout()
        {
            service.Logout();
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        /// <summary>
        /// Invoked on initializing.
        /// </summary>
        protected override void OnInitialize()
        {
            eventAggregator.Subscribe(this);
            DisplayName = "Instant Delivery";
            ActivateItem(IoC.Get<LoginViewModel>());
        }
    }
}
