using InstantDelivery.ViewModel.ViewModels;
using PropertyChanged;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku okna dialogowego z błędem
    /// </summary>
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
