using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Entities
{
    public class ApartmentImage
    {
        public int Id { get; set; }

        public int ApartmentId { get; set; }

        public string PathPhoto { get; set; }

        public string LinkPhoto { get; set; }

        public virtual Apartment Apartment { get; set; }
    }
}
