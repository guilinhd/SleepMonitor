using SleepService.Models;
using SleepService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepService.HeartBeatWaveServices
{
    public class StartService : BaseService
    {
        public StartService(int count) : base(count)
        {
        }

        public override bool Filter()
        {
            Func = () =>
            {
                double value = Math.Abs(this.ElementAt(0).Y - this.ElementAt(1).Y) * 0.00315 / 1.493;
                //Console.WriteLine($"count:{TotalCount}, x:{this.ElementAt(0).Y}");
                if (value != 0)
                {
                    Wave = new WaveModel()
                    {
                        Y = 60 / value
                    };
                    return true;
                }
                
                return false;
            };

            return base.Filter();
        }
    }
}
