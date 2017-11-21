using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alus.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Alus.WebService.Models
{
    public class AlusContext : DbContext
    {
        public AlusContext(DbContextOptions<AlusContext> options)
            : base(options)
        {
        }
        
        public DbSet<DatabaseFeedback> FeedbackItems { get; set; }
    }
}
