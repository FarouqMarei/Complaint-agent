using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Models
{
    public class ComplaintAgentDBContext : DbContext
    {
        public ComplaintAgentDBContext(DbContextOptions<ComplaintAgentDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
    }
}
