using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.User.Dto
{
    public class ResetPasswordDto
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
        public string ConfrimPassword { get; set; }
    }
}
