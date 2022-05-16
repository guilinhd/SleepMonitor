using SleepService.Models;
using SleepService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepService.BreathServices
{
    /// <summary>
    /// 波形服务- 生成峰谷差数据
    /// </summary>
    public class WaveService : BaseService
    {
        public WaveService(int count) : base(count)
        {
        }

        public override bool Filter()
        {
            Func = () =>
            {
                Wave = new WaveModel() { X = this.ElementAt(1).X - this.ElementAt(0).X };
                return true;
            };
            
            return base.Filter();
        }
    }
}
