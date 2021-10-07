using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Business.Repository.IRepository
{
    public interface IHotelRoomRepository
    {
        public Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);

        public Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO);

        public Task<HotelRoomDTO> GetHotelRoom(int roomId, string checkInDateStr = null, string checkOutDateStr = null);

        public Task<int> DeleteHotelRoom(int roomId);

        public Task<IEnumerable<HotelRoomDTO>> GetAllHotelRoom(string checkInDateStr = null, string checkOutDateStr = null);

        public Task<HotelRoomDTO> IsRoomUnique(string name, int id = 0);

        public Task<bool> IsRoomBooked(int roomId, string checkInDateStr, string checkOutDateStr);
    }
}