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

        public async Task<VehicleDto> GetById(int id)
        {
            return await Get<VehicleDto>(id.ToString());
        }

        public async Task<IList<VehicleModelDto>> GetAllModels()
        {
            return await Get<IList<VehicleModelDto>>("Models");
        }

        public async Task<PagedResult<VehicleDto>> Page(PageQuery query)
        {
            string queryString = "Page?" + query.ToQueryString();
            return await Get<PagedResult<VehicleDto>>(queryString);
        }

        public async Task<PagedResult<VehicleDto>> AvailableVehiclesPage(PageQuery query)
        {
            string queryString = "Available/Page?" + query.ToQueryString();
            return await Get<PagedResult<VehicleDto>>(queryString);
        }

        public async Task DeleteVehicle(int vehicleId)
        {
            await Delete(vehicleId);
        }

        public async Task UpdateVehicle(VehicleDto vehicle)
        {
            await Put(vehicle);
        }

        public async Task AddVehicle(AddVehicleDto vehicle)
        {
            await PostAsJson("", vehicle);
        }

        public async Task<int> AddVehicleModel(AddVehicleModelDto model)
        {
            return await PostAsJson<AddVehicleModelDto, int>("Models", model);
        }
    }
}