using SleepMonitor.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.Services
{
    /// <summary>
    /// 添加原始数据并添加滤波后的数据
    /// </summary>
    public class InitService : Queue<double>, IBaseService<AverageService>
    {
        private int FilterCount { set; get; }
        public InitService(int count)
        {
            FilterCount = count;
        }

        /// <summary>
        /// 滤波操作
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public bool Filter(AverageService service)
        {
            if (Count >= FilterCount)
            {
                //取平均值
                service.Enqueue(this.Average());

                //移除第一个数据
                Dequeue();

                return true;
            }

            return false;
        }
    }
}
