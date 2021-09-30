using HiddenVilla_Client.Service.IService;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class RoomOrderDetailService : IRoomOrderDetailService
    {
        private readonly HttpClient _httpClient;

        public RoomOrderDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RoomOrderDetailDTO> MarkPaymentSuccessful(RoomOrderDetailDTO roomOrderDetail)
        {
            var content = JsonConvert.SerializeObject(roomOrderDetail);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/roomorder/paymentsucessful", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RoomOrderDetailDTO>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<RoomOrderDetailDTO> SaveRoomOrderDetail(RoomOrderDetailDTO roomOrderDetail)
        {
            roomOrderDetail.UserId = "dummy user";
            var content = JsonConvert.SerializeObject(roomOrderDetail);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/roomorder/create", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RoomOrderDetailDTO>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    }
}