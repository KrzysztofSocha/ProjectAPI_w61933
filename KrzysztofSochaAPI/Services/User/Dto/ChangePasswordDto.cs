using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.User.Dto
{
    public class ChangePasswordDto
    {
        public string ActualPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfrimPassword { get; set; }
    }
}
