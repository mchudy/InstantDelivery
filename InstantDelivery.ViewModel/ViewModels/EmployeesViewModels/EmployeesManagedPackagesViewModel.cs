using InstantDelivery.Model.Employees;
using InstantDelivery.ViewModel.Proxies;
using PropertyChanged;
using System.Collections.Generic;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku pracowników i paczek przez nich dostarczanych.
    /// </summary>
    [ImplementPropertyChanged]
    public class EmployeesManagedPackagesViewModel : EmployeesViewModelBase
    {
        private readonly EmployeesServiceProxy service;

        public EmployeesManagedPackagesViewModel(EmployeesServiceProxy service)
            : base(service)
        {
            this.service = service;
        }

        public new IList<EmployeePackagesDto> Employees { get; set; }

        protected override async void UpdateData()
        {
            var query = GetPageQuery();
            AddFilters(query);
            var pageDto = await service.PackagesPage(query);
            if (pageDto != null)
            {
                PageCount = pageDto.PageCount;
                Employees = pageDto.PageCollection;
            }
        }
    }
}