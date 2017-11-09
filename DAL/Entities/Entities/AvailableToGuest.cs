using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Entities
{
    public class AvailableToGuest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Apartment> Apartments { get; set; }
    }
}
