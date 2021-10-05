using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAcess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository.IRepository
{
    public class HotelAmenityRepository : IHotelAmenityRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public HotelAmenityRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<HotelAmenityDTO> CreateHotelAmenity(HotelAmenityDTO hotelAmenityDTO)
        {
            HotelAmenity hotelAmenity = _mapper.Map<HotelAmenityDTO, HotelAmenity>(hotelAmenityDTO);
            hotelAmenity.CreatedDate = DateTime.Now;
            hotelAmenity.CreatedBy = string.Empty;
            var addedHotelAmenity = await _db.HotelAmenities.AddAsync(hotelAmenity);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelAmenity, HotelAmenityDTO>(addedHotelAmenity.Entity);
        }

        public async Task<int> DeleteHotelAmenity(int amenityId)
        {
            var hotelAmenity = await _db.HotelAmenities.FindAsync(amenityId);
            if (hotelAmenity != null)
            {
                _db.HotelAmenities.Remove(hotelAmenity);
                return await _db.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<IEnumerable<HotelAmenityDTO>> GetAllHotelAmenity()
        {
            try
            {
                IEnumerable<HotelAmenityDTO> hotelAmenityDTOs =
                    _mapper.Map<IEnumerable<HotelAmenity>, IEnumerable<HotelAmenityDTO>>(
                        await _db.HotelAmenities.ToListAsync());
                return hotelAmenityDTOs;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<HotelAmenityDTO> GetHotelAmenity(int amenityId)
        {
            try
            {
                HotelAmenityDTO hotelAmenity = _mapper.Map<HotelAmenity, HotelAmenityDTO>(
                    await _db.HotelAmenities.FirstOrDefaultAsync(x => x.Id == amenityId));

                return hotelAmenity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<HotelAmenityDTO> IsAmenityUnique(string name, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    HotelAmenityDTO hotelAmenityDTO = _mapper.Map<HotelAmenity, HotelAmenityDTO>(
                        await _db.HotelAmenities.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));

                    return hotelAmenityDTO;
                }
                else
                {
                    HotelAmenityDTO hotelAmenityDTO = _mapper.Map<HotelAmenity, HotelAmenityDTO>(
                        await _db.HotelAmenities.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()
                        && x.Id != id));

                    return hotelAmenityDTO;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<HotelAmenityDTO> UpdateHotelAmenity(int amenityId, HotelAmenityDTO hotelAmenityDTO)
        {
            try
            {
                if (amenityId == hotelAmenityDTO.Id)
                {
                    // valid
                    HotelAmenity hotelAmenity = await _db.HotelAmenities.FindAsync(amenityId);
                    HotelAmenity amenity = _mapper.Map<HotelAmenityDTO, HotelAmenity>(hotelAmenityDTO, hotelAmenity);
                    amenity.UpdatedBy = string.Empty;
                    amenity.UpdatedDate = DateTime.Now;
                    var updatedAmenity = _db.HotelAmenities.Update(amenity);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelAmenity, HotelAmenityDTO>(updatedAmenity.Entity);
                }
                else
                {
                    // invalid
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