using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class AvailableToGuestViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
    }

    public class AvailableToGuestEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }

    public class AvailableToGuestCreateViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
