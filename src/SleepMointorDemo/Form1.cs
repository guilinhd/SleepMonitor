﻿using SleepMonitor.Models;
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
        private HeartBeatService _heartBeatService = new HeartBeatService();

        //呼吸服务
        private BreathService _breathService = new BreathService();

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

            _heartBeatService.GetHeartBeat += GetHeartBeat;
            _breathService.GetBreath += GetBreath;
        }

        private void GetHeartBeat(string msg, double count)
        {
            //if (msg.Contains("反向波"))
            //{
            //    txtReverse.Text = count.ToString("0.0");
            //    txtReverseCount.Text = _heartBeatService.TroughCount.ToString();
            //    txtReverseDetail.Text = count.ToString("0.0") + " (" + DateTime.Now.ToString("MM-dd hh:mm:ss") + ")" + "\r\n" + txtReverseDetail.Text;
            //}
            //else
            //{
            //    txtForward.Text = count.ToString("0.0");
            //    txtForwardCount.Text = _heartBeatService.PeakCount.ToString();
            //    txtForwardDetail.Text = count.ToString("0.0") + " (" + DateTime.Now.ToString("MM-dd hh:mm:ss") + ")" + "\r\n" + txtForwardDetail.Text;
            //}
        }

        private void GetBreath(double count)
        {
            txtBreath.Text = count.ToString("0.0");
            txtBreathDetail.Text = count.ToString("0.0") + " (" + DateTime.Now.ToString("MM-dd hh:mm:ss") + ")" + "\r\n" + txtBreathDetail.Text;
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
                //_breathService
                //    .Average(model)   
                //    .Wave()
                //    .Difference()
                //    .Build();
                _breathService
                    .Init(model)
                    .Average()
                    .Wave()
                    .Build();


                //添加心跳数据
                //foreach (var item in model.HeartBeats)
                //{
                //    _heartBeatService
                //        .Average(item)
                //        .Wave()
                //        .WaveValid();
                //}
            }
        }

        
    }
}
