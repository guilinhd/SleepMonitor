using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepService.Models;
using SleepService.Services;

namespace SleepServiceDemo
{
    public class MonitorService
    {
        //呼吸数据分析服务
        private AnalysisServiceFactory _breathService = new AnalysisServiceFactory();

        //心跳数据分析服务
        private AnalysisServiceFactory _heartBeatService = new AnalysisServiceFactory();

        //心跳数据正向波形分析服务
        private AnalysisServiceFactory _peakService;

        //心跳数据反向波形分析服务
        private AnalysisServiceFactory _troughService;

        public MonitorService()
        {
            InitBreathService();

            InitHeartBeatService();

            InitHeartBeatWaveServices();
        }

        public void Add(string data)
        {
            //解析数据
            SensorModel model = new SensorModel().GetRaw(data.Substring(4));

            //添加呼吸数据
            _breathService.Add(new double[] { model.Breath});

            //添加心跳数据
            _heartBeatService.Add(model.HeartBeats.ToArray());
        }

        /// <summary>
        /// 配置呼吸监测服务
        /// </summary>
        private void InitBreathService()
        {
            #region 添加处理数据服务
            StartService start = new StartService(50);
            _breathService.Start = start;

            SleepService.BreathServices.FilteringService filter = new SleepService.BreathServices.FilteringService(6);
            _breathService.AddService(filter);

            SleepService.BreathServices.WaveService wave = new SleepService.BreathServices.WaveService(7);
            _breathService.AddService(wave);

            SleepService.BreathServices.EndService end = new SleepService.BreathServices.EndService(4);
            _breathService.End = end;
            #endregion

            #region 处理获得呼吸数据
            _breathService.GetValidDatas += (datas) =>
            {
                double count = datas.First().Value;
                Console.WriteLine($"呼吸值:{count.ToString("0.0")} ({DateTime.Now.ToString("MM-dd hh:mm:ss")})" );
            };
            #endregion

            _breathService.BuildService();
        }

        /// <summary>
        /// 配置心跳监测服务
        /// </summary>
        private void InitHeartBeatService()
        {
            #region 添加处理数据服务
            StartService start = new StartService(10);
            _heartBeatService.Start = start;

            SleepService.HeartBeatServices.FilteringService filter = new SleepService.HeartBeatServices.FilteringService(7);
            _heartBeatService.AddService(filter);

            SleepService.HeartBeatServices.EndService end = new SleepService.HeartBeatServices.EndService(27);
            _heartBeatService.End = end;
            #endregion

            #region 处理获得心跳波形数据
            _heartBeatService.GetValidDatas += (datas) =>
            {
                if (datas.TryGetValue("心跳正向波", out double peak))
                {
                    _peakService.Add(new double[] { peak });
                }

                if (datas.TryGetValue("心跳反向波", out double trough))
                {
                    _troughService.Add(new double[] { trough });
                }
            };
            #endregion

            _heartBeatService.BuildService();
        }

        /// <summary>
        /// 配置心跳波形监测服务
        /// </summary>
        private void InitHeartBeatWaveServices()
        {
            _peakService = CreateInstance();
            _peakService.GetValidDatas += (datas) =>
            {
                double count = datas.First().Value;
                Console.WriteLine($"仰睡心跳值:{count.ToString("0.0")} ({DateTime.Now.ToString("MM-dd hh:mm:ss")})");
            };

            _troughService = CreateInstance();
            _troughService.GetValidDatas += (datas) =>
            {
                double count = datas.First().Value;
                Console.WriteLine($"侧睡心跳值:{count.ToString("0.0")} ({DateTime.Now.ToString("MM-dd hh:mm:ss")})");
            };
        }

        /// <summary>
        /// 生成心跳数据波形分析服务
        /// </summary>
        /// <returns>分析服务</returns>
        private AnalysisServiceFactory CreateInstance()
        {
            AnalysisServiceFactory service = new AnalysisServiceFactory();

            #region 添加处理数据服务
            SleepService.HeartBeatWaveServices.StartService start = new SleepService.HeartBeatWaveServices.StartService(7);
            service.Start = start;

            SleepService.HeartBeatWaveServices.FilteringService filter = new SleepService.HeartBeatWaveServices.FilteringService(7);
            service.AddService(filter);

            SleepService.HeartBeatWaveServices.WaveService wave = new SleepService.HeartBeatWaveServices.WaveService(6);
            service.AddService(wave);

            SleepService.HeartBeatWaveServices.EndService end = new SleepService.HeartBeatWaveServices.EndService(3);
            service.End = end;
            #endregion

            service.BuildService();

            return service;
        }
    }
}
