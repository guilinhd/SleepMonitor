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
    /// 滤波服务-生成有效波形数据
    /// </summary>
    public class FilteringService : BaseService
    {
        public FilteringService(int count) : base(count)
        {
        }
       
        public override bool Filter()
        {
            var next = GetNext();
            WaveModel wave = new WaveModel();
            Func = () =>
            {
                //左边3个数据的平均值
                double leftValue = (this.ElementAt(0).Y + this.ElementAt(1).Y + this.ElementAt(2).Y) / 3;
                //后边3个数据的平均值
                double RightValue = (this.ElementAt(4).Y + this.ElementAt(5).Y + this.ElementAt(6).Y) / 3;
                //中间第3个值
                double midValue = this.ElementAt(3).Y;

                //最后一个波形的类型
                bool waveType = false;
                if (next.Count > 0)
                {
                    waveType = !next.Last().Type;
                }
                int waveStatus = 0;
                if (leftValue < midValue && RightValue < midValue && waveType && WaveCount > 20)  //强制寻波峰
                {
                    waveStatus = 1;
                }
                else if (leftValue > midValue && RightValue > midValue && !waveType && WaveCount > 20) //强制寻波谷
                {
                    waveStatus = 2;
                }

                if (waveStatus > 0)
                {
                    wave.Type = waveStatus == 1;
                    wave.X = TotalCount;

                    WaveCount = 0;
                    return true;
                }

                return false;
            };
            Wave = wave;
            
            var result = base.Filter();
            if (result)
            {
                Console.WriteLine($"有效波形数:{next.Count}, 波形X的值:{TotalCount}, 当前波形计数器:{WaveCount}");
            }

            return result;
        }
    }
}
