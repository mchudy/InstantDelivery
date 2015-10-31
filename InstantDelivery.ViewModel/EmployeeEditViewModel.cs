using System;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.ViewModel
{
    public class EmployeeEditViewModel : Screen
    {
        public Employee Employee { get; set; }

        public void Save()
        {
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}
