﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HotelAmenityDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter amenity name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter amenity description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Plese enter amenity timing")]
        public string Timming { get; set; }

        [Required(ErrorMessage = "Please enter amenity icon from font awesome")]
        public string Icon { get; set; }
    }
}
