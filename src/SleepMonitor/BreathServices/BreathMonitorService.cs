using SleepMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Services
{
    /// <summary>
    /// 呼吸监测服务
    /// </summary>
    public class BreathMonitorService
    {
        //呼吸计数器
        private int _breathCount = 0;

        //过滤后的呼吸配置
        private int _breathFilter = 50;

        //原始呼吸数据
        private Queue<double> _breaths = new Queue<double>();

        //过滤后的呼吸数据
        private Queue<double> _breathFilers = new Queue<double>();

        //有效的波形
        private Queue<SensorRawModel> _raws = new Queue<SensorRawModel>();

        //有效波形间隔计数器
        private int _breathWaveCount = 0;

        //最终正确的呼吸数据
        private Queue<double> _breathOks = new Queue<double>();

        /// <summary>
        /// 更新呼吸信息
        /// </summary>
        public Action<double> GetBreath;

        /// <summary>
        /// 添加传感器数据
        /// </summary>
        /// <param name="model">传感器数据</param>
        public void Add(SensorModel model)
        {
            if (_breathCount > 2147483647)
            {
                _breathCount = 0;
            }

            _breathCount++;
            _breathWaveCount++;

            _breaths.Enqueue(model.Breath);
            BreathFilter();
        }

        /// <summary>
        /// 原始呼吸数据过滤
        /// </summary>
        private void BreathFilter()
        {
            if (_breaths.Count < _breathFilter)
            {
                return;
            }

            _breathFilers.Enqueue(_breaths.Average());
            BreathWave();

            _breaths.Dequeue();
        }

        /// <summary>
        /// 过滤后心率数据寻找波峰或者波谷
        /// </summary>
        private void BreathWave()
        {
            if (_breathFilers.Count < 7)
            {
                return;
            }

            

            //左边3个数据的平均值
            double leftValue = (_breathFilers.ElementAt(0) + _breathFilers.ElementAt(1) + _breathFilers.ElementAt(2)) / 3;
            //后边3个数据的平均值
            double RightValue = (_breathFilers.ElementAt(4) + _breathFilers.ElementAt(5) + _breathFilers.ElementAt(6)) / 3;
            //中间第3个值
            double midValue = _breathFilers.ElementAt(3);

            //最后一个波形的类型
            bool waveType = false;
            if (_raws.Count > 0)
            {
                waveType = !_raws.Last().Type;
            }
            int waveStatus = 0;
            if (leftValue < midValue && RightValue < midValue && waveType && _breathWaveCount > 20)  //强制寻波峰
            {
                waveStatus = 1;
            }
            else if (leftValue >= midValue && RightValue >= midValue && !waveType && _breathWaveCount > 20) //强制寻波谷
            {
                waveStatus = 2;
            }

            if (waveStatus > 0)
            {
                _raws.Enqueue(new SensorRawModel()
                {
                    Type = waveStatus == 1 ? true : false,
                    X = _breathCount
                });

                Console.WriteLine($"Ph.Hx :{_breathCount}");
                _breathWaveCount = 0;
                BreathWaveValid();
            }

            _breathFilers.Dequeue();
        }

        /// <summary>
        /// 获取有效的波形
        /// </summary>
        private void BreathWaveValid()
        {

            if (_raws.Count <= 7)
            {
                return;
            }

            _breathOks.Enqueue(_raws.ElementAt(1).X - _raws.ElementAt(0).X);
            BreathOk();
            _raws.Dequeue();

        }

        /// <summary>
        /// 获取每分钟呼吸数
        /// </summary>
        private void BreathOk()
        {
            if (_breathOks.Count <= 4)
            {
                return;
            }

            _breathOks.Dequeue();

            double average = _breathOks.Average();
            if (average != 0)
            {
                GetBreath(600 / average);
            }
        }
    }
}
