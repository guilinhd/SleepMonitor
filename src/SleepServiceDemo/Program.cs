using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepServiceDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string _rcvBuff = "";//数据接收缓存
            string _rcvPre = ""; //上次数据接收缓存
            string data = "";

            MonitorService service = new MonitorService();

            #region 从记事本中读取测试数据
            _rcvBuff = File.ReadAllText(@"c:\sleepdata.txt", Encoding.ASCII);

            _rcvBuff = _rcvBuff.Replace("AA33", ",AA33");//串口数据包分割
            _rcvBuff = _rcvBuff.Replace("AA35", ",AA35");//串口数据包分割
            #endregion

            #region 分析数据
            string[] ssa = _rcvBuff.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < ssa.Length; i++)
            {
                if (i == ssa.Length - 1)//这是数据包的尾巴，丢入缓存
                {
                    _rcvPre = ssa[i];
                }
                if (i != ssa.Length - 1)//这是正常取出来的完整数据包
                {
                    data = ssa[i];
                    if ((data.Contains("AA33") && data.Length == 108) | (data.Contains("AA35") && data.Length == 112))
                    {
                        service.Add(data);
                    }
                }
            }
            #endregion

            Console.ReadKey();
        }
    }
}
