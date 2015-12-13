using System;

namespace InstantDelivery.ViewModel
{
    public class ShowShellEvent
    {
        public Type ViewModel { get; private set; }

        public ShowShellEvent(Type viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
