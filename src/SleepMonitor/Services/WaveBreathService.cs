using SleepMonitor.IServices;
using SleepMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Services
{
    public class WaveBreathService : Queue<SensorRawModel>, IBaseService<DifferenceService>
    {
        public WaveBreathService(int count)
        {
            FilterCount = count;
        }

        public int FilterCount { set; get; }

        public bool Filter(DifferenceService service)
        {
            if (Count < FilterCount)
            {
                return false;
            }

            service.Enqueue(this.ElementAt(1).X - this.ElementAt(0).X);

            return true;
        }
    }
}
