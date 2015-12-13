using InstantDelivery.ViewModel.ViewModels;
using PropertyChanged;

namespace InstantDelivery.ViewModel
{
    [ImplementPropertyChanged]
    public class ErrorDialogViewModel : DialogViewModelBase
    {
        /// <summary>
        /// Tytuł
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Wiadomość
        /// </summary>
        public string Message { get; set; }
    }
}
