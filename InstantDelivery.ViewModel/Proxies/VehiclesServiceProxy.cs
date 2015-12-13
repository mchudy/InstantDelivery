using InstantDelivery.Model;
using InstantDelivery.Model.Paging;
using InstantDelivery.Model.Vehicles;
using InstantDelivery.ViewModel.Dialogs;
using InstantDelivery.ViewModel.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel.Proxies
{
    public class VehiclesServiceProxy : ServiceProxyBase
    {
        public VehiclesServiceProxy(IDialogManager dialogManager)
            : base("Vehicles", dialogManager)
        {
        }

        /// <summary>
        /// Zwraca VehicleDto dla konkretnego id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VehicleDto> GetById(int id)
        {
            return await Get<VehicleDto>(id.ToString());
        }

        /// <summary>
        /// Zwraca wszystkie modele pojazdów.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<VehicleModelDto>> GetAllModels()
        {
            return await Get<IList<VehicleModelDto>>("Models");
        }

        /// <summary>
        /// Zwraca stronę danych pojazdów.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PagedResult<VehicleDto>> Page(PageQuery query)
        {
            string queryString = "Page?" + query.ToQueryString();
            return await Get<PagedResult<VehicleDto>>(queryString);
        }

        /// <summary>
        /// Zwraca stronę danych dostępnych pojazdów.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PagedResult<VehicleDto>> AvailableVehiclesPage(PageQuery query)
        {
            string queryString = "Available/Page?" + query.ToQueryString();
            return await Get<PagedResult<VehicleDto>>(queryString);
        }

        /// <summary>
        /// Usuwa pojazd o zadanym ID z bazy danych.
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        public async Task DeleteVehicle(int vehicleId)
        {
            await Delete(vehicleId);
        }

        /// <summary>
        /// Aktualizuje pojazd o podanym ID.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public async Task UpdateVehicle(VehicleDto vehicle)
        {
            await Put(vehicle);
        }

        /// <summary>
        /// Dodaje pojazd do bazy danych.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public async Task AddVehicle(AddVehicleDto vehicle)
        {
            await PostAsJson("", vehicle);
        }

        /// <summary>
        /// Dodaje model pojazdu do bazy danych.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> AddVehicleModel(AddVehicleModelDto model)
        {
            return await PostAsJson<AddVehicleModelDto, int>("Models", model);
        }
    }
}