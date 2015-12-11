using InstantDelivery.ViewModel.ViewModels;
using PropertyChanged;

namespace InstantDelivery.ViewModel
{
    [ImplementPropertyChanged]
    public class ErrorDialogViewModel : DialogViewModelBase
    {
        public string Title { get; set; }

        public string Message { get; set; }
    }
}
