﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAcess.Data;
using Models;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HotelRoomDTO, HotelRoom>();
            CreateMap<HotelRoom, HotelRoomDTO>();

            CreateMap<HotelRoomImage, HotelRoomImageDTO>().ReverseMap();
            CreateMap<HotelAmenity, HotelAmenityDTO>().ReverseMap();
            CreateMap<RoomOrderDetail, RoomOrderDetailDTO>().ReverseMap();
        }
    }
}