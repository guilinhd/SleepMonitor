using SleepService.Models;
using SleepService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepService.HeartBeatServices
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
                var next = GetNext();

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
                if (leftValue < midValue && RightValue < midValue && waveType)  //强制寻波峰
                {
                    waveStatus = 1;
                }
                else if (leftValue > midValue && RightValue > midValue && !waveType) //强制寻波谷
                {
                    waveStatus = 2;
                }

                if (waveStatus > 0)
                {
                    Wave = new WaveModel()
                    {
                        Type = waveStatus == 1,
                        X = TotalCount,
                        Y = midValue
                    };
                    //Console.WriteLine($"心跳有效波形数:{next.Count + 1}, 心跳波形X的值:{TotalCount}, 当前心跳波形计数器:{WaveCount}");
                    //Console.WriteLine($"type:{waveStatus.ToString()}, X:{TotalCount}, Y:{midValue}");
                    return true;
                }

                return false;
            };

            return base.Filter();
        }
    }
}
