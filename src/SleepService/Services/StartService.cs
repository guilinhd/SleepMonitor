using SleepService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepService.Services
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
                Wave = new WaveModel() { Y = this.Select(c => c.Y).ToArray().Average() };
                return true;
            };

            return base.Filter();
        }
    }
}
