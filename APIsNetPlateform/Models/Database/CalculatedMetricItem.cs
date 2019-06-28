using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIsNetPlateform.Models.Database
{
    [Table("calculatedmetric")]
    public class CalculatedMetricItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int id_device { get; set; }
        public string date { get; set; }
        public string period { get; set; }
        public float sum { get; set; }
        public float average { get; set; }
        public float percent { get; set; }
    }
}
