using Caliburn.Micro;
using InstantDelivery.Model.Paging;
using InstantDelivery.Model.Vehicles;
using InstantDelivery.ViewModel.Proxies;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Bazowy modeul widoku dla widoków pracowników.
    /// </summary>
    public abstract class VehiclesViewModelBase : PagingViewModel
    {
        private readonly VehiclesServiceProxy service;
        private BindableCollection<VehicleDto> vehicles;

        private string brandFilter = string.Empty;
        private string modelFilter = string.Empty;
        private string registrationNumberFilter = string.Empty;

        protected VehiclesViewModelBase(VehiclesServiceProxy service)
        {
            this.service = service;
        }

        /// <summary>
        /// Kolekcja skojarzona z taelą danych.
        /// </summary>
        public BindableCollection<VehicleDto> Vehicles
        {
            get { return vehicles; }
            set
            {
                vehicles = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Filtr po marce wpisany przez użytkownika.
        /// </summary>
        public string BrandFilter
        {
            get { return brandFilter; }
            set
            {
                brandFilter = value;
                UpdateData();
            }
        }

        /// <summary>
        /// Filtr po modelu wpisany przez użytkownika.
        /// </summary>
        public string ModelFilter
        {
            get { return modelFilter; }
            set
            {
                modelFilter = value;
                UpdateData();
            }
        }

        /// <summary>
        /// Filtr numeru rejestracyjnego wprowadzony przez użytkownika.
        /// </summary>
        public string RegistrationNumberFilter
        {
            get { return registrationNumberFilter; }
            set
            {
                registrationNumberFilter = value;
                UpdateData();
            }
        }

        /// <summary>
        /// Uaktualnia dane w tabeli
        /// </summary>
        protected override async void UpdateData()
        {
            var query = GetPageQuery();
            AddFilters(query);
            var pageDto = await service.Page(query);
            if (pageDto != null)
            {
                PageCount = pageDto.PageCount;
                Vehicles = new BindableCollection<VehicleDto>(pageDto.PageCollection);
            }
        }

        protected void AddFilters(PageQuery query)
        {
            if (!string.IsNullOrEmpty(RegistrationNumberFilter))
            {
                query.Filters[nameof(VehicleDto.RegistrationNumber)] = RegistrationNumberFilter;
            }
            if (!string.IsNullOrEmpty(ModelFilter))
            {
                query.Filters[nameof(VehicleDto.Model)] = ModelFilter;
            }
            if (!string.IsNullOrEmpty(BrandFilter))
            {
                query.Filters[nameof(VehicleDto.Brand)] = BrandFilter;
            }
        }
    }
}
