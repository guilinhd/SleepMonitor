using SleepService.Models;
using SleepService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepService.HeartBeatWaveServices
{
    public class WaveService : BaseService
    {
        public WaveService(int count) : base(count)
        {
        }

        public override bool Filter()
        {
            Func = () =>
            {
                List<double> values = this.Select(c => c.Y).ToList();
                values.Sort();
                double[] middleValues = new double[3] { values[2], values[3], values[4] };
                if (middleValues.Max() - middleValues.Min() < 3)
                {
                    Wave = new WaveModel()
                    {
                        Y = middleValues.Average()
                    };
                    
                    return true;
                }
                return false;
            };

            return base.Filter();
        }
    }
}
