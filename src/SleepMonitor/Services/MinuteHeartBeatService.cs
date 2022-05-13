using SleepMonitor.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Services
{
    public class MinuteHeartBeatService : Queue<double>
    {
        private Queue<double> _datas = new Queue<double>();
        public Queue<double> Datas { set => _datas = value; }

        public int FilterCount { private set; get; }

        public MinuteHeartBeatService(int count)
        {
            FilterCount = count;
        }

        public bool Filter()
        {
            if (_datas.Count < FilterCount)
            {
                return false;
            }

            double result = Math.Abs(_datas.Dequeue() - _datas.ElementAt(0)) * 0.00315 / 1.493;
            if (result != 0)
            {
                Enqueue(60 / result);
            }

            return true;
        }
    }
}
