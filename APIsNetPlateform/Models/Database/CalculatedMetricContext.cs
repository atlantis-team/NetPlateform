using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIsNetPlateform.Models.Database
{
    public class CalculatedMetricContext : DbContext
    {
        public CalculatedMetricContext(DbContextOptions<CalculatedMetricContext> options)
            : base(options)
        {
        }

        public DbSet<CalculatedMetricItem> MetricItems { get; set; }
    }
}
