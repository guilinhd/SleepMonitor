using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Services
{
    /// <summary>
    /// 心跳数据服务
    /// </summary>
    public class HeartBeatService
    {
        private int _heartBeatFilter = 7;

        //心跳数据
        private Queue<double> _heartBeats = new Queue<double>();

        //有等间隔的波形数据
        private Queue<double> _heartBeatValids = new Queue<double>();

        //过滤波形数据
        private Queue<double> _heartBeatFilters = new Queue<double>();

        //最终正确的波形数据
        private Queue<double> _heartBeatOks = new Queue<double>();

        /// <summary>
        /// 心跳数据类型
        /// </summary>
        private string _type { set; get; }
        public HeartBeatService(string type)
        {
            _type = type;
        }

        /// <summary>
        /// 有效心跳计数器
        /// </summary>
        public int Count { set; get; }

        /// <summary>
        /// 心跳数据计数器
        /// </summary>
        public int TotalCount { set; get; }
        /// <summary>
        /// 更新心跳
        /// </summary>
        public Action<double> GetHeartBeat;

        /// <summary>
        /// 增加心跳时间点
        /// </summary>
        /// <param name="x"></param>
        public void Add(double x)
        {
            TotalCount++;
            _heartBeats.Enqueue(x);
            HeartBeat();
        }

        /// <summary>
        /// 获取心跳时间间隔
        /// </summary>
        private void HeartBeat()
        {
            if (_heartBeats.Count <= _heartBeatFilter)
            {
                return;
            }
            double result = Math.Abs(_heartBeats.Dequeue() - _heartBeats.ElementAt(0)) * 0.00315 / 1.493;
            if (result != 0)
            {
                _heartBeatValids.Enqueue(60 / result);
                HeartBeatValid();
            }
        }

        /// <summary>
        /// 获取有效的波形数据每秒
        /// </summary>
        private void HeartBeatValid()
        {
            if (_heartBeatValids.Count <= _heartBeatFilter)
            {
                return;
            }

            double result = _heartBeatValids.Dequeue();
            if (result > 35 && result < 120)
            {
                _heartBeatFilters.Enqueue(result);
                HeartBeatFilter();
            }
        }

        /// <summary>
        /// 过滤波形数据 排序后取7个数据中中间的3个值
        /// </summary>
        private void HeartBeatFilter()
        {
            if (_heartBeatFilters.Count <= 7)
            {
                return;
            }

            List<double> vs = _heartBeatFilters.ToList();
            vs.Sort();
            double[] middleValues = new double[3] { vs[2], vs[3], vs[4] };
            if (middleValues.Max() - middleValues.Min() < 3)
            {
                _heartBeatOks.Enqueue(middleValues.Average());
                HeartBeatOk();
            }

            _heartBeatFilters.Dequeue();
        }

        /// <summary>
        /// 显示最终的心跳数据
        /// </summary>
        private void HeartBeatOk()
        {
            if (_heartBeatOks.Count <= 3)
            {
                return;
            }

            _heartBeatOks.Dequeue();

            if (_heartBeatOks.Max() - _heartBeatOks.Min() < 3)
            {
                double average = _heartBeatOks.Average();

                Console.WriteLine($"{_type}心率输出:{average:0.0}");
                Count++;
                Console.WriteLine($"{_type}计数:{Count}");

                GetHeartBeat(average);
            }
        }
    }
}
