using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace HiddenVilla_Client.Service.IService
{
    public interface IRoomOrderDetailService
    {
        public Task<RoomOrderDetailDTO> SaveRoomOrderDetail(RoomOrderDetailDTO roomOrderDetail);

        public Task<RoomOrderDetailDTO> MarkPaymentSuccessful(RoomOrderDetailDTO roomOrderDetail);
    }
}