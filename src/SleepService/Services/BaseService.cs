﻿using SleepService.IServices;
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
        public WaveModel Wave { get; set; }
        public Func<bool> Func { get; set; }

        public void SetNext(IBaseService next)
        {
            _next = (BaseService)next;
        }

        public BaseService GetNext()
        {
            return _next;
        }

        /// <summary>
        /// 最终结果  key-类型  value-值
        /// </summary>
        public Dictionary<string, double> Datas { get; set; } = new Dictionary<string, double>();

        public virtual bool Filter()
        {
            if (Count > _filterCount)
            {
                if (_next != null)  //不是最后一个环节
                {
                    if (Func.Invoke())
                    {
                        _next.Enqueue(Wave);
                    }
                    Dequeue();
                }

                return true;
            }

            return false;
        }
    }
}
