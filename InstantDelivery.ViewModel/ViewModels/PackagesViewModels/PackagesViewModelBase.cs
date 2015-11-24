using InstantDelivery.Domain.Entities;
using InstantDelivery.Services;
using InstantDelivery.Services.Paging;
using System.Collections.Generic;
using System.ComponentModel;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Bazowy model widoku dlda paczek.
    /// </summary>
    public abstract class PackagesViewModelBase : PagingViewModel
    {
        private IList<Package> packages;
        private string idFilter = string.Empty;
        private PackageStatusFilter packageStatusFilter = PackageStatusFilter.All;

        private IPackageService service;

        protected PackagesViewModelBase(IPackageService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Kolekcja skojarzona z tabelą danych.
        /// </summary>
        public IList<Package> Packages
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

        protected override void UpdateData()
        {
            var query = new PageQuery<Package>
            {
                PageSize = PageSize,
                PageIndex = CurrentPage,
                SortProperty = SortProperty,
                SortDirection = SortDirection,
            };
            AddFilters(query);
            var pageDto = service.GetPage(query);
            PageCount = pageDto.PageCount;
            Packages = pageDto.PageCollection;
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

        private void AddFilters(PageQuery<Package> query)
        {
            query.Filters.Add(e => string.IsNullOrEmpty(idFilter) || e.Id.ToString().StartsWith(idFilter));
            switch (PackageStatusFilter)
            {
                case PackageStatusFilter.Delivered:
                    query.Filters.Add(e => e.Status == PackageStatus.Delivered);
                    break;
                case PackageStatusFilter.InProgress:
                    query.Filters.Add(e => e.Status == PackageStatus.InDelivery);
                    break;
                case PackageStatusFilter.New:
                    query.Filters.Add(e => e.Status == PackageStatus.New);
                    break;
            }
        }
    }

    public enum PackageStatusFilter
    {
        [Description("Dostarczone")]
        Delivered,
        [Description("Nowe")]
        New,
        [Description("W dostawie")]
        InProgress,
        [Description("Wszystkie")]
        All
    }
}