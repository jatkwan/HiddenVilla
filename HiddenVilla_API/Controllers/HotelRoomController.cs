using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Business.Repository.IRepository;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace HiddenVilla_API.Controllers
{
    [Route("api/[controller]")]
    public class HotelRoomController : Controller
    {
        private readonly IHotelRoomRepository _hotelRoomRepository;

        public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
        {
            _hotelRoomRepository = hotelRoomRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotelRooms(string checkInDate = null, string checkOutDate = null)
        {
            if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel
                {
                    Title = "Bad Request",
                    ErrorMessage = "All parameters need to be supplied",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel
                {
                    Title = "Bad Request",
                    ErrorMessage = "Invalid check in date. Valid format should be MM/dd/yyyy",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckOutDate))
            {
                return BadRequest(new ErrorModel
                {
                    Title = "Bad Request",
                    ErrorMessage = "Invalid check out date. Valid format should be MM/dd/yyyy",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var allHotelRooms = await _hotelRoomRepository.GetAllHotelRoom(checkInDate, checkOutDate);
            return Ok(allHotelRooms);
        }

        [HttpGet("roomId")]
        public async Task<IActionResult> GetHotelRoom(int? roomId, string checkInDate = null, string checkOutDate = null)
        {
            if (roomId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "Bad Request",
                    ErrorMessage = "Invalid Room Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel
                {
                    Title = "Bad Request",
                    ErrorMessage = "All parameters need to be supplied",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel
                {
                    Title = "Bad Request",
                    ErrorMessage = "Invalid check in date. Valid format should be MM/dd/yyyy",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckOutDate))
            {
                return BadRequest(new ErrorModel
                {
                    Title = "Bad Request",
                    ErrorMessage = "Invalid check out date. Valid format should be MM/dd/yyyy",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var hotelRoom = await _hotelRoomRepository.GetHotelRoom(roomId.Value, checkInDate, checkOutDate);

            if (hotelRoom == null)
            {
                return NotFound(new ErrorModel()
                {
                    Title = "Not Found",
                    ErrorMessage = "Hotel Room Not Found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(hotelRoom);
        }
    }
}