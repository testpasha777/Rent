using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IAvailableToGuestService
    {
        bool AddAvailableToGuest(AvailableToGuestCreateViewModel availableToGuestVM);
        void DeleteAvailableToGuest(int id);
        bool UpdateAvailableToGuest(AvailableToGuestEditViewModel availableToGuestEditVM);
        AvailableToGuestViewModel GetAvailableToGuest(int id);
        AvailableToGuestEditViewModel GetEditAvailableToGuest(int id);
        IEnumerable<AvailableToGuestViewModel> GetAllAvailableToGuest();
    }
}
