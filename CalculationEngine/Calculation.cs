using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationEngine
{
    class Calculation
    {
        public float Average(List<float> values)
        {
            return values.Sum() / values.Count;
        }

        public float Sum(List<float> values)
        {
            return values.Sum();
        }
    }
}
