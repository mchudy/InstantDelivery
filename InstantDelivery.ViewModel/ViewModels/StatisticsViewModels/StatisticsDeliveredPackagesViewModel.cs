using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Caliburn.Micro;
using InstantDelivery.Services;
using PropertyChanged;

namespace InstantDelivery.ViewModel
{
    public class StatisticsDeliveredPackagesViewModel : Screen
    {
        public StatisticsService service;
        public ObservableCollection<Population> Budget { get; } = new ObservableCollection<Population>();

        public StatisticsDeliveredPackagesViewModel(StatisticsService service)
        {
            this.service = service;

            var valueOfPackages = service.ValueOfAllPackages();
            var employeesSalaries = service.EmployeesSalaries();
            var taxes = service.Taxes(valueOfPackages, employeesSalaries);
            Budget.Add(new Population() { Name = "Wartość dostarczanych paczek", Count = valueOfPackages });
            Budget.Add(new Population() { Name = "Pensje pracowników", Count = employeesSalaries });
            Budget.Add(new Population() { Name = "Podatki", Count = taxes });
        }
    }
}