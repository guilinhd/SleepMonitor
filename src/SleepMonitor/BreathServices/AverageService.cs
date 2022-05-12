using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SleepMonitor.IServices;
using SleepMonitor.Models;
using System.Linq;

namespace SleepMonitor.Services
{
    /// <summary>
    /// 生成波峰波谷数据
    /// </summary>
    public class AverageService : Queue<double>, IBaseService<WaveBreathService>
    {
        private int _waveCount = 0;
        public int WaveCount { set => _waveCount = value; get => _waveCount; }

        private int _totalCount = 0;
        public int TotalCount { set => _totalCount = value; }

        private int FilterCount { set; get; }
        public AverageService(int count)
        {
            FilterCount = count;
        }

        public bool Filter(WaveBreathService service)
        {
            if (Count >= FilterCount)
            {
                //左边3个数据的平均值
                double leftValue = (this.ElementAt(0) + this.ElementAt(1) + this.ElementAt(2)) / 3;
                //后边3个数据的平均值
                double RightValue = (this.ElementAt(4) + this.ElementAt(5) + this.ElementAt(6)) / 3;
                //中间第3个值
                double midValue = this.ElementAt(3);

                //最后一个波形的类型
                bool waveType = false;
                if (service.Count > 0)
                {
                    waveType = !service.Last().Type;
                }
                int waveStatus = 0;
                if (leftValue < midValue && RightValue < midValue && waveType && _waveCount > 20)  //强制寻波峰
                {
                    waveStatus = 1;
                }
                else if (leftValue >= midValue && RightValue >= midValue && !waveType && _waveCount > 20) //强制寻波谷
                {
                    waveStatus = 2;
                }

                if (waveStatus > 0)
                {
                    service.Enqueue(new SensorRawModel()
                    {
                        Type = waveStatus == 1,
                        X = _totalCount
                    });

                    _waveCount = 0;
                }

                //移除第一个数据
                Dequeue();

                return true;
            }
            
            return false;
            
        }
    }
}
