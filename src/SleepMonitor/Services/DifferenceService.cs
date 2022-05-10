using SleepMonitor.IServices;
using SleepMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Services
{
    public class DifferenceService : Queue<double>, IFilter
    {
        private Queue<SensorRawModel> _datas = new Queue<SensorRawModel>();
        public Queue<SensorRawModel> Datas { set => _datas = value; }

        public DifferenceService(int count)
        {
            FilterCount = count;
        }

        public int FilterCount { private set; get; }

        public bool Filter()
        {
            if (_datas.Count <= FilterCount)
            {
                return false;
            }

            Enqueue(_datas.ElementAt(1).X - _datas.ElementAt(0).X);
            return true;
        }
    }
}
