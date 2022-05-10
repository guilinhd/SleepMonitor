using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepMonitor.IServices;

namespace SleepMonitor.Services
{
    public class AverageService : Queue<double>, IFilter
    {
        //待处理的原始数据
        private Queue<double> _datas = new Queue<double>();

        public AverageService(int count)
        {
            FilterCount = count;
        }

        public int FilterCount { private set; get; }

        public void Add(double data)
        {
            _datas.Enqueue(data);
        }

        public bool Filter()
        {
            if (_datas.Count < FilterCount)
            {
                return false;
            }

            //滤波操作:取平均值
            Enqueue(_datas.Average());

            //移除第一个数据
            _datas.Dequeue();
            return true;
        }
    }
}
