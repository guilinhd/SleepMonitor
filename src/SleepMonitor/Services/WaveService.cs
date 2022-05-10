using SleepMonitor.IServices;
using SleepMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Services
{
    public class WaveService : Queue<SensorRawModel>, IFilter
    {
        private Queue<double> _datas = new Queue<double>();
        public Queue<double> Datas { set => _datas = value; }

        private int _waveCount = 0;
        public int WaveCount { set => _waveCount = value; get => _waveCount; }

        private int _totalCount = 0;
        public int TotalCount { set => _totalCount = value; }

        public WaveService(int count)
        {
            FilterCount = count;
        }

        public int FilterCount { private set; get; }

        public bool Filter()
        {
            if (_datas.Count < FilterCount)
            {
                return false;
            }

            //左边3个数据的平均值
            double leftValue = (_datas.ElementAt(0) + _datas.ElementAt(1) + _datas.ElementAt(2)) / 3;
            //后边3个数据的平均值
            double RightValue = (_datas.ElementAt(4) + _datas.ElementAt(5) + _datas.ElementAt(6)) / 3;
            //中间第3个值
            double midValue = _datas.ElementAt(3);

            //最后一个波形的类型
            bool waveType = false;
            if (Count > 0)
            {
                waveType = !this.Last().Type;
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
                Enqueue(new SensorRawModel()
                {
                    Type = waveStatus == 1 ? true : false,
                    X = _totalCount
                });
                
                _waveCount = 0;
            }

            return true;
        }
    }
}
