using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IRoomOrderDetailRepository
    {
        public Task<RoomOrderDetailDTO> Create(RoomOrderDetailDTO roomOrderDetail);

        public Task<RoomOrderDetailDTO> MarkPaymentSuccessful(int id);

        public Task<RoomOrderDetailDTO> GetRoomOrderDetail(int roomOrderId);

        public Task<IEnumerable<RoomOrderDetailDTO>> GetAllRoomOrderDetails();

        public Task<bool> UpdateOrderStatus(int roomOrderId, string status);
    }
}