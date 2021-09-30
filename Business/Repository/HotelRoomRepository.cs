using AutoMapper;
using DataAcess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            HotelRoom hotelRoom = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
            hotelRoom.CreatedDate = DateTime.Now;
            hotelRoom.CreatedBy = "";
            var addedHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelRoom, HotelRoomDTO>(addedHotelRoom.Entity);
        }

        public async Task<int> DeleteHotelRoom(int roomId)
        {
            var roomDetails = await _db.HotelRooms.FindAsync(roomId);
            if (roomDetails != null)
            {
                var allImages = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();

                _db.HotelRoomImages.RemoveRange(allImages);
                _db.HotelRooms.Remove(roomDetails);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRoom(string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                IEnumerable<HotelRoomDTO> hotelRoomDTOs =
                    _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>
                    (_db.HotelRooms.Include(x => x.HotelRoomImages));

                if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDateStr))
                {
                    foreach (HotelRoomDTO hotelRoom in hotelRoomDTOs)
                    {
                        hotelRoom.IsBooked = await IsRoomBooked(hotelRoom.Id, checkInDateStr, checkOutDateStr);
                    }
                }
                return hotelRoomDTOs;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int roomId, string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                    await _db.HotelRooms.Include(x => x.HotelRoomImages).FirstOrDefaultAsync(x => x.Id == roomId));

                if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDateStr))
                {
                    hotelRoom.IsBooked = await IsRoomBooked(hotelRoom.Id, checkInDateStr, checkOutDateStr);
                }

                return hotelRoom;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> IsRoomBooked(int roomId, string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDateStr))
                {
                    DateTime checkInDate = DateTime.ParseExact(checkInDateStr, "MM/dd/yyyy", null);
                    DateTime checkOutDate = DateTime.ParseExact(checkOutDateStr, "MM/dd/yyyy", null);

                    var existingBooking = await _db.RoomOrderDetails.Where(x => x.RoomId == roomId && x.IsPaymentSuccessful &&
                         //check if checkin date that user want does not fall in between any dates for room that is booked
                         ((checkInDate.Date < x.CheckOutDate && checkInDate.Date >= x.CheckInDate)
                         //check if checkout date that user wants does not fall in between any dates for room that is booked
                         || (checkOutDate.Date > x.CheckInDate.Date && checkInDate.Date <= x.CheckInDate.Date))).FirstOrDefaultAsync();

                    if (existingBooking != null)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //if unique return null else retrurn the room obj
        public async Task<HotelRoomDTO> IsRoomUnique(string name, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                        await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));

                    return hotelRoom;
                }
                else
                {
                    HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                                            await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()
                                            && x.Id != id));
                    return hotelRoom;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if (roomId == hotelRoomDTO.Id)
                {
                    //valid
                    HotelRoom roomDetails = await _db.HotelRooms.FindAsync(roomId);
                    HotelRoom room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomDetails);
                    room.UpdatedBy = "";
                    room.UpdatedDate = DateTime.Now;
                    var updatedRoom = _db.HotelRooms.Update(room);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelRoom, HotelRoomDTO>(updatedRoom.Entity);
                }
                else
                {
                    //invalid
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}