﻿using SleepService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepService.BreathServices
{
    public class EndService : BaseService
    {

        public EndService(int count) : base(count)
        {
        }

        public override bool Filter()
        {
            Func = () => true;
            if (base.Filter())
            {
                double average = this.Select(c => c.X).Average();
                if (average != 0)
                {
                    Datas = new Dictionary<string, double>();
                    Datas.Add("呼吸", average);
                    return true;
                }
            }

            return false;
        }
    }
}
