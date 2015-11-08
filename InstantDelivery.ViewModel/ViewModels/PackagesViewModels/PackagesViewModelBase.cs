using System.Linq;
using Caliburn.Micro;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.ViewModel
{
    public abstract class PackagesViewModelBase : Screen
    {
        private IQueryable<Package> packages;

        private int currentPage = 1;
        private string idFilter = string.Empty;

        public IQueryable<Package> Packages
        {
            get { return packages; }
            set
            {
                packages = value;
                NotifyOfPropertyChange();
            }
        }

        public string IdFilter
        {
            get { return idFilter; }
            set
            {
                idFilter = value;
                UpdatePackages();
            }
        }

        protected abstract IQueryable<Package> GetPackages();

        protected void UpdatePackages()
        {
            var newPackages = GetPackages();
            newPackages = FilterPackages(newPackages);
            Packages = newPackages;
        }

        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                NotifyOfPropertyChange();
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            UpdatePackages();
        }

        private IQueryable<Package> FilterPackages(IQueryable<Package> newVehicles)
        {
            CurrentPage = 1;
            return newVehicles
                .Where(e => idFilter == "" || e.Id.ToString().StartsWith(idFilter));
        }
    }
}