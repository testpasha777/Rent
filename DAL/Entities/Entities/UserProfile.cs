using DAL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Entities
{
    public class UserProfile
    {
        [Key]
        [ForeignKey("AppUser")]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SurName { get; set; }

        public string AvatarPath { get; set; }

        public string AvatarLink { get; set; }

        public virtual AppUser AppUser { get; set; }

    }
}
