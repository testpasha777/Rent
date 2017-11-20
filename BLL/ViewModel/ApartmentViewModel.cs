using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.ViewModel
{
    public class ApartmentViewModel
    {

    }

    public class ApartmentCreateViewModel
    {
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [RegularExpression(@"^[a-zA-Z'a-zA-Z\,\s]+$", ErrorMessage = "Only English letter")]
        [Required]
        [Display(Name = "Location")]
        public string CityAndCountry { get; set; }

        [Required]
        [Display(Name = "Street Number")]
        public string StreetNumber { get; set; }

        [Display(Name = "Flet number not require")]
        public string FletNumber { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Max number of guests")]
        [Range(1, 16)]
        public int MaxNumberOfGuests { get; set; }

        [Required]
        [Display(Name = "Available to guest")]
        public int AvailableToGuestId { get; set; }

        public string UserProfileId { get; set; }

        [Required]
        [Display(Name = "Type of housing")]
        public int TypeOfHousingId { get; set; }

        [Required]
        [Display(Name = "Add photo")]
        public string [] images { get; set; }

        [Display(Name = "Select Apartment comfort")]
        public List<string> SelectedApartmentComfortsId { get; set; }

        public List<AvailableToGuestViewModel> AvailableToGuest { get; set; }
        public List<TypeOfHousingViewModel> TypeOfHousing { get; set; }
        public List<ApartmentComfortViewModel> ApartmentComforts { get; set; }
    }

    public class ApartmentEditViewModel
    {

    }

}
