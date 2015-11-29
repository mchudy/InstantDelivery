using InstantDelivery.Model;
using InstantDelivery.Model.Vehicles;
using InstantDelivery.ViewModel.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel.Proxies
{
    public class VehiclesServiceProxy
    {
        private HttpClient client = new HttpClient();

        public VehiclesServiceProxy()
        {
            client.BaseAddress = new Uri("http://localhost:13600/api/Vehicles/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<PagedResult<VehicleDto>> Page(PageQuery query)
        {
            HttpResponseMessage response = await client.GetAsync("Page?" + query.ToQueryString());
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<PagedResult<VehicleDto>>();
        }

        public async Task DeleteVehicle(int vehicleId)
        {
            HttpResponseMessage response = await client.DeleteAsync(vehicleId.ToString());
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateVehicle(VehicleDto employee)
        {
            var response = await client.PutAsJsonAsync("", employee);
            response.EnsureSuccessStatusCode();
        }

        public async Task<VehicleDto> GetById(int id)
        {
            var response = await client.GetAsync(id.ToString());
            return await response.Content.ReadAsAsync<VehicleDto>();
        }

        public async Task<IList<VehicleModelDto>> GetAllModels()
        {
            var response = await client.GetAsync("Models");
            return await response.Content.ReadAsAsync<IList<VehicleModelDto>>();
        }

        public async Task AddVehicle(AddVehicleDto vehicle)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("", vehicle);
            response.EnsureSuccessStatusCode();
        }

        public async Task<int> AddVehicleModel(AddVehicleModelDto model)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("Model", model);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<int>();
        }
    }
}