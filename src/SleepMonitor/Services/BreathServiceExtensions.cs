using SleepMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Services
{
    public static class BreathServiceExtensions
    {
        /// <summary>
        /// 滤波
        /// </summary>
        /// <param name="service">呼吸服务</param>
        /// <param name="model">呼吸数据</param>
        /// <returns></returns>
        public static BreathService Average(this BreathService service, SensorModel model)
        {
            if (service._breathCount > 2147483647)
            {
                service._breathCount = 0;
            }

            service._breathCount++;
            service._breathWaveCount++;

            service.averageService.Add(model.Breath);
            service.averageService.Filter();

            return service;
        }

        /// <summary>
        /// 寻波峰谷
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static BreathService Wave(this BreathService service)
        {
            service.waveService.TotalCount = service._breathCount;
            service.waveService.WaveCount = service._breathWaveCount;
            service.waveService.Datas = service.averageService;

            if (service.waveService.Filter())
            {
                service.averageService.Dequeue();
                service._breathWaveCount = service.waveService.WaveCount;
            }

            return service;
        }

        /// <summary>
        /// 计算峰谷差
        /// </summary>
        /// <returns></returns>
        public static BreathService Difference(this BreathService service)
        {
            service.differenceService.Datas = service.waveService;
            if (service.differenceService.Filter())
            {
                service.waveService.Dequeue();
            }

            return this;
        }

        /// <summary>
        /// 获取呼吸值
        /// </summary>
        /// <param name="service"></param>
        public static void Build(this BreathService service)
        {
            if (service.differenceService.Count > 4)
            {
                service.differenceService.Dequeue();
                double average = service.differenceService.Average();
                if (average != 0)
                {
                    service.GetBreath(600 / average);
                }
            }
        }
    }
}
