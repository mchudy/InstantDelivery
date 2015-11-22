using InstantDelivery.Domain.Entities;
using InstantDelivery.Services;
using InstantDelivery.Services.Paging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Bazowy modeul widoku dla widoków pracowników.
    /// </summary>
    public abstract class VehiclesViewModelBase : PagingViewModel
    {
        private IVehiclesService service;
        private IList<Vehicle> vehicles;

        private string brandFilter = string.Empty;
        private string modelFilter = string.Empty;
        private string registrationNumberFilter = string.Empty;

        private Expression<Func<Vehicle, bool>> filter =>
            e => (string.IsNullOrEmpty(BrandFilter) || e.VehicleModel.Brand.StartsWith(BrandFilter)) &&
                (string.IsNullOrEmpty(ModelFilter) || e.VehicleModel.Model.StartsWith(ModelFilter)) &&
                (string.IsNullOrEmpty(RegistrationNumberFilter) || e.RegistrationNumber.StartsWith(RegistrationNumberFilter));


        protected VehiclesViewModelBase(IVehiclesService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Kolekcja skojarzona z taelą danych.
        /// </summary>
        public IList<Vehicle> Vehicles
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
        public override void UpdateData()
        {
            var query = new PageQuery<Vehicle>
            {
                PageSize = PageSize,
                PageIndex = CurrentPage,
                SortProperty = SortProperty,
                SortDirection = SortDirection,
            };
            query.Filters.Add(filter);
            var pageDto = service.GetPage(query);
            PageCount = pageDto.PageCount;
            Vehicles = pageDto.PageCollection;
        }
    }
}
