using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class RegisterationDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }
    }
}
