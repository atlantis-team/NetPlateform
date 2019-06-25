using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationEngine
{
    class Calculation
    {
        public float Average(float[] values)
        {
            return values.Sum() / 2;
        }

        public float Sum(float[] values)
        {
            return values.Sum();
        }
    }
}
