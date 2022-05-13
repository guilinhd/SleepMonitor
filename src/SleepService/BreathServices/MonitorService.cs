using SleepService.IServices;
using SleepService.Models;
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
        private List<IBaseService> _services = new List<IBaseService>();    //处理数据服务列表
        private int _totalCount = 0;    //数据累计
        private int _waveCount = 0;     //有效波形数据小计
        
        private BaseService _initService;
        /// <summary>
        /// 第一个服务,通过该服务添加呼吸数据,然后就可以依次执行过滤
        /// </summary>
        public BaseService Init { set => _initService = value; }
       
        private BaseService _endService;
        /// <summary>
        /// 最后一个服务, 通过该服务获取最终的有效呼吸数据
        /// </summary>
        public BaseService End { set => _endService = value; }

        /// <summary>
        /// 获得有效呼吸数据后通知外部
        /// </summary>
        public Action<double> GetBreath;    

        /// <summary>
        /// 添加处理数据服务
        /// </summary>
        /// <param name="service"></param>
        public void AddService(IBaseService service)
        {
            _services.Add(service);
        }

        /// <summary>
        /// 依次设置当前服务的下一个服务
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
        }

        /// <summary>
        /// 添加呼吸数据，激活过滤
        /// </summary>
        /// <param name="value">呼吸数据</param>
        public void Add(SensorModel model)
        {
            if (_totalCount > 2147483647)
            {
                _totalCount = 0;
            }

            _totalCount++;
            _waveCount++;

            #region 添加新数据
            _initService.Enqueue(new WaveModel()
            {
                Y = model.Breath
            });
            #endregion

            #region 过滤数据
            for (int i = 0; i < _services.Count - 1; i++)
            {
                _services[i].WaveCount = _waveCount;
                _services[i].TotalCount = _totalCount;

                _services[i].Filter();

                _waveCount = _services[i].WaveCount;
            }
            #endregion

            #region 判断是否获得有效数据
            if (_endService.Filter())
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
