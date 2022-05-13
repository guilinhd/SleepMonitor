using SleepMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.HeartBeatServices
{
    /// <summary>
    /// 心跳监测服务
    /// </summary>
    public class HeartBeatMonitorService
    {
        //心率计数器
        private int _heartBeatCount = 0;

        //原始心率数据
        private Queue<double> _heartBeats = new Queue<double>();
        //心率配置
        private int _heartBeat = 10;

        //过滤后的心率数据
        private Queue<double> _heartBeatFilers = new Queue<double>();

        //过滤后的心率数配置
        private int _heartBeatFilter = 7;

        //有效的波形
        private Queue<SensorRawModel> _raws = new Queue<SensorRawModel>();

        //波峰
        private HeartBeatService _peakService = new HeartBeatService("正向波");
        public int PeakCount { get; private set; }  //正向波计数器

        //波谷
        private HeartBeatService _troughService = new HeartBeatService("反向波");
        public int TroughCount { get; private set; } //反向波计数器

        public HeartBeatMonitorService()
        {
            _peakService.GetHeartBeat += (result) =>
            {
                //if (_peakService.Count > _troughService.Count)
                //{
                //    PeakCount = _peakService.Count;
                //    GetHeartBeat("正向波心率", result);
                //}

                PeakCount = _peakService.Count;
                GetHeartBeat("正向波心率", result);
            };

            _troughService.GetHeartBeat += (result) =>
            {
                //if (_peakService.Count < _troughService.Count)
                //{
                //    TroughCount = _troughService.Count;
                //    GetHeartBeat("反向波心率", result);
                //}

                TroughCount = _troughService.Count;
                GetHeartBeat("反向波心率", result);
            };
        }

        /// <summary>
        /// 更新心跳信息
        /// </summary>
        public Action<string, double> GetHeartBeat;

        /// <summary>
        /// 添加传感器数据
        /// </summary>
        /// <param name="model">传感器数据</param>
        public void Add(SensorModel model)
        {
            foreach (var item in model.HeartBeats)
            {
                if (_heartBeatCount > 2147483647)
                {
                    _heartBeatCount = 0;
                }

                _heartBeats.Enqueue(item);
                HeartBeatFilter();
                _heartBeatCount++;
            }

        }

        /// <summary>
        /// 原始心率数据过滤
        /// </summary>
        private void HeartBeatFilter()
        {
            if (_heartBeats.Count < _heartBeat)
            {
                return;
            }

            _heartBeatFilers.Enqueue(_heartBeats.Average());
            HeartBeatWave();

            _heartBeats.Dequeue();
        }

        /// <summary>
        /// 过滤后心率数据寻找波峰或者波谷
        /// </summary>
        private void HeartBeatWave()
        {

            if (_heartBeatFilers.Count < _heartBeatFilter)
            {
                return;
            }

            //左边3个数据的平均值
            double leftValue = (_heartBeatFilers.ElementAt(0) + _heartBeatFilers.ElementAt(1) + _heartBeatFilers.ElementAt(2)) / 3;
            //后边3个数据的平均值
            double RightValue = (_heartBeatFilers.ElementAt(4) + _heartBeatFilers.ElementAt(5) + _heartBeatFilers.ElementAt(6)) / 3;
            //中间第3个值
            double midValue = _heartBeatFilers.ElementAt(3);

            //最后一个波形的类型
            bool waveType = false;
            if (_raws.Count > 0)
            {
                waveType = !_raws.Last().Type;
            }
            int waveStatus = 0;
            if (leftValue < midValue && RightValue < midValue && waveType)  //强制寻波峰
            {
                waveStatus = 1;
            }
            else if (leftValue >= midValue && RightValue >= midValue && !waveType) //强制寻波谷
            {
                waveStatus = 2;
            }

            if (waveStatus > 0)
            {
                _raws.Enqueue(new SensorRawModel()
                {
                    Type = waveStatus == 1 ? true : false,
                    X = _heartBeatCount,
                    Y = midValue
                });

                HeartBeatWaveValid();
            }

            _heartBeatFilers.Dequeue();
        }

        /// <summary>
        /// 获取有效的波形
        /// </summary>
        private void HeartBeatWaveValid()
        {
            if (_raws.Count <= 27)
            {
                return;
            }

            //峰谷差
            double[] dots = _raws.Select(f => f.Y).ToArray();
            double average = 0.5 * (dots.Max() - dots.Min());
                                                  //波峰                //波谷     
            SensorRawModel raw = _raws.Dequeue(); SensorRawModel peak;  SensorRawModel trough;  
            if (!raw.Type)      //当前波形是波谷
            {
                trough = raw;  //直接设置波谷
            }
            else
            {
                trough = _raws.Dequeue();   //设置下一个波形是波谷
            }
            peak = _raws.Dequeue(); //设置波峰

            #region 仰睡状态
            double ssfgc = peak.Y - _raws.ElementAt(0).Y;//仰睡状态的实时峰谷差
            double ssfgcmf = _raws.ElementAt(1).Y - _raws.ElementAt(2).Y;//仰睡数列内头部两个元素的峰谷差
            double ssfgcmi = _raws.ElementAt(3).Y - _raws.ElementAt(4).Y;

            //当前波峰后面3个波峰是否是连续递减
            if (ssfgc > average && ssfgc > ssfgcmf && ssfgc > ssfgcmi)
            {
                _peakService.Add(peak.X);
            }
            #endregion


            #region 侧睡状态
            double csfgc = Math.Abs(peak.Y - trough.Y);//侧睡状态的实时峰谷差
            double csfgcmf = Math.Abs(_raws.ElementAt(1).Y - _raws.ElementAt(0).Y);//侧睡数列内头部两个元素的峰谷差
            double csfgcmi = Math.Abs(_raws.ElementAt(3).Y - _raws.ElementAt(2).Y);

            if (csfgc > average && csfgc > csfgcmf && csfgc > csfgcmi)
            {
                _troughService.Add(trough.X);
            }
            #endregion

        }

       
    }
}
