using KrzysztofSochaAPI.Enums;
using KrzysztofSochaAPI.Services.Address.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.User.Dto
{
    public class RegisterUserDto
    {
        
        public string Name { get; set; }
       
        public string Surname { get; set; }
        public SexType Sex { get; set; }

        public string CreatePassword { get; set; }
            
        public string ConfrimPassword { get; set; }
       
        public string Email { get; set; }
        public string Phone { get; set; }
       
        public DateTime DateOfBirth { get; set; }
        public AddressDto Address { get; set; }
    }
}
