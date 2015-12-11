using System.Threading.Tasks;
using InstantDelivery.ViewModel.ViewModels;

namespace InstantDelivery.ViewModel.Dialogs
{
    public interface IDialogManager
    {
        Task ShowDialogAsync(DialogViewModelBase viewModel);
    }
}