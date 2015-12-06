using System.Collections.Generic;
using Caliburn.Micro;
using InstantDelivery.Common.Enums;
using InstantDelivery.Model;
using InstantDelivery.ViewModel.Proxies;

namespace InstantDelivery.ViewModel
{
    public abstract class CourierPackagesViewModelBase : PagingViewModel
    {
        private IList<PackageDto> packages;
        private string idFilter = string.Empty;
        private PackageStatusFilter packageStatusFilter = PackageStatusFilter.All;
        private string employeeId = string.Empty;
        private readonly PackagesServiceProxy service;

        protected CourierPackagesViewModelBase(PackagesServiceProxy service)
        {
            this.service = service;
        }

        public string EmployeeId
        {
            get { return employeeId; }
            set
            {
                employeeId = value;
                UpdateData();
            }
        }

        /// <summary>
        /// Kolekcja skojarzona z tabelą danych.
        /// </summary>
        public IList<PackageDto> Packages
        {
            get { return packages; }
            set
            {
                packages = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Filtr po ID wybrany przez użytkownika
        /// </summary>
        public string IdFilter
        {
            get { return idFilter; }
            set
            {
                idFilter = value;
                UpdateData();
            }
        }

        /// <summary>
        /// Filtr statusu paczki wybrany przez użytkownika
        /// </summary>
        public PackageStatusFilter PackageStatusFilter
        {
            get { return packageStatusFilter; }
            set
            {
                packageStatusFilter = value;
                UpdateData();
            }
        }

        protected override async void UpdateData()
        {
            var query = GetPageQuery();
            AddFilters(query);
            var pageDto = await service.PageWithSpecifiedEmployee(query);
            PageCount = pageDto.PageCount;
            Packages = pageDto.PageCollection;
        }

        private void AddFilters(PageQuery query)
        {
            query.Filters[nameof(PackageDto.Id)] = IdFilter;
            switch (PackageStatusFilter)
            {
                case PackageStatusFilter.Delivered:
                    query.Filters[nameof(PackageDto.Status)] = PackageStatus.Delivered.ToString();
                    break;
                case PackageStatusFilter.InDelivery:
                    query.Filters[nameof(PackageDto.Status)] = PackageStatus.InDelivery.ToString();
                    break;
                case PackageStatusFilter.New:
                    query.Filters[nameof(PackageDto.Status)] = PackageStatus.New.ToString();
                    break;
            }
            if (!string.IsNullOrEmpty(EmployeeId))
            {
                query.Filters[nameof(PackageDto.EmployeeId)] = EmployeeId;
            }
        }

    }
}