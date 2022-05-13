using SleepService.IServices;
using SleepService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepService.Services
{
    public class BaseService : Queue<WaveModel>, IBaseService
    {
        private int _filterCount = 0;
        public BaseService(int count)
        {
            _filterCount = count;
        }

        private BaseService _next;

        public int TotalCount { get; set; }
        public int WaveCount { get; set; }

        public void SetNext(IBaseService next)
        {
            _next = (BaseService)next;
        }

        public BaseService GetNext()
        {
            return _next;
        }

        public virtual bool Filter()
        {
            return Count > _filterCount;
        }
    }
}
