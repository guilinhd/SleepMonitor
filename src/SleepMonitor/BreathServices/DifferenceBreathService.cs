using SleepMonitor.IServices;
using SleepMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Services
{
    /// <summary>
    /// 生成最终呼吸数据
    /// </summary>
    public class DifferenceBreathService : Queue<double>, IBaseService<DifferenceBreathService>
    {
        public BreathService BreathService { set; get; }
         
        private int FilterCount { set; get; }
        public DifferenceBreathService(int count)
        {
            FilterCount = count;
        }

        public bool Filter(DifferenceBreathService service)
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
