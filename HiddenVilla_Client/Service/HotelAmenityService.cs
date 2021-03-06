using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HiddenVilla_Client.Service.IService;
using Models;
using Newtonsoft.Json;

namespace HiddenVilla_Client.Service
{
    public class HotelAmenityService : IHotelAmenityService
    {
        private readonly HttpClient _httpClient;

        public HotelAmenityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<HotelAmenityDTO>> GetAllHotelAmenity()
        {
            var response = await _httpClient.GetAsync("/api/hotelamenity");
            var content = await response.Content.ReadAsStringAsync();
            var amenities = JsonConvert.DeserializeObject<IEnumerable<HotelAmenityDTO>>(content);

            return amenities;
        }
    }
}