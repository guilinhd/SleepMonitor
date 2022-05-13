using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Services
{
    public class HeartBeatChildService
    {
        /// <summary>
        /// 有效心跳计数器
        /// </summary>
        public int Count { set; get; }

        private Queue<double> _datas = new Queue<double>();
        public Queue<double> Datas { set => _datas = value; }

        private int FilterCount = 0;

        /// <summary>
        /// 心跳数据类型 
        /// </summary>
        private string _type { set; get; }
        public HeartBeatChildService(string type, int count)
        {
            _type = type;
            FilterCount = count;
        }
    }
}
