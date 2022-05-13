using SleepService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepService.BreathServices
{
    public class DifferenceService : BaseService
    {
        public DifferenceService(int count) : base(count)
        {
        }

        public override bool Filter()
        {
            Func = () => true;
            return base.Filter();
        }
    }
}
