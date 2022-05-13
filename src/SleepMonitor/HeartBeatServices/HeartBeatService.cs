using SleepMonitor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Services
{
    public class HeartBeatService
    {
        //心率计数器
        public int _heartBeatCount = 0;

        /// <summary>
        /// 更新心跳信息
        /// </summary>
        public Action<string, double> GetHeartBeat;

        //滤波服务
        public AverageBreathService averageService = new AverageBreathService(10);

        //寻峰服务
        public WaveHeartBeatService waveService = new WaveHeartBeatService(7);

        //过滤波形服务
        public WaveValidHeartBeatService waveValidService = new WaveValidHeartBeatService(27);

        //心跳子服务
        public HeartBeatChildService forwardWave = new HeartBeatChildService("正向波", 7);

        //心跳子服务
        public HeartBeatChildService reverseWave = new HeartBeatChildService("反向波", 7);
    }
}
