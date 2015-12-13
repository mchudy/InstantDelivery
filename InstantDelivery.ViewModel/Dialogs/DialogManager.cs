using Caliburn.Micro;
using InstantDelivery.ViewModel.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InstantDelivery.ViewModel.Dialogs
{
    /// <summary>
    /// Umożliwia wyświetlanie komunikatów w głównym oknie aplikacji
    /// </summary>
    public class DialogManager : IDialogManager
    {
        public async Task ShowDialogAsync(DialogViewModelBase viewModel)
        {
            var viewType = ViewLocator.LocateTypeForModelType(viewModel.GetType(), null, null);
            var dialog = (BaseMetroDialog)Activator.CreateInstance(viewType);
            if (dialog == null)
            {
                throw new InvalidOperationException(
                    $"The view {viewType} belonging to view model {viewModel.GetType()} " +
                    $"does not inherit from {typeof(BaseMetroDialog)}");
            }
            dialog.DataContext = viewModel;

            MetroWindow firstMetroWindow =
                Application.Current.Windows.OfType<MetroWindow>().First();
            await firstMetroWindow.ShowMetroDialogAsync(dialog);
            await viewModel.Task;
            await firstMetroWindow.HideMetroDialogAsync(dialog);
        }
    }
}
