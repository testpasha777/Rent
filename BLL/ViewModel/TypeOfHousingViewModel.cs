using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class TypeOfHousingViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class TypeOfHousingCreateViewModel
    {
        [Required]
        public string Name { get; set; }
    }

    public class TypeOfHousingEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

}
