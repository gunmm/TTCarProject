using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;




namespace TTCarProject
{
   
    public partial class LoginForm : Form
    {
        public delegate void InitMainUI(string str);
        InitMainUI myMainUI;
        FormLoading2 formLoading;
        Dictionary<string, User> usersAll;
        public bool isQiehuan = false;

        public LoginForm()
        {
            InitializeComponent();
           
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            myMainUI = new InitMainUI(endLoad);

            //  读取配置文件寻找记住的用户名和密码
            FileStream fs = new FileStream("D:\\data.bin", FileMode.OpenOrCreate);

            if (fs.Length > 0)
            {
                BinaryFormatter bf = new BinaryFormatter();
                Dictionary<string, User> users = bf.Deserialize(fs) as Dictionary<string, User>;
                usersAll = users;
                foreach (User user in users.Values)
                {
                    this.usernameComboBox.Items.Add(user.Username);
                }

                //  用户名默认选中第一个
                if (this.usernameComboBox.Items.Count > 0)
                {
                    this.usernameComboBox.SelectedIndex = this.usernameComboBox.Items.Count - 1;
                }

                for (int i = 0; i < users.Count; i++)
                {
                    if (this.usernameComboBox.Text != "")
                    {
                        if (users.ContainsKey(this.usernameComboBox.Text))
                        {
                            this.passwordTextBox.Text = users[this.usernameComboBox.Text].Password;
                            this.passwordcheckBox.Checked = true;
                        }
                    }
                }
            }
            fs.Close();

            if (this.passwordTextBox.Text.Length > 0) {
                if(!isQiehuan){
                    login();
                }

            }

           
        }

        private void login()
        {
            if (usernameComboBox.Text.Length == 0)
            {
                MessageBox.Show("用户名不能为空！");
                return;
            }

            if (passwordTextBox.Text.Length == 0)
            {
                MessageBox.Show("密码不能为空！");
                return;
            }


            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("tel", usernameComboBox.Text);
            param.Add("password", HttpHelper.GetMD5(passwordTextBox.Text));
            Thread threadStatus2 = new Thread(new ParameterizedThreadStart(beginLoad));
            threadStatus2.Start(param);

            formLoading = new FormLoading2();
            formLoading.StartPosition = FormStartPosition.CenterScreen;
            formLoading.FormBorderStyle = FormBorderStyle.None;
            formLoading.StartWaiting();
            formLoading.ShowDialog();         
        }



        private void setPassword()
        {
            string username = this.usernameComboBox.Text.Trim();
            string password = this.passwordTextBox.Text.Trim();

            User user = new User();
            FileStream fs = new FileStream("D:\\data.bin", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            user.Username = username;
            if (this.passwordcheckBox.Checked)       //  如果单击了记住密码的功能
            {   //  在文件中保存密码
                user.Password = password;
            }
            else
            {   //  不在文件中保存密码
                user.Password = "";
            }

            Dictionary<string, User> users;
            if (fs.Length > 0)
            {
                users = bf.Deserialize(fs) as Dictionary<string, User>;
            }
            else
            {
                users = new Dictionary<string, User>();
            }
            //  选在集合中是否存在用户名 
            if (users.ContainsKey(user.Username))
            {
                users.Remove(user.Username);
            }
            users.Add(user.Username, user);
            //要先将User类先设为可以序列化(即在类的前面加[Serializable])
            bf.Serialize(fs, users);
            //user.Password = this.PassWord.Text;
            fs.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (usernameComboBox.Text.Length == 0)
            {
                MessageBox.Show("用户名不能为空！");
                return;
            }

            if (passwordTextBox.Text.Length == 0)
            {
                MessageBox.Show("密码不能为空！");
                return;
            }

           
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("tel", usernameComboBox.Text);
            param.Add("password", HttpHelper.GetMD5(passwordTextBox.Text));
            Thread threadStatus2 = new Thread(new ParameterizedThreadStart(beginLoad));
            threadStatus2.Start(param);

            formLoading = new FormLoading2();
            formLoading.StartPosition = FormStartPosition.CenterScreen;
            formLoading.FormBorderStyle = FormBorderStyle.None;
            formLoading.StartWaiting();
            formLoading.ShowDialog();           

        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;
            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                Application.Exit();
                return;
            }
            base.WndProc(ref m);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请联系管理员重置密码");
        }

        private void endLoad(string str)
        {
            setPassword();
            if (str.Length == 0)
            {
                MessageBox.Show("系统异常，请稍后重试！");
            }
            else if (str == "accessToken")
            {
                MessageBox.Show("Token过期，请退出后重新登录！");
            }
            else
            {
                JObject jsonObj = JObject.Parse(str);
                string head = jsonObj["head"].ToString();
                HeaderModel headerModel = new HeaderModel();
                headerModel = (HeaderModel)HttpHelper.JsonToObject(head, headerModel);
                if (Convert.ToInt16(headerModel.rspCode) == 0)
                {
                    string body = jsonObj["body"].ToString();

                    UserModel userModel = new UserModel();
                    userModel = (UserModel)HttpHelper.JsonToObject(body, userModel);
                    MainForm.userId = userModel.userId;
                    MainForm.username = userModel.name;
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
            string strURL = serverIp + "/mobile/login";
            //Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add("tel", usernameTextBox.Text);
            //param.Add("password", HttpHelper.GetMD5(passwordTextBox.Text));

            string str = HttpHelper.HttpPostLogin(strURL, (Dictionary<string, object>)param);
            this.BeginInvoke(myMainUI, str);
            Application.DoEvents();
        }

        private void usernameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < usersAll.Count; i++)
            {
                if (this.usernameComboBox.Text != "")
                {
                    if (usersAll.ContainsKey(this.usernameComboBox.Text))
                    {
                        this.passwordTextBox.Text = usersAll[this.usernameComboBox.Text].Password;
                        this.passwordcheckBox.Checked = true;
                    }
                }
            }
        }

        private void usernameComboBox_TextUpdate(object sender, EventArgs e)
        {
            this.passwordTextBox.Text = "";
        }
    }
}
