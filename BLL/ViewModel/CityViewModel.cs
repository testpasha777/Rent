using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class CityViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }
    }

    public class CityEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public List<SelectItemViewModel> Countries { get; set; }
    }

    public class CityCreateViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public List<SelectItemViewModel> Countries { get; set; }
    }
}
