using SleepMonitor.IServices;
using SleepMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Services
{
    public class BreathService
    {
        //呼吸计数器
        private int _breathCount = 0;

        //有效波形间隔计数器
        private int _breathWaveCount = 0;

        /// <summary>
        /// 更新呼吸信息
        /// </summary>
        public Action<double> GetBreath;

        //滤波服务
        private AverageService averageService = new AverageService(50);

        //寻峰服务
        private WaveService waveService = new WaveService(7);

        //峰谷差服务
        private DifferenceService differenceService = new DifferenceService(7);

        /// <summary>
        /// 滤波
        /// </summary>
        /// <param name="model">呼吸数据</param>
        /// <returns></returns>
        public BreathService Average(SensorModel model)
        {
            if (_breathCount > 2147483647)
            {
                _breathCount = 0;
            }

            _breathCount++;
            _breathWaveCount++;

            averageService.Add(model.Breath);
            averageService.Filter();

            return this;
        }

        /// <summary>
        /// 寻波峰谷
        /// </summary>
        /// <returns></returns>
        public BreathService Wave()
        {
            waveService.TotalCount = _breathCount;
            waveService.WaveCount = _breathWaveCount;
            waveService.Datas = averageService;

            if (waveService.Filter())
            {
                averageService.Dequeue();
                _breathWaveCount = waveService.WaveCount;
            }

            return this;
        }

        /// <summary>
        /// 计算峰谷差
        /// </summary>
        /// <returns></returns>
        public BreathService Difference()
        {
            differenceService.Datas = waveService;
            if (differenceService.Filter())
            {
                waveService.Dequeue();
            }

            return this;
        }

        /// <summary>
        /// 取呼吸值
        /// </summary>
        public void Build()
        {
            if (differenceService.Count > 4)
            {
                differenceService.Dequeue();
                double average = differenceService.Average();
                if (average != 0)
                {
                    GetBreath(600 / average);
                }
            }
        }
    }
}
