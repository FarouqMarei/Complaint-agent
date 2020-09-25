using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public partial class Complaint
    {
        [NotMapped]
        public string StatusStr { get; set; }
        [NotMapped]
        public string TypeStr { get; set; }
        [NotMapped]
        public string username { get; set; }
    }
}
