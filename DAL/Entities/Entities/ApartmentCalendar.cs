using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Entities
{
    public class ApartmentCalendar
    {
        public int Id { get; set; }

        public DateTime Arrival { get; set; }

        public DateTime Departure { get; set; }

        public int ApartmentId { get; set; }

        public Apartment Apartment { get; set; }
    }
}
