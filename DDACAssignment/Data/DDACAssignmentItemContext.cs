using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DDACAssignment.Models;

namespace DDACAssignment.Data
{
    public class DDACAssignmentItemContext : DbContext
    {
        public DDACAssignmentItemContext (DbContextOptions<DDACAssignmentItemContext> options)
            : base(options)
        {
        }

        public DbSet<DDACAssignment.Models.Item> Item { get; set; }
    }
}
