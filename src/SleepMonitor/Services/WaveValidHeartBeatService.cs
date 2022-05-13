using SleepMonitor.IServices;
using SleepMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Services
{
    public class WaveValidHeartBeatService 
    {
        private Queue<SensorRawModel> _datas = new Queue<SensorRawModel>();
        public Queue<SensorRawModel> Datas { set => _datas = value; }

        private Queue<double> _forwards = new Queue<double>();   //正向波
        public Queue<double> Forwards { get => _forwards; }

        private Queue<double> _reverses = new Queue<double>();   //反向波
        public Queue<double> Reverses { get => _reverses; }

        public int FilterCount { private set; get; }

        public WaveValidHeartBeatService(int count)
        {
            FilterCount = count;
        }

        public bool Filter()
        {
            if (_datas.Count <= FilterCount)
            {
                return false;
            }
            
            //峰谷差
            double[] dots = _datas.Select(f => f.Y).ToArray();
            double average = 0.5 * (dots.Max() - dots.Min());
            //波峰                //波谷     
            SensorRawModel raw = _datas.Dequeue(); SensorRawModel peak; SensorRawModel trough;
            if (!raw.Type)      //当前波形是波谷
            {
                trough = raw;  //直接设置波谷
            }
            else
            {
                trough = _datas.Dequeue();   //设置下一个波形是波谷
            }
            peak = _datas.Dequeue(); //设置波峰


            #region 仰睡状态
            double ssfgc = peak.Y - _datas.ElementAt(0).Y;//仰睡状态的实时峰谷差
            double ssfgcmf = _datas.ElementAt(1).Y - _datas.ElementAt(2).Y;//仰睡数列内头部两个元素的峰谷差
            double ssfgcmi = _datas.ElementAt(3).Y - _datas.ElementAt(4).Y;

            //当前波峰后面3个波峰是否是连续递减
            if (ssfgc > average && ssfgc > ssfgcmf && ssfgc > ssfgcmi)
            {
                _forwards.Enqueue(peak.X);
            }
            #endregion

            #region 侧睡状态
            double csfgc = Math.Abs(peak.Y - trough.Y);//侧睡状态的实时峰谷差
            double csfgcmf = Math.Abs(_datas.ElementAt(1).Y - _datas.ElementAt(0).Y);//侧睡数列内头部两个元素的峰谷差
            double csfgcmi = Math.Abs(_datas.ElementAt(3).Y - _datas.ElementAt(2).Y);

            if (csfgc > average && csfgc > csfgcmf && csfgc > csfgcmi)
            {
                _reverses.Enqueue(trough.X);
            }
            #endregion

            return true;
        }
    }
}
