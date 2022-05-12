using SleepMonitor.IServices;
using SleepMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Services
{
    public class DifferenceService : Queue<double>, IBaseService<DifferenceService>
    {
        public BreathService BreathService { set; get; }
         
        private int FilterCount { set; get; }
        public DifferenceService(int count)
        {
            FilterCount = count;
        }

        public bool Filter(DifferenceService service)
        {
            if (Count > FilterCount)
            {
                Dequeue();
                double average = this.Average();
                if (average != 0)
                {
                    BreathService.GetBreath(600 / average);
                    return true;
                }
            }

            return false;
        }
    }


}
