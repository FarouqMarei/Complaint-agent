﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class LoginResonse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Token { get; set; }
    }
}
