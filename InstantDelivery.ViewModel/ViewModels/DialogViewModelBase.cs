using Caliburn.Micro;
using System;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel.ViewModels
{
    public abstract class DialogViewModelBase : Screen
    {
        private readonly TaskCompletionSource<int> tcs;

        internal Task Task => tcs.Task;

        protected DialogViewModelBase()
        {
            tcs = new TaskCompletionSource<int>();
        }

        public void Close()
        {
            tcs.SetResult(0);
            var handler = Closed;
            handler?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Closed;
    }
}
