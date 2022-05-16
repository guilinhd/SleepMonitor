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
            Func = () =>
            {
                var value = this.ElementAt(0).Y;
                if (value > 35 && value < 120)
                {
                    Wave = new WaveModel()
                    {
                        Y = value
                    };
                    return true;
                }
                return false;
            };

            return base.Filter();
        }
    }
}
