using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.IServices
{
    public interface IBaseService<T>
    {
        bool Filter(T service);
    }
}
