using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ITypeOfHousingService
    {
        bool Create(TypeOfHousingCreateViewModel typeOfHousingVM);
        void Delete(int id);
        bool Update(TypeOfHousingEditViewModel typeOfHousingVM);
        TypeOfHousingViewModel GetById(int id);
        TypeOfHousingEditViewModel GetEditById(int id);
        IEnumerable<TypeOfHousingViewModel> GetAll();
    }
}
