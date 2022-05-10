using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepMonitor.IServices
{
    public interface IFilter
    {
        int FilterCount {  get; }

        bool Filter();
    }
}
