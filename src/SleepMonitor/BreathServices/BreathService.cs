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
        public int _breathCount = 0;

        //有效波形间隔计数器
        public int _breathWaveCount = 0;

        /// <summary>
        /// 更新呼吸信息
        /// </summary>
        public Action<double> GetBreath;

        public InitService intiService = new InitService(50);

        //滤波服务
        public AverageService averageService = new AverageService(50);

        //寻峰服务
        public WaveBreathService waveService = new WaveBreathService(7);

        //峰谷差服务
        public DifferenceService differenceService = new DifferenceService(4);
    }
}
