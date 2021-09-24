using Business.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_API.Controllers
{
    [Route("api/[controller]")]
    public class HotelAmenityController : Controller
    {
        private readonly IHotelAmenityRepository _hotelAmenityRepository;

        public HotelAmenityController(IHotelAmenityRepository hotelAmenityRepository)
        {
            _hotelAmenityRepository = hotelAmenityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotelAmenity()
        {
            var allHotelAmenity = await _hotelAmenityRepository.GetAllHotelAmenity();
            return Ok(allHotelAmenity);
        }
    }
}