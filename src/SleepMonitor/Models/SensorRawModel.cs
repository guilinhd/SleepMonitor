using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Models
{
    public class SensorRawModel
    {
        //类型: true 波峰 false 波谷
        public bool Type { set; get; }

        public int X { set; get; }

        public double Y { set; get; }
    }
}
