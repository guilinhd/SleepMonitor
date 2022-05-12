
namespace SleepMointorDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Btn_receive = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Btn_open = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoudRate = new System.Windows.Forms.TextBox();
            this.txtDataBit = new System.Windows.Forms.TextBox();
            this.txtStopBit = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.txtTimeOut = new System.Windows.Forms.TextBox();
            this.cboPortName = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtBreath = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.txtBreathDetail = new System.Windows.Forms.RichTextBox();
            this.txtForward = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtForwardCount = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtForwardDetail = new System.Windows.Forms.TextBox();
            this.txtReverse = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtReverseCount = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtReverseDetail = new System.Windows.Forms.TextBox();
            this.txtTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_receive
            // 
            this.Btn_receive.Location = new System.Drawing.Point(886, 23);
            this.Btn_receive.Name = "Btn_receive";
            this.Btn_receive.Size = new System.Drawing.Size(100, 23);
            this.Btn_receive.TabIndex = 21;
            this.Btn_receive.Text = "接受数据";
            this.Btn_receive.UseVisualStyleBackColor = true;
            this.Btn_receive.Click += new System.EventHandler(this.Btn_receive_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(15, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 17);
            this.label10.TabIndex = 22;
            this.label10.Text = "接收端：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(651, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 17);
            this.label9.TabIndex = 20;
            this.label9.Text = "超时ms：";
            // 
            // Btn_open
            // 
            this.Btn_open.Location = new System.Drawing.Point(786, 23);
            this.Btn_open.Name = "Btn_open";
            this.Btn_open.Size = new System.Drawing.Size(100, 23);
            this.Btn_open.TabIndex = 19;
            this.Btn_open.Text = "打开串口";
            this.Btn_open.UseVisualStyleBackColor = true;
            this.Btn_open.Click += new System.EventHandler(this.Btn_open_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(531, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 18;
            this.label6.Text = "校验位：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(410, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "停止位：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(284, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "数据位：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(164, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "波特率：";
            // 
            // txtBoudRate
            // 
            this.txtBoudRate.Location = new System.Drawing.Point(220, 23);
            this.txtBoudRate.Name = "txtBoudRate";
            this.txtBoudRate.Size = new System.Drawing.Size(49, 21);
            this.txtBoudRate.TabIndex = 24;
            this.txtBoudRate.Text = "38400";
            // 
            // txtDataBit
            // 
            this.txtDataBit.Location = new System.Drawing.Point(340, 23);
            this.txtDataBit.Name = "txtDataBit";
            this.txtDataBit.Size = new System.Drawing.Size(49, 21);
            this.txtDataBit.TabIndex = 25;
            this.txtDataBit.Text = "8";
            // 
            // txtStopBit
            // 
            this.txtStopBit.Location = new System.Drawing.Point(466, 23);
            this.txtStopBit.Name = "txtStopBit";
            this.txtStopBit.Size = new System.Drawing.Size(49, 21);
            this.txtStopBit.TabIndex = 26;
            this.txtStopBit.Text = "1";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(587, 23);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(49, 21);
            this.textBox4.TabIndex = 27;
            this.textBox4.Text = "None";
            // 
            // txtTimeOut
            // 
            this.txtTimeOut.Location = new System.Drawing.Point(712, 23);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Size = new System.Drawing.Size(49, 21);
            this.txtTimeOut.TabIndex = 28;
            this.txtTimeOut.Text = "500";
            // 
            // cboPortName
            // 
            this.cboPortName.FormattingEnabled = true;
            this.cboPortName.Location = new System.Drawing.Point(71, 23);
            this.cboPortName.Name = "cboPortName";
            this.cboPortName.Size = new System.Drawing.Size(75, 20);
            this.cboPortName.TabIndex = 29;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtBreath
            // 
            this.txtBreath.Location = new System.Drawing.Point(90, 69);
            this.txtBreath.Name = "txtBreath";
            this.txtBreath.Size = new System.Drawing.Size(69, 21);
            this.txtBreath.TabIndex = 45;
            this.txtBreath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(25, 74);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(65, 12);
            this.label42.TabIndex = 44;
            this.label42.Text = "呼吸率输出";
            // 
            // txtBreathDetail
            // 
            this.txtBreathDetail.Location = new System.Drawing.Point(19, 96);
            this.txtBreathDetail.Name = "txtBreathDetail";
            this.txtBreathDetail.Size = new System.Drawing.Size(263, 355);
            this.txtBreathDetail.TabIndex = 46;
            this.txtBreathDetail.Text = "";
            // 
            // txtForward
            // 
            this.txtForward.Location = new System.Drawing.Point(397, 65);
            this.txtForward.Name = "txtForward";
            this.txtForward.Size = new System.Drawing.Size(76, 21);
            this.txtForward.TabIndex = 48;
            this.txtForward.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(302, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 47;
            this.label8.Text = "正向波心率输出";
            // 
            // txtForwardCount
            // 
            this.txtForwardCount.Location = new System.Drawing.Point(553, 65);
            this.txtForwardCount.Name = "txtForwardCount";
            this.txtForwardCount.Size = new System.Drawing.Size(76, 21);
            this.txtForwardCount.TabIndex = 50;
            this.txtForwardCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(488, 69);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 12);
            this.label18.TabIndex = 49;
            this.label18.Text = "正向波计数";
            // 
            // txtForwardDetail
            // 
            this.txtForwardDetail.Location = new System.Drawing.Point(304, 96);
            this.txtForwardDetail.Multiline = true;
            this.txtForwardDetail.Name = "txtForwardDetail";
            this.txtForwardDetail.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtForwardDetail.Size = new System.Drawing.Size(339, 355);
            this.txtForwardDetail.TabIndex = 51;
            // 
            // txtReverse
            // 
            this.txtReverse.Location = new System.Drawing.Point(761, 65);
            this.txtReverse.Name = "txtReverse";
            this.txtReverse.Size = new System.Drawing.Size(76, 21);
            this.txtReverse.TabIndex = 53;
            this.txtReverse.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(666, 69);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 12);
            this.label13.TabIndex = 52;
            this.label13.Text = "反向波心率输出";
            // 
            // txtReverseCount
            // 
            this.txtReverseCount.Location = new System.Drawing.Point(911, 65);
            this.txtReverseCount.Name = "txtReverseCount";
            this.txtReverseCount.Size = new System.Drawing.Size(76, 21);
            this.txtReverseCount.TabIndex = 55;
            this.txtReverseCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(846, 69);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 54;
            this.label19.Text = "反向波计数";
            // 
            // txtReverseDetail
            // 
            this.txtReverseDetail.Location = new System.Drawing.Point(668, 96);
            this.txtReverseDetail.Multiline = true;
            this.txtReverseDetail.Name = "txtReverseDetail";
            this.txtReverseDetail.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtReverseDetail.Size = new System.Drawing.Size(319, 355);
            this.txtReverseDetail.TabIndex = 56;
            // 
            // txtTest
            // 
            this.txtTest.Location = new System.Drawing.Point(167, 69);
            this.txtTest.Name = "txtTest";
            this.txtTest.Size = new System.Drawing.Size(75, 23);
            this.txtTest.TabIndex = 57;
            this.txtTest.Text = "测试";
            this.txtTest.UseVisualStyleBackColor = true;
            this.txtTest.Click += new System.EventHandler(this.txtTest_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 468);
            this.Controls.Add(this.txtTest);
            this.Controls.Add(this.txtReverseDetail);
            this.Controls.Add(this.txtReverseCount);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtReverse);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtForwardDetail);
            this.Controls.Add(this.txtForwardCount);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtForward);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtBreathDetail);
            this.Controls.Add(this.txtBreath);
            this.Controls.Add(this.label42);
            this.Controls.Add(this.cboPortName);
            this.Controls.Add(this.txtTimeOut);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.txtStopBit);
            this.Controls.Add(this.txtDataBit);
            this.Controls.Add(this.txtBoudRate);
            this.Controls.Add(this.Btn_receive);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Btn_open);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_receive;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button Btn_open;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoudRate;
        private System.Windows.Forms.TextBox txtDataBit;
        private System.Windows.Forms.TextBox txtStopBit;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox txtTimeOut;
        private System.Windows.Forms.ComboBox cboPortName;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtBreath;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.RichTextBox txtBreathDetail;
        private System.Windows.Forms.TextBox txtForward;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtForwardCount;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtForwardDetail;
        private System.Windows.Forms.TextBox txtReverse;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtReverseCount;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtReverseDetail;
        private System.Windows.Forms.Button txtTest;
    }
}

