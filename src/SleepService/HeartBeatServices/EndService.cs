using SleepService.Models;
using SleepService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepService.HeartBeatServices
{
    public class EndService : BaseService
    {
        public EndService(int count) : base(count)
        {
        }

        public override bool Filter()
        {
            Func = () => false;
            bool filter = false;

            if (base.Filter())
            {
                //峰谷差
                double[] dots = this.Select(f => f.Y).ToArray();
                double average = 0.5 * (dots.Max() - dots.Min());
                                            //波峰              //波谷     
                WaveModel wave = Dequeue(); WaveModel peakWave; WaveModel troughWave;
                if (!wave.Type)      //当前波形是波谷
                {
                    troughWave = wave;  //直接设置波谷
                }
                else
                {
                    troughWave = Dequeue();   //设置下一个波形是波谷
                }
                peakWave = Dequeue(); //设置波峰

                Datas.Clear();

                #region 仰睡状态
                double ssfgc = peakWave.Y - this.ElementAt(0).Y;//仰睡状态的实时峰谷差
                double ssfgcmf = this.ElementAt(1).Y - this.ElementAt(2).Y;//仰睡数列内头部两个元素的峰谷差
                double ssfgcmi = this.ElementAt(3).Y - this.ElementAt(4).Y;
                
                //当前波峰后面3个波峰是否是连续递减
                if (ssfgc > average && ssfgc > ssfgcmf && ssfgc > ssfgcmi)
                {
                    //Datas.Add("心跳正向波", peakWave.X);
                    //filter = true;
                }
                #endregion

                #region 侧睡状态
                double csfgc = Math.Abs(peakWave.Y - troughWave.Y);//侧睡状态的实时峰谷差
                double csfgcmf = Math.Abs(this.ElementAt(1).Y - this.ElementAt(0).Y);//侧睡数列内头部两个元素的峰谷差
                double csfgcmi = Math.Abs(this.ElementAt(3).Y - this.ElementAt(2).Y);

                if (csfgc > average && csfgc > csfgcmf && csfgc > csfgcmi)
                {
                    Datas.Add("心跳反向波", troughWave.X);
                    filter = true;
                }
                #endregion

                return filter;
            }

            return filter;
        }
    }
}
