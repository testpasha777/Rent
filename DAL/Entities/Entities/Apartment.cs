using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Entities
{
    public class Apartment
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public int MaxNumberOfGuests { get; set; }

        public int CityId { get; set; }

        public int AvailableToGuestId { get; set; }

        public string UserProfileId { get; set; }

        public virtual City City { get; set; }

        public virtual AvailableToGuest AvailableToGuest { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public virtual ICollection<ApartmentComfort> ApartmentComforts { get; set; }
        
        public virtual ICollection<ApartmentImage> ApartmentImages { get; set; }

        public virtual ICollection<ApartmentCalendar> ApartmentCalendars { get; set; }
    }
}
