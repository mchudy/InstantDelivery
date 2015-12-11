using Caliburn.Micro;
using InstantDelivery.ViewModel.Dialogs;
using InstantDelivery.ViewModel.Proxies;
using System;

namespace InstantDelivery.ViewModel
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive, IHandle<ShowShell>
    {
        private readonly IEventAggregator eventAggregator;

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public void Handle(ShowShell @event)
        {
            Type viewModel = @event.ViewModel;
            ActivateItem(IoC.GetInstance(viewModel, ""));
        }

        public void Logout()
        {
            ServiceProxyBase.Logout();
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        protected override void OnInitialize()
        {
            eventAggregator.Subscribe(this);
            DisplayName = "Instant Delivery";
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await new DialogManager().ShowDialogAsync(new ErrorDialogViewModel
            {
                Title = "Błąd",
                Message = "Wystąpił błąd"
            });
        }
    }



}
