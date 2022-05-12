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

        //初始化服务
        public InitService intiService = new InitService(50);

        //滤波服务
        public AverageBreathService averageService = new AverageBreathService(7);

        //寻峰服务
        public WaveBreathService waveService = new WaveBreathService(7);

        //峰谷差服务
        public DifferenceBreathService differenceService = new DifferenceBreathService(4);
    }
}
