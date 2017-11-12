﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Entities
{
    public class ApartmentImage
    {
        public int Id { get; set; }

        public int ApartmentId { get; set; }

        [Required]
        public string PathPhoto { get; set; }

        [Required]
        public string LinkPhoto { get; set; }

        public virtual Apartment Apartment { get; set; }
    }
}
