using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class SubmitComplaint
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
