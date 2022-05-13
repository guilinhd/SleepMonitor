using SleepService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepService.Services
{
    public class InitService : BaseService
    {
        public InitService(int count) : base(count)
        {
        }

        public override bool Filter()
        {
            if (base.Filter())
            {
                var next = GetNext();
                if (next != null)
                {
                    //取平均值
                    var average = this.Select(c => c.Y).ToArray().Average();
                    next.Enqueue(new WaveModel() { Y = average });
                    
                    //移除第一个数据
                    Dequeue();
                }

                return true;
            }

            return false;
        }
    }
}
