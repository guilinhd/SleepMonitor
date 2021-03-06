using SleepMonitor.Models;
using System.Linq;

namespace SleepMonitor.Services
{
    public static class BreathServiceExtensions
    {
        /// <summary>
        /// 初始化-处理原始数据
        /// </summary>
        /// <param name="service">呼吸服务</param>
        /// <param name="model">呼吸数据</param>
        /// <returns></returns>
        public static BreathService Init(this BreathService service, SensorModel model)
        {
            if (service._breathCount > 2147483647)
            {
                service._breathCount = 0;
            }

            service._breathCount++;
            service._breathWaveCount++;

            service.intiService.Enqueue(model.Breath);
            service.intiService.Filter(service.averageService);

            return service;
        }

        /// <summary>
        /// 处理滤波数据
        /// </summary>
        /// <param name="service">呼吸服务</param>
        /// <returns></returns>
        public static BreathService Average(this BreathService service)
        {
            service.averageService.TotalCount = service._breathCount;
            service.averageService.WaveCount = service._breathWaveCount;

            if (service.averageService.Filter(service.waveService))
            {
                service._breathWaveCount = service.averageService.WaveCount;
            }
            return service;
        }

        /// <summary>
        /// 处理波峰谷数据
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static BreathService Wave(this BreathService service)
        {
            service.waveService.Filter(service.differenceService);
            return service;
        }

        /// <summary>
        /// 服务结束-处理峰谷差数据
        /// </summary>
        /// <param name="service"></param>
        public static void Build(this BreathService service)
        {
            service.differenceService.BreathService = service;
            service.differenceService.Filter(null);
        }
    }
}
