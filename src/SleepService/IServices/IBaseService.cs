using SleepService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepService.IServices
{
    public interface IBaseService
    {
        int TotalCount { get; set; }

        int WaveCount { get; set; }

        void SetNext(IBaseService next);

        WaveModel Wave { get; set; }

        Func<bool> Func { get; set; }

        bool Filter();
    }
}
