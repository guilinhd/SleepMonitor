using SleepService.IServices;
using SleepService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepService.BreathServices
{
    public class MonitorService
    {
        private List<IBaseService> _services = new List<IBaseService>();
        private BaseService _initService; //第一个服务,通过该服务添加呼吸数据,然后就可以依次执行过滤
        private BaseService _endService;    //最后一个服务, 通过该服务获取最终的有效呼吸数据

        private int _totalCount = 0;    //数据累计
        private int _waveCount = 0;     //有效波形数据小计

        /// <summary>
        /// 获得有效呼吸数据后通知外部
        /// </summary>
        public Action<double> GetBreath;    

        /// <summary>
        /// 有效呼吸数据队列的最小长度
        /// </summary>
        public int FilterCount { set; get; }  

        /// <summary>
        /// 添加处理数据服务
        /// </summary>
        /// <param name="service"></param>
        public void AddService(IBaseService service)
        {
            _services.Add(service);
        }

        /// <summary>
        /// 依次设置当前服务的下一个服务, 并获取第一个、最后一个服务
        /// </summary>
        public void BuildService()
        {
            for (int i = 0; i < _services.Count; i++)
            {
                if (i < _services.Count - 1)
                {
                    _services[i].SetNext(_services[i + 1]);
                }
            }

            _initService = (BaseService)_services.First();
            _endService = (BaseService)_services.Last();
        }

        /// <summary>
        /// 添加呼吸数据，激活过滤
        /// </summary>
        /// <param name="value">呼吸数据</param>
        public void Add(double value)
        {
            if (_totalCount > 2147483647)
            {
                _totalCount = 0;
            }

            _totalCount++;
            _waveCount++;

            #region 添加新数据
            _initService.Enqueue(new Models.WaveModel()
            {
                X = _totalCount,
                Y = value
            });
            #endregion

            #region 过滤数据
            foreach (var service in _services)
            {
                service.WaveCount = _waveCount;
                service.TotalCount = _totalCount;

                service.Filter();

                _waveCount = service.WaveCount;
            }
            #endregion

            #region 判断是否获得有效数据
            if (_endService.Count >= FilterCount)
            {
                _endService.Dequeue();
                double average = _endService.Select(c => c.X).Average();
                if (average != 0)
                {
                    GetBreath(600 / average);
                }
            }
            #endregion
        }


    }
}
