using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Threading;





namespace TTCarProject
{
    public partial class AddAppointForm : Form
    {
        public delegate void InitMainUI(string str);
        InitMainUI myMainUI;
        FormLoading2 formLoading;
        public AddAppointForm()
        {
            InitializeComponent();
            
        }

        private void AddAppointForm_Load(object sender, EventArgs e)
        {
            typeComboBox.SelectedText = "生产车辆";
            typeComboBox.SelectedIndex = 0;
            managerTextBox.Text = MainForm.username;
            myMainUI = new InitMainUI(endLoad);


        }

        private void cancleButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {

            if (carnumberTextBox.Text.Length == 0){
                MessageBox.Show("车牌号不能为空！");
                return;
            }

            if (driverNameTextBox.Text.Length == 0)
            {
                MessageBox.Show("司机姓名不能为空！");
                return;
            }

            if (driverPhonetextBox.Text.Length == 0)
            {
                MessageBox.Show("司机电话不能为空！");
                return;
            }

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("appointMan", managerTextBox.Text);
            param.Add("appointType", (typeComboBox.SelectedIndex + 1).ToString());
            param.Add("beginTime", beginDateTimePicker.Value.ToString().Replace("/", "-"));
            param.Add("endTime", endDateTimePicker.Value.ToString().Replace("/", "-"));
            param.Add("appointPlatnumber", carnumberTextBox.Text);
            param.Add("driverName", driverNameTextBox.Text);
            param.Add("driverPhone", driverPhonetextBox.Text);
            param.Add("appointReason", reasonRichTextBox.Text);
            Thread threadStatus2 = new Thread(new ParameterizedThreadStart(beginLoad));
            threadStatus2.Start(param);

            formLoading = new FormLoading2();
            formLoading.StartPosition = FormStartPosition.CenterScreen;
            formLoading.FormBorderStyle = FormBorderStyle.None;
            formLoading.StartWaiting();
            formLoading.ShowDialog();


            //string serverIp = ConfigurationManager.AppSettings["serverIp"].ToString();


            //string strURL = serverIp + "/mobile/subscribeAdd";

            //Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add("appointMan", managerTextBox.Text);
            //param.Add("appointType", (typeComboBox.SelectedIndex + 1).ToString());
            //param.Add("beginTime", beginDateTimePicker.Value.ToString().Replace("/","-"));
            //param.Add("endTime", endDateTimePicker.Value.ToString().Replace("/", "-"));
            //param.Add("appointPlatnumber", carnumberTextBox.Text);
            //param.Add("driverName", driverNameTextBox.Text);
            //param.Add("driverPhone", driverPhonetextBox.Text);
            //param.Add("appointReason",reasonRichTextBox.Text);
            //string str = HttpHelper.HttpPost(strURL, param);
            //if (str.Length == 0)
            //{
            //    MessageBox.Show("系统异常，请稍后重试！");
            //}
            //else if (str == "accessToken")
            //{
            //    LoginForm loginForm = new LoginForm();
            //    loginForm.StartPosition = FormStartPosition.CenterScreen;
            //    loginForm.ShowDialog();
            //}
            //else
            //{
            //    JObject jsonObj = JObject.Parse(str);
            //    string body = jsonObj["body"].ToString();
            //    string head = jsonObj["head"].ToString();
            //    HeaderModel headerModel = new HeaderModel();
            //    headerModel = (HeaderModel)HttpHelper.JsonToObject(head, headerModel);
            //    if (Convert.ToInt16(headerModel.rspCode) == 0)
            //    {
            //        MessageBox.Show("预约成功！");
            //        this.Close();
            //    }
            //    else
            //    {
            //        MessageBox.Show(headerModel.rspMsg);
            //    }
            //}
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void endLoad(string str)
        {
            if (str.Length == 0)
            {
                MessageBox.Show("系统异常，请稍后重试！");
            }
            else if (str == "accessToken")
            {
                LoginForm loginForm = new LoginForm();
                loginForm.StartPosition = FormStartPosition.CenterScreen;
                loginForm.ShowDialog();
            }
            else
            {
                JObject jsonObj = JObject.Parse(str);
                string body = jsonObj["body"].ToString();
                string head = jsonObj["head"].ToString();
                HeaderModel headerModel = new HeaderModel();
                headerModel = (HeaderModel)HttpHelper.JsonToObject(head, headerModel);
                if (Convert.ToInt16(headerModel.rspCode) == 0)
                {
                    MessageBox.Show("预约成功！");
                    this.Close();
                }
                else
                {
                    MessageBox.Show(headerModel.rspMsg);
                }
            }
            formLoading.StopWaiting();
        }


        private void beginLoad(object param)
        {
            string serverIp = ConfigurationManager.AppSettings["serverIp"].ToString();


            string strURL = serverIp + "/mobile/subscribeAdd";

            //Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add("appointMan", appointMan);
            //param.Add("appointType", appointType);
            //param.Add("beginTime", beginTime);
            //param.Add("endTime", endTime);
            //param.Add("appointPlatnumber", appointPlatnumber);
            //param.Add("driverName", driverName);
            //param.Add("driverPhone", driverPhone);
            //param.Add("appointReason", appointReason);
            string str = HttpHelper.HttpPost(strURL, (Dictionary<string, object>)param);
            this.BeginInvoke(myMainUI, str);
            Application.DoEvents();
        }
    }
}
