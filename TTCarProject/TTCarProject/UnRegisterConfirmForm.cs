using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTCarProject
{
    public delegate void MyDelegate(string strNum, string confirmMan, string typeStr);
    public delegate void MyDelegate2(string strNum, string confirmMan, string typeStr);

    public partial class UnRegisterConfirmForm : Form
    {
        public string confirmManName;
        public string strNum;
        public string typeStr;

        public event MyDelegate MyEvent;
        public event MyDelegate2 MyEvent2;

        public UnRegisterConfirmForm()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        private void UnRegisterConfirmForm_Load(object sender, EventArgs e)
        {
            confirmManTextBox.Text = confirmManName;
            strNumTextBox.Text = strNum;
            this.ControlBox = false;

            this.bigTitleLabel.Text = "未注册车辆" + this.strNum + "请求" + ((typeStr == "1") ? "进门" : "出门") + "开闸";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyEvent(this.strNumTextBox.Text, this.confirmManTextBox.Text, this.typeStr);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyEvent2(this.strNumTextBox.Text, this.confirmManTextBox.Text, this.typeStr);

            this.Close();
        }
    }
}
