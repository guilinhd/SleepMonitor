using SleepService.Models;
using SleepService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepService.HeartBeatWaveServices
{
    public class FilteringService : BaseService
    {
        public FilteringService(int count) : base(count)
        {
        }

        public override bool Filter()
        {
            WaveModel wave = new WaveModel();
            var value = this.ElementAt(0).Y;
            Func = () =>
            {
                if (value > 35 && value < 120)
                {
                    wave.Y = value;
                    return true;
                }
                return false;
            };
            Wave = wave;

            return base.Filter();
        }
    }
}
