using InstantDelivery.Model;
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
        private EmployeesServiceProxy service;

        public EmployeesManagedPackagesViewModel(EmployeesServiceProxy service)
        {
            this.service = service;
        }

        public new IList<EmployeePackagesDto> Employees { get; set; }

        protected override async void UpdateData()
        {
            var query = new PageQuery
            {
                PageSize = PageSize,
                PageIndex = CurrentPage,
                SortProperty = SortProperty,
                SortDirection = SortDirection,
            };
            AddFilters(query);
            service = new EmployeesServiceProxy();
            var pageDto = await service.PackagesPage(query);
            PageCount = pageDto.PageCount;
            Employees = pageDto.PageCollection;
        }
    }
}