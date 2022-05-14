using SleepService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SleepService.Services;

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

        //呼吸数据分析服务
        private AnalysisServiceFactory _breathService = new AnalysisServiceFactory();

        //心跳数据分析服务
        private AnalysisServiceFactory _heartBeatService = new AnalysisServiceFactory();

        //心跳数据正向波形分析服务
        private AnalysisServiceFactory _peakService;

        //心跳数据反向波形分析服务
        private AnalysisServiceFactory _troughService;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MyFormSet();
        }

        private void txtTest_Click(object sender, EventArgs e)
        {
            string input = txtForwardDetail.Text;

            _rcvBuff = input.Replace("AA33", ",AA33");//串口数据包分割
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
        }

        /// <summary>
        /// 开启或关闭 两个通信的串口，刷新按钮状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_open_Click(object sender, EventArgs e)
        {
            try
            {
                if (_pchReceive.PortState)
                {
                    //pchSend.ClosePort();
                    _pchReceive.ClosePort();
                }
                else
                {
                    //pchSend.OpenPort(cb_portNameSend.Text, int.Parse(cb_baudRate.Text),
                    //int.Parse(cb_dataBit.Text), int.Parse(cb_stopBit.Text),
                    //int.Parse(cb_timeout.Text));
                    _pchReceive.OpenPort(cboPortName.Text, int.Parse(txtBoudRate.Text),
                       int.Parse(txtDataBit.Text), int.Parse(txtStopBit.Text),
                       int.Parse(txtTimeOut.Text));
                    timer1.Enabled = true;
                }
                FreshBtnState(_pchReceive.PortState);
                _pchReceive.OnComReceiveDataHandler += new PortControlHelper.ComReceiveDataHandler(ComReceiveData);
                Btn_receive.Text = "停止接收";
                _receiveState = true;
            }
            catch { }
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

        /// <summary>
        /// 窗体初始化
        /// </summary>
        private void MyFormSet()
        {
            cboPortName.DataSource = _pchReceive.PortNameArr;

            InitBreathService();

            InitHeartBeatService();

            InitHeartBeatWaveServices();
        }

        /// <summary>
        /// 配置呼吸监测服务
        /// </summary>
        private void InitBreathService()
        {
            #region 添加处理数据服务
            StartService start = new StartService(50);
            _breathService.Start = start;

            SleepService.BreathServices.FilteringService filter = new SleepService.BreathServices.FilteringService(6);
            _breathService.AddService(filter);

            SleepService.BreathServices.WaveService wave = new SleepService.BreathServices.WaveService(7);
            _breathService.AddService(wave);

            SleepService.BreathServices.EndService end = new SleepService.BreathServices.EndService(4);
            _breathService.End = end;
            #endregion

            #region 处理获得呼吸数据
            _breathService.GetValidDatas += (datas) =>
            {
                double count = datas.First().Value;
                txtBreath.Text = count.ToString("0.0");
                txtBreathDetail.Text = count.ToString("0.0") + " (" + DateTime.Now.ToString("MM-dd hh:mm:ss") + ")" + "\r\n" + txtBreathDetail.Text;
            };
            #endregion

            _breathService.BuildService();
        }

        /// <summary>
        /// 配置心跳监测服务
        /// </summary>
        private void InitHeartBeatService()
        {
            #region 添加处理数据服务
            StartService start = new StartService(10);
            _heartBeatService.Start = start;

            SleepService.HeartBeatServices.FilteringService filter = new SleepService.HeartBeatServices.FilteringService(7);
            _heartBeatService.AddService(filter);

            SleepService.HeartBeatServices.EndService end = new SleepService.HeartBeatServices.EndService(27);
            _heartBeatService.End = end;
            #endregion

            #region 处理获得心跳波形数据
            _heartBeatService.GetValidDatas += (datas) =>
            {
                if (datas.TryGetValue("心跳正向波", out double peak))
                {
                    _peakService.Add(new double[] { peak });
                }

                if (datas.TryGetValue("心跳反向波", out double trough))
                {
                    _troughService.Add(new double[] { trough });
                }
            };
            #endregion

            _heartBeatService.BuildService();
        }

        /// <summary>
        /// 配置心跳波形监测服务
        /// </summary>
        private void InitHeartBeatWaveServices()
        {
            _peakService = CreateInstance();
            _peakService.GetValidDatas += (datas) =>
            {
                double count = datas.First().Value;
                txtForward.Text = count.ToString("0.0");
                txtForwardCount.Text = _peakService.ValidCount.ToString();
                txtForwardDetail.Text = count.ToString("0.0") + " (" + DateTime.Now.ToString("MM-dd hh:mm:ss") + ")" + "\r\n" + txtForwardDetail.Text;
            };

            _troughService = CreateInstance();
            _troughService.GetValidDatas += (datas) =>
            {
                double count = datas.First().Value;
                txtReverse.Text = count.ToString("0.0");
                txtReverseCount.Text = _troughService.ValidCount.ToString();
                txtReverseDetail.Text = count.ToString("0.0") + " (" + DateTime.Now.ToString("MM-dd hh:mm:ss") + ")" + "\r\n" + txtReverseDetail.Text;
            };
        }

        /// <summary>
        /// 生成心跳数据波形分析服务
        /// </summary>
        /// <returns>分析服务</returns>
        private AnalysisServiceFactory CreateInstance()
        {
            AnalysisServiceFactory service = new AnalysisServiceFactory();

            #region 添加处理数据服务
            SleepService.HeartBeatWaveServices.StartService start = new SleepService.HeartBeatWaveServices.StartService(7);
            service.Start = start;

            SleepService.HeartBeatWaveServices.FilteringService filter = new SleepService.HeartBeatWaveServices.FilteringService(7);
            service.AddService(filter);

            SleepService.HeartBeatWaveServices.WaveService wave = new SleepService.HeartBeatWaveServices.WaveService(6);
            service.AddService(wave);

            SleepService.HeartBeatWaveServices.EndService end = new SleepService.HeartBeatWaveServices.EndService(3);
            service.End = end;
            #endregion

            service.BuildService();

            return service;
        }

        /// <summary>
        /// 刷新按钮状态
        /// </summary>
        /// <param name="state"></param>
        private void FreshBtnState(bool state)
        {
            if (state)
            {
                Btn_open.Text = "关闭串口";
                Btn_receive.Enabled = true;
            }
            else
            {
                Btn_open.Text = "打开串口";
                Btn_receive.Enabled = false;
            }
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
            if ((data.Contains("AA33") && data.Length == 108) | (data.Contains("AA35") && data.Length == 112))
            {
                //解析数据
                SensorModel model = new SensorModel().GetRaw(data.Substring(4));

                //添加呼吸数据
                //_breathService.Add(new double[] { model.Breath});

                //添加心跳数据
                _heartBeatService.Add(model.HeartBeats.ToArray());
            }
        }
    }
}
