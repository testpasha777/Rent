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
        bool Add(AvailableToGuestCreateViewModel availableToGuestVM);
        void Delete(int id);
        bool Update(AvailableToGuestEditViewModel availableToGuestEditVM);
        AvailableToGuestViewModel GetById(int id);
        AvailableToGuestEditViewModel GetEditById(int id);
        IEnumerable<AvailableToGuestViewModel> GetAll();
    }
}
