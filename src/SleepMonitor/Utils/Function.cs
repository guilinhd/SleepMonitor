using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Utils
{
    public static class Function
    {
        public static double HexNumberBigToDouble(this string hexNumber)
        {
            return Convert.ToDouble(int.Parse(hexNumber, NumberStyles.HexNumber));
        }

        public static double HexNumberToDouble(this string hexNumber)
        {
            return Convert.ToDouble(short.Parse(hexNumber, NumberStyles.HexNumber));
        }
    }
}
