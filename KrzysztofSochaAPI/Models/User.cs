using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Models
{
    public class User
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int ModifierUserId { get; set; }
        public virtual User Modifier { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public int DeletorUserId { get; set; }
      
        public DateTime? DeletionTime { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address {get; set;}
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

    }
}
