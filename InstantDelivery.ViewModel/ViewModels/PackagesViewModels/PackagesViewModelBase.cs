using System.ComponentModel;
using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using System.Linq;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Bazowy model widoku dlda paczek.
    /// </summary>
    public abstract class PackagesViewModelBase : Screen
    {
        private IQueryable<Package> packages;

        private int currentPage = 1;
        private string idFilter = string.Empty;

        /// <summary>
        /// Kolekcja skojarzona z tabelą danych.
        /// </summary>
        public IQueryable<Package> Packages
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
                UpdatePackages();
            }
        }

        /// <summary>
        /// Bieżąca strona
        /// </summary>
        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                NotifyOfPropertyChange();
            }
        }

        protected abstract IQueryable<Package> GetPackages();

        protected void UpdatePackages()
        {
            var newPackages = GetPackages();
            newPackages = FilterPackages(newPackages);
            Packages = newPackages;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            UpdatePackages();
        }

        private IQueryable<Package> FilterPackages(IQueryable<Package> newVehicles)
        {
            CurrentPage = 1;
            var tmp = newVehicles
                .Where(e => idFilter == "" || e.Id.ToString().StartsWith(idFilter));
            switch (PackageStatusFilter)
            {
                case PackageStatusFilter.Delivered:
                    return tmp.Where(e => e.Status == PackageStatus.Delivered);
                case PackageStatusFilter.InProgress:
                    return tmp.Where(e => e.Status == PackageStatus.InDelivery);
                case PackageStatusFilter.New:
                    return tmp.Where(e => e.Status == PackageStatus.New);
                default:
                    return tmp;
            }
        }

        private PackageStatusFilter packageStatusFilter = PackageStatusFilter.All;

        /// <summary>
        /// Filtr statusu paczki wybrany przez użytkownika
        /// </summary>
        public PackageStatusFilter PackageStatusFilter
        {
            get { return packageStatusFilter; }
            set
            {
                packageStatusFilter = value;
                UpdatePackages();
            }
        }
    }

    public enum PackageStatusFilter
    {
        [Description("Dostarczone")]
        Delivered,
        [Description("Nowe")]
        New,
        [Description("W toku")]
        InProgress,
        [Description("Wszystkie")]
        All
    }
}