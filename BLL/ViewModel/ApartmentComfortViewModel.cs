using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class ApartmentComfortViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
    }

    public class ApartmentComfortCreateViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
    }

    public class ApartmentComfortEditViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
