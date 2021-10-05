using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Repository.IRepository;
using Common;
using DataAcess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository
{
    public class RoomOrderDetailRepository : IRoomOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public RoomOrderDetailRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<RoomOrderDetailDTO> Create(RoomOrderDetailDTO roomOrderDetail)
        {
            try
            {
                roomOrderDetail.CheckInDate = roomOrderDetail.CheckInDate.Date;
                roomOrderDetail.CheckOutDate = roomOrderDetail.CheckOutDate.Date;
                var roomOrder = _mapper.Map<RoomOrderDetailDTO, RoomOrderDetail>(roomOrderDetail);
                roomOrder.Status = SD.Status_Pending;
                var result = await _db.RoomOrderDetails.AddAsync(roomOrder);
                await _db.SaveChangesAsync();

                return _mapper.Map<RoomOrderDetail, RoomOrderDetailDTO>(result.Entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<RoomOrderDetailDTO>> GetAllRoomOrderDetails()
        {
            try
            {
                IEnumerable<RoomOrderDetailDTO> roomOrders = _mapper.Map<IEnumerable<RoomOrderDetail>,
                                                                IEnumerable<RoomOrderDetailDTO>>(
                                                                _db.RoomOrderDetails.Include(u => u.HotelRoom));
                return roomOrders;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RoomOrderDetailDTO> GetRoomOrderDetail(int roomOrderId)
        {
            try
            {
                RoomOrderDetail roomOrder = await _db.RoomOrderDetails.Include(u => u.HotelRoom)
                    .ThenInclude(x => x.HotelRoomImages)
                    .FirstOrDefaultAsync(u => u.Id == roomOrderId);
                RoomOrderDetailDTO roomOrderDetailDTO = _mapper.Map<RoomOrderDetail, RoomOrderDetailDTO>(roomOrder);
                roomOrderDetailDTO.HotelRoomDTO.TotalDays = roomOrderDetailDTO.CheckOutDate
                    .Subtract(roomOrderDetailDTO.CheckInDate).Days;

                return roomOrderDetailDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RoomOrderDetailDTO> MarkPaymentSuccessful(int id)
        {
            var data = await _db.RoomOrderDetails.FindAsync(id);
            if (data == null)
            {
                return null;
            }

            if (!data.IsPaymentSuccessful)
            {
                data.IsPaymentSuccessful = true;
                data.Status = SD.Status_Booked;
                var markPaymentSucessful = _db.RoomOrderDetails.Update(data);
                await _db.SaveChangesAsync();
                return _mapper.Map<RoomOrderDetail, RoomOrderDetailDTO>(markPaymentSucessful.Entity);
            }

            return new RoomOrderDetailDTO();
        }

        public Task<bool> UpdateOrderStatus(int roomOrderId, string status)
        {
            throw new NotImplementedException();
        }
    }
}