using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static WebAPI.Utilities.Enum;

namespace WebAPI.Models
{
    public partial class Complaint
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public ComplaintStatus Status { get; set; }
        public ComplaintType Type { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User user { get; set; }
    }
}
