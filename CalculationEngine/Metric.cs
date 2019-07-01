using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationEngine
{
    class Metric
    {
        public long ID { get; set; }
        public long Device_ID { get; set; }
        public string MetricValue { get; set; }
        public DateTime MetricDate { get; set; }

        public Metric(long id, long dID, string value, DateTime date)
        {
            ID = id;
            Device_ID = dID;
            MetricValue = value;
            MetricDate = date;
        }
    }
}
