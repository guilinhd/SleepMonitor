using SleepMonitor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Models
{
    public class SensorModel
    {
        public double Breath { set; get; }

        public List<double> HeartBeats { set; get; }

        public SensorModel GetRaw(string raw)
        {
            //呼吸数据
            string breath = raw.Substring(0, 6);

            //心跳数据
            string data = raw.Replace(breath, "");

            return new SensorModel()
            {
                Breath = GetBreath(breath),
                HeartBeats = GetHeartBeats(data.Substring(0, data.Length - 2))
            };
        }

        private List<double> GetHeartBeats(string data)
        {
            List<double> vs = new List<double>();


            for (int i = 0; i < data.Length; i += 4)
            {
                vs.Add(data.Substring(i, 4).HexNumberToDouble());
            }

            return vs;
        }

        private double GetBreath(string raw)
        {
            return raw.HexNumberBigToDouble();
        }
    }
}
