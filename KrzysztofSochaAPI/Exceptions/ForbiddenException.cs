﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Exceptions
{
    public class ForbiddenException:Exception
    {
        public ForbiddenException(string message):base(message)
        {

        }
    }
}
