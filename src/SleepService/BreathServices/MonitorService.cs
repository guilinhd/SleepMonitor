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
        private BaseService _initService;
        private BaseService _endService;

        private int _totalCount = 0;
        private int _waveCount = 0;

        public Action<double> GetBreath;
        public int FilterCount { set; get; }
        public void AddService(IBaseService service)
        {
            _services.Add(service);
        }

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
