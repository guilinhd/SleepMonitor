using SleepMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Services
{
    public static class HeartBeatServiceExtensions
    {
        /// <summary>
        /// 滤波
        /// </summary>
        /// <param name="service"></param>
        /// <param name="value">心跳数据</param>
        /// <returns></returns>
        public static HeartBeatService Average(this HeartBeatService service,  double value)
        {
            if (service._heartBeatCount > 2147483647)
            {
                service._heartBeatCount = 0;
            }

            service.averageService.Enqueue(value);
            service._heartBeatCount++;

            return service;
        }

        /// <summary>
        /// 寻波峰谷
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static HeartBeatService Wave(this HeartBeatService service)
        {
            service.waveService.TotalCount = service._heartBeatCount;
            service.waveService.Datas = service.averageService;

            if (service.waveService.Filter())
            {
                service.averageService.Dequeue();
            }

            return service;
        }

        /// <summary>
        /// 过滤波峰谷
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static HeartBeatService WaveValid(this HeartBeatService service)
        {
            service.waveValidService.Datas = service.waveService;
            service.waveValidService.Filter();
            
            return service;
        }

        
    }
}
