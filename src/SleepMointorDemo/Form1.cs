using SleepMonitor.Models;
using SleepMonitor.Services;
using SleepMonitor.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SleepMointorDemo
{
    public partial class Form1 : Form
    {
        private Queue<string> _rcvPortBuff = new Queue<string>();//串口数据接收缓存
        private string _rcvBuff = "";//数据接收缓存
        private string _rcvPre = ""; //上次数据接收缓存
        private bool _receiveState = false;

        //串口
        private PortControlHelper _pchReceive = new PortControlHelper();

        //心跳监测服务
        private HeartBeatMonitorService _heartBeatMonitorService = new HeartBeatMonitorService();

        //呼吸监测服务
        private BreathMonitorService _breathMonitorService = new BreathMonitorService();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MyFormSet();
        }

        /// <summary>
        /// 点击 开始接收 按钮，开始监听串口接收入口数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_receive_Click(object sender, EventArgs e)
        {
            if (_receiveState)
            {
                _pchReceive.OnComReceiveDataHandler -= new PortControlHelper.ComReceiveDataHandler(ComReceiveData);//解除委托，数据停止
                Btn_receive.Text = "开始接收";
                _receiveState = false;
                timer1.Enabled = false;
            }
            else
            {
                _pchReceive.OnComReceiveDataHandler += new PortControlHelper.ComReceiveDataHandler(ComReceiveData);//添加委托，数据开始
                Btn_receive.Text = "停止接收";
                _receiveState = true;
                timer1.Enabled = true;
            }
        }

        /// <summary>
        /// 处理串口获取到的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_rcvPortBuff.Count > 10)
            {
                _rcvBuff = _rcvPre;

                for (int i = 0; i < _rcvPortBuff.Count - 1; i++)
                {
                    _rcvBuff += _rcvPortBuff.Dequeue();
                }

                _rcvBuff = _rcvBuff.Replace("AA33", ",AA33");//串口数据包分割
                _rcvBuff = _rcvBuff.Replace("AA35", ",AA35");//串口数据包分割
                string[] ssa = _rcvBuff.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < ssa.Length; i++)
                {
                    if (i == ssa.Length - 1)//这是数据包的尾巴，丢入缓存
                    {
                        _rcvPre = ssa[i];
                    }
                    if (i != ssa.Length - 1)//这是正常取出来的完整数据包
                    {
                        DataHand(ssa[i]);
                    }

                }

                _rcvBuff = "";
            }


        }

        private void MyFormSet()
        {
            cboPortName.DataSource = _pchReceive.PortNameArr;

            _heartBeatMonitorService.GetHeartBeat += GetHeartBeat;
            _breathMonitorService.GetBreath += GetBreath;
        }

        private void GetHeartBeat(string msg, double count)
        {
            Console.WriteLine($"{msg} == {count}");
        }

        private void GetBreath(double count)
        {
            Console.WriteLine($"呼吸输出:{count:0.0}");
        }

        /// <summary>
        /// 接收到的数据，写入缓存
        /// </summary>
        /// <param name="data"></param>
        private void ComReceiveData(string data)
        {
            try
            {
                this.Invoke(new EventHandler(delegate { _rcvPortBuff.Enqueue(data); }));

            }
            catch { }
        }

        
        /// <summary>
        /// 调用服务解析呼吸、心跳
        /// </summary>
        /// <param name="data">单行数据</param>
        private void DataHand(string data)
        {
            //解析数据
            SensorModel model = new SensorModel().GetRaw(data.Substring(4));

            //添加呼吸数据
            _breathMonitorService.Add(model);

            //添加心跳数据
            _heartBeatMonitorService.Add(model);
        }
    }
}
