using InstantDelivery.Common.Enums;
using InstantDelivery.Model.Packages;
using InstantDelivery.Model.Paging;
using InstantDelivery.ViewModel.Proxies;
using System.Collections.Generic;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Bazowy model widoku dlda paczek.
    /// </summary>
    public abstract class PackagesViewModelBase : PagingViewModel
    {
        private IList<PackageDto> packages;
        private string idFilter = string.Empty;
        private PackageStatusFilter packageStatusFilter = PackageStatusFilter.All;
        private string employeeFilterId = null;
        private readonly PackagesServiceProxy service;

        protected PackagesViewModelBase(PackagesServiceProxy service)
        {
            this.service = service;
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

        public string EmployeeId
        {
            get
            {
                return employeeFilterId;
            }
            set
            {
                employeeFilterId = value;
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
            var pageDto = await service.Page(query);
            PageCount = pageDto?.PageCount ?? 0;
            Packages = pageDto?.PageCollection;
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
                    query.Filters[nameof(PackageDto.Status)] = PackageStatus.InWarehouse.ToString();
                    break;
            }
            if (!string.IsNullOrEmpty(EmployeeId))
                query.Filters[nameof(PackageDto.EmployeeId)] = EmployeeId;
        }
    }
}