using System;

namespace InstantDelivery.ViewModel
{
    public class ShowShell
    {
        public Type ViewModel { get; private set; }

        public ShowShell(Type viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
