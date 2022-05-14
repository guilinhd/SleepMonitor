using SleepService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepService.HeartBeatWaveServices
{
    public class EndService : BaseService
    {
        public EndService(int count) : base(count)
        {
            
        }

        public override bool Filter()
        {
            Func = () => false;

            if (base.Filter())
            {
                #region 获取有效数据
                Dequeue();
                double[] values = this.Select(c => c.Y).ToArray();
                if (values.Max() - values.Min() < 3)
                {
                    Datas.Add("心跳", values.Average());
                    return true;
                }
                #endregion

                return false;
            }

            return false;
        }
    }
}
