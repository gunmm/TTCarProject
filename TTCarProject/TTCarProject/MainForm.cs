using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Configuration;
using Newtonsoft.Json.Linq;



namespace TTCarProject
{
    public partial class MainForm : Form
    {

        public static string username;
        public static string userId;
        public static string accessToken;

        public CLIENT_LPRC_ConnectCallback ConnectCallback = null;
        public CLIENT_LPRC_DataEx2Callback DataEx2Callback = null;
        public CLIENT_LPRC_JpegCallback JpegCallback = null;
        public bool running1 = false;
        static CLIENT_LPRC_PLATE_RESULTEX recRes1;
        IntPtr pIP1 = IntPtr.Zero;
        IntPtr pIP2 = IntPtr.Zero;
        CLIENT_LPRC_DEVDATA_INFO JpegInfo1;
        byte[] chJpegStream = new byte[NativeConstants.CLIENT_LPRC_BIG_PICSTREAM_SIZE_EX + 312];
        byte[] chJpegStream2 = new byte[NativeConstants.CLIENT_LPRC_BIG_PICSTREAM_SIZE_EX + 312]; //线程可以控制控件
        //------------------------------------------------------------------------------------------------------------------------------------------------

        Mutex mutex = new Mutex();
        Mutex mutexThread = new Mutex();
        public static Thread threadTrigger = null;
        public static Thread threadOpenGate = null;
        public static Thread threadStatus = null;
        public static Thread threadOpenGate2 = null;
        public static Thread threadRS485 = null;
        public static Thread threadRS232 = null;
        public MainForm()
        {
            InitializeComponent();
            this.pictureBox2.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseWheel);
        }

        private ipcsdk.ICE_IPCSDK_OnPlate onPlate;
        private ipcsdk.ICE_IPCSDK_OnFrame_Planar onFrame;
        private ipcsdk.ICE_IPCSDK_OnPastPlate onPastPlate;
        private ipcsdk.ICE_IPCSDK_OnSerialPort onSerialPort;
        private ipcsdk.ICE_IPCSDK_OnSerialPort_RS232 onSerialPortRS232;

        private IntPtr[] pUid = new IntPtr[4] { IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero };
        private int[] count = new int[4] { 0, 0, 0, 0 };

        private bool signView = false;
        private string lastPlateNumber = "";



        private StringBuilder[] strMac = new StringBuilder[4];
        private uint[] nRecvPortCount_RS232 = new uint[4] { 0, 0, 0, 0 };
        private uint[] nRecvPortCount_RS485 = new uint[4] { 0, 0, 0, 0 };
        private UInt32[] nGateNum = new UInt32[4] { 0, 0, 0, 0 };

        private UInt32[] nTriggerNum = new UInt32[4] { 0, 0, 0, 0 };
        private UInt32[] nCurrentStatus = new UInt32[4] { 0, 0, 0, 0 };
        private UInt32[] nStatus = new UInt32[4] { 0, 0, 0, 0 };

        private string[] strIp = new string[4];
        private bool[] bPreview = new bool[4] { true, true, true, true };

        //设置变量
        private string m_strStorePath = "D:\\";
        private int m_bOpenGate = 0;
        private int m_bTrigger = 0;
        private int m_bOpenGate2 = 0;
        private int m_bRS485 = 0;
        private int m_bRS232 = 0;

        private int m_nOpenInterval = 0;
        private int m_nTriggerInterval = 0;
        private int m_nOpenInterval2 = 0;
        private int m_nRS485Interval = 0;
        private int m_nRS232Interval = 0;
        private int m_nRecordInterval = 10;

        private string m_nVideoColor = "000000";
        private string m_nJpegColor = "000000";
        private string m_strLogPath = "D:\\";
        private int m_bEnableLog = 0;

        public ICE_OSDAttr_S osdInfo = new ICE_OSDAttr_S();
        private string[] strVehicleColor_old = new string[] { "未知", "红色", "绿色", "蓝色", "黄色", "白色", "灰色", "黑色", "紫色", "棕色", "粉色" };
        private string[] strVehicleColor = new string[] {
            "未知",
            "黑色",
            "蓝色",
            "灰色",
            "棕色",
            "绿色",
            "夜间深色",
            "紫色",
            "红色",
            "白色",
            "黄色" };
        private string[] strAlarmType = new string[]{
            "实时_硬触发+临时车辆",
            "实时_视频触发+临时车辆",
            "实时_软触发+临时车辆",
            "实时_硬触发+白名单",
            "实时_视频触发+白名单",
            "实时_软触发+白名单",
            "实时_硬触发+黑名单",
            "实时_视频触发+黑名单",
            "实时_软触发+黑名单",
            "脱机_硬触发+临时车辆",
            "脱机_视频触发+临时车辆",
            "脱机_软触发+临时车辆",
            "脱机_硬触发+白名单",
            "脱机_视频触发+白名单",
            "脱机_软触发+白名单",
            "脱机_硬触发+黑名单",
            "脱机_视频触发+黑名单",
            "脱机_软触发+黑名单",
            "实时_硬触发+过期白名单",
            "实时_视频触发+过期白名单",
            "实时_软触发+过期白名单",
            "脱机_硬触发+过期白名单",
            "脱机_视频触发+过期白名单",
            "脱机_软触发+过期白名单"
        };
        private string[] strVehicleType = new string[]
        {
            "未知",
            "普通汽车",
            "面包车",
            "大客车",
            "箱式货车",
            "大货车",
            "非机动车"
        };

        public ICE_VDC_PICTRUE_INFO_S vdcInfo = new ICE_VDC_PICTRUE_INFO_S();

        public ICE_VBR_RESULT_S vbrResult = new ICE_VBR_RESULT_S();

        public delegate void UpdatePlateInfo(string strIP, string strNum, string strColor, uint nVehicleColor,
           uint nAlarmType, short nVehiclType, uint nCapTime, int index, string strLogName);
        public UpdatePlateInfo updatePlateInfo;

        public delegate void UpdateStatus(int index, int type);
        public UpdateStatus updateStatus;
        public delegate void UpdateTriggerStatus(int index, uint nStatus);
        public UpdateTriggerStatus triggerStatus;

        public delegate void UpdatePortInfo(string strIp, uint len, int index, string data, int type);
        public UpdatePortInfo updatePortInfo;

        //海康威视
        private bool m_bInitSDK = false;
        private Int32 m_lUserID = -1;
        private uint iLastErr = 0;
        private string str;
        private Int32 m_lRealHandle = -1;
        private CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo;



        private void MainForm_Load(object sender, EventArgs e)
        {

          /**
            if (!DBHelper.judgeDataBase())
            {
                DBHelper.createDataBase();
                if (!DBHelper.judgeDataBase())
                {
                    MessageBox.Show("数据库创建失败！请手动创建或退出后重试！");
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("数据库创建成功！");
                    DBHelper.createTable();

                    if (DBHelper.judgeTable())
                    {
                        MessageBox.Show("表创建成功！");
                    }
                    else
                    {
                        MessageBox.Show("表创建失败！请手动创建或退出后重试！");
                        Application.Exit();
                    }

                }
            }
            else
            {
                if (!DBHelper.judgeTable())
                {
                    DBHelper.createTable();

                    if (DBHelper.judgeTable())
                    {
                        MessageBox.Show("表创建成功！");
                    }
                    else
                    {
                        MessageBox.Show("表创建失败！请手动创建或退出后重试！");
                        Application.Exit();
                    }
                }
            }*/

            //初始化View
            initView();

            //初始化SDK
            initSdk();

            //初始化出门SDK
            initOutSdk();

            //初始化摄像头SDK
            initHKWSSDK();

            //开启线程处理耗时操作
            initThreadHandleTime();

            initCar();

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ////添加过车记录
            insertToTable(0, "冀G50187", "2");
            insertToTable(0, "冀G50187", "1");

        }

        private void initCar()
        {
            //查询今日过车记录
            queryTodyRecord();

            //选择连接方式为：视频预览
            IntPtr videoHwnd = pictureBox1.Handle;
            if (videoHwnd != IntPtr.Zero)
            {
                //使用不带密码的接口连接相机
                pUid[0] = ipcsdk.ICE_IPCSDK_OpenPreview(ConfigurationManager.AppSettings["cameraIp"].ToString(), 1, 1, (uint)videoHwnd, onPlate, new IntPtr(0));
                if (pUid[0] == IntPtr.Zero)
                {
                    OutputPrintInfo("进门相机连接失败, 密码错误或者网络不好！");
                    return;
                }
                //}
            }
            else
            {
                OutputPrintInfo("未获得视频播放窗口");
                return;
            }
            buttonStartVideo1.Text = "结束视频";
            buttonStartVideo1.Enabled = true;



            //设置断网续传回调
            ipcsdk.ICE_IPCSDK_SetPastPlateCallBack(pUid[0], onPastPlate, new IntPtr(0));

            //设置RS485透明串口回调
            ipcsdk.ICE_IPCSDK_SetSerialPortCallBack(pUid[0], onSerialPort, new IntPtr(0));//485
            //设置RS232透明串口回调
            ipcsdk.ICE_IPCSDK_SetSerialPortCallBack_RS232(pUid[0], onSerialPortRS232, new IntPtr(0));//232


            //strIp[0] = textBoxIP.Text;
            buttonConnect.Enabled = false;
            button_openGate.Enabled = true;
            bPreview[0] = true;
            bClose = false;
        }

        private void initOutSdk()
        {
            NativeMethods.CLIENT_LPRC_QuitSDK();
            ConnectCallback = new CLIENT_LPRC_ConnectCallback(OnConnectCallback);   //连接状态的回调函数
            DataEx2Callback = new CLIENT_LPRC_DataEx2Callback(OnDataEx2Callback);  //识别结果回调函数
            JpegCallback = new CLIENT_LPRC_JpegCallback(OnJpegCallback);
            NativeMethods.CLIENT_LPRC_RegCLIENTConnEvent(ConnectCallback);
            NativeMethods.CLIENT_LPRC_RegDataEx2Event(DataEx2Callback);
            NativeMethods.CLIENT_LPRC_RegJpegEvent(JpegCallback);

            string outCameraIp = ConfigurationManager.AppSettings["outCameraIp"].ToString();

            //设备连接
            if (running1 == false)
            {
                if (NativeMethods.CLIENT_LPRC_InitSDK(8080, IntPtr.Zero, 0, Marshal.StringToHGlobalAnsi(outCameraIp), 1) != 0)
                {
                    OutputPrintInfo("出门相机连接失败, 密码错误或者网络不好！");
                }
                else
                {
                    running1 = true;
                }
            }
        }



        //连接状态回调函数
        public void OnConnectCallback(System.IntPtr chCLIENTIP, uint nStatus, uint dwUser)
        {
            if (nStatus == 0)
            {
                if (dwUser == 1)
                {
                    this.running1 = false;
                    OutputPrintInfo("出门相机连接异常, 正尝试重新初始化！");
                    int installCount = 0;
                    while (!running1)
                    {
                        installCount++;
                        OutputPrintInfo("正尝试重新初始化" + installCount + "次！");
                        initOutSdk();
                        Thread.Sleep(1000 * 15);
                    }
                }
               
            }

        }
        //识别结果回调函数
        private void OnDataEx2Callback(ref CLIENT_LPRC_PLATE_RESULTEX recResultEx, uint dwUser)
        {
            recRes1 = recResultEx;
            this.dealWithData(recRes1);///以后

        }

        //显示识别结果中的图片,车牌号码??
        private void dealWithData(CLIENT_LPRC_PLATE_RESULTEX recResultEx)
        {
            try
            {
                ////整车图片保存
                byte[] chJpegStream = new byte[NativeConstants.CLIENT_LPRC_BIG_PICSTREAM_SIZE_EX + 312];  //SaveToNativeFile
                Int32 nJpegStream = recResultEx.pFullImage.nLen;
                Array.Clear(chJpegStream, 0, chJpegStream.Length);
                Marshal.Copy(recResultEx.pFullImage.pBuffer, chJpegStream, 0, nJpegStream);
                //string strtocp = System.Text.Encoding.Default.GetString(chJpegStream);

                ////文件名称///
                string sFilePath = System.Environment.CurrentDirectory + "\\OutPhoto\\" + DateTime.Now.ToString("yyyyMMdd");
                string sFileName = recResultEx.chLicense + DateTime.Now.ToString("hhmmss") + ".jpg";
                ////存图片
                savethepic(chJpegStream, sFilePath, sFileName);
                
                string strcphm = recResultEx.chLicense;//获取车牌号码，
                if (strcphm == "" || strcphm == null)
                {
                    DBHelper.WriteLog("出门车牌识别失败");
                    return;
                }
                else
                {

                    if (pUid[0] != IntPtr.Zero)
                    {
                        ipcsdk.ICE_IPCSDK_OpenGate(pUid[0]);//开闸
                        OutputPrintInfo("出门车辆" + strcphm + "开闸成功！");
                    }
                    CheckForIllegalCrossThreadCalls = false;
                    this.outPlateLabel.Text = strcphm;
                    insertWhiteRecordToTable(strcphm, DateTime.Now, "1", "", "0", "2");
                    //insertToTable(0, strcphm, "2");
                }
            }
            catch (Exception ex)
            {
                DBHelper.WriteLog(ex.Message);
            }

        }

        public void savethepic(byte[] data, string strpath, string sFileName)
        {
            string sFileName1 = strpath + "\\" + sFileName; //文件的绝对路径
            if (!Directory.Exists(strpath))//验证路径是否存在
            {
                Directory.CreateDirectory(strpath);
                //不存在则创建
            }
            System.IO.File.WriteAllBytes(sFileName1, data);

        }


        //图像回调函数
        private void OnJpegCallback(ref CLIENT_LPRC_DEVDATA_INFO JpegInfo, uint dwUser)
        {
           

            if (running1 == true)//1号初始成功在运行
            {

                if (dwUser == 1)//是1号传回的
                {
                    JpegInfo1 = JpegInfo;
                    if (JpegInfo1.nLen > 0)
                    {
                        uint nJpegStream = JpegInfo1.nLen;
                        Array.Clear(chJpegStream, 0, chJpegStream.Length);
                        Marshal.Copy(JpegInfo1.pchBuf, chJpegStream, 0, (Int32)nJpegStream);
                        this.outVideoPictureBox.Image = Image.FromStream(new MemoryStream(chJpegStream));
                       
                    }
                }
            }
        }


        private void initHKWSSDK()
        {
            m_bInitSDK = CHCNetSDK.NET_DVR_Init();
            if (m_bInitSDK == false)
            {
                MessageBox.Show("NET_DVR_Init error!");
                return;
            }
            else
            {
                //保存SDK日志 To save the SDK log
                CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\SdkLog\\", true);
                HKSDKLogin();
            }
        }

        private void HKSDKLogin()
        {
            string DVRIPAddress = ConfigurationManager.AppSettings["HKIP"].ToString(); //设备IP地址或者域名
            Int16 DVRPortNumber = Int16.Parse(ConfigurationManager.AppSettings["HKPORT"].ToString());//设备服务端口号
            string DVRUserName = ConfigurationManager.AppSettings["HKUSERNAME"].ToString();//设备登录用户名
            string DVRPassword = ConfigurationManager.AppSettings["HKPASSWORD"].ToString();//设备登录密码

            DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();

            //登录设备 Login the device
            m_lUserID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
            if (m_lUserID < 0)
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "监控摄像头登陆失败！NET_DVR_Login_V30 failed, error code= " + iLastErr; //登录失败，输出错误号
                MessageBox.Show(str);
                return;
            }
            else
            {
                //登录成功
                //MessageBox.Show("Login Success!");
                OutputPrintInfo("监控摄像头登陆成功！");

                //预览
                HKPreview();
            }
        }

        private void HKPreview()
        {
            if (m_lUserID < 0)
            {
                MessageBox.Show("监控摄像头未登录！");
                return;
            }

            if (m_lRealHandle < 0)
            {
                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = pictureBox2.Handle;//预览窗口
                lpPreviewInfo.lChannel = Int16.Parse("1");//预te览的设备通道
                lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                lpPreviewInfo.dwDisplayBufNum = 15; //播放库播放缓冲区最大缓冲帧数

                CHCNetSDK.REALDATACALLBACK RealData = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
                IntPtr pUser = new IntPtr();//用户数据

                //打开预览 Start live view 
                m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
                if (m_lRealHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "监控摄像头预览失败！NET_DVR_RealPlay_V40 failed, error code= " + iLastErr; //预览失败，输出错误号
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    //预览成功
                    OutputPrintInfo("监控摄像头开始预览！");
                }
            }
        }

        public void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, ref byte pBuffer, UInt32 dwBufSize, IntPtr pUser)
        {
        }

        private void initThreadHandleTime()
        {
            //if (threadStatus != null)
            //{
            //    threadStatus.Abort();
            //    threadStatus = null;
            //}
            //threadStatus = new Thread(new ThreadStart(getStatus));//开启获取相机状态线程
            //threadStatus.Start();


            Thread threadStatus2 = new Thread(new ThreadStart(setTimer));//开启获取shijian线程
            threadStatus2.Start();

            Thread threadStatus3 = new Thread(new ThreadStart(beginSync));//开启初始同步数据线程
            threadStatus3.Start();
        }

        private void beginSync()
        {
            //初次拉取预约数据表信息到本地
            syncAppointTable();

            //拉取车辆表到本地
            syncCarTable();

            //初次上传过车记录表到云端
            syncRecordTable();
        }

        private void initSdk()
        {
            ipcsdk.ICE_IPCSDK_Init(); //调用全局初始化
            onFrame = new ipcsdk.ICE_IPCSDK_OnFrame_Planar(SDK_OnFrame);
            onPlate = new ipcsdk.ICE_IPCSDK_OnPlate(SDK_OnPlate);
            onPastPlate = new ipcsdk.ICE_IPCSDK_OnPastPlate(SDK_OnPastPlate);
            onSerialPort = new ipcsdk.ICE_IPCSDK_OnSerialPort(SDK_OnSerialPort);
            onSerialPortRS232 = new ipcsdk.ICE_IPCSDK_OnSerialPort_RS232(SDK_OnSerialPortRS232);


            updatePlateInfo = new UpdatePlateInfo(showCount);
            updateStatus = new UpdateStatus(showStatus);

            //获得设置
            if (File.Exists(@"./param.dat"))
            {
                FileStream fs = new FileStream("param.dat", FileMode.Open, FileAccess.Read);
                if (fs != null)
                {
                    try
                    {
                        BinaryReader br = new BinaryReader(fs);
                        if (br != null)
                        {
                            m_strStorePath = br.ReadString();
                            m_strLogPath = br.ReadString();
                            m_bEnableLog = br.ReadInt32();
                            m_bOpenGate = br.ReadInt32();
                            m_bTrigger = br.ReadInt32();
                            m_nOpenInterval = br.ReadInt32();
                            m_nTriggerInterval = br.ReadInt32();
                            m_nRecordInterval = br.ReadInt32();

                            osdInfo.u32OSDLocationVideo = (UInt32)br.ReadInt32();
                            m_nVideoColor = br.ReadString();
                            osdInfo.u32DateVideo = (UInt32)br.ReadInt32();
                            osdInfo.u32License = (UInt32)br.ReadInt32();
                            osdInfo.u32CustomVideo = (UInt32)br.ReadInt32();
                            osdInfo.szCustomVideo6 = br.ReadString();

                            osdInfo.u32OSDLocationJpeg = (UInt32)br.ReadInt32();
                            m_nJpegColor = br.ReadString();
                            osdInfo.u32DateJpeg = (UInt32)br.ReadInt32();
                            osdInfo.u32Algo = (UInt32)br.ReadInt32();
                            osdInfo.u32CustomJpeg = (UInt32)br.ReadInt32();
                            osdInfo.szCustomJpeg6 = br.ReadString();

                            m_bOpenGate2 = br.ReadInt32();
                            m_bRS485 = br.ReadInt32();
                            m_bRS232 = br.ReadInt32();
                            m_nOpenInterval2 = br.ReadInt32();
                            m_nRS485Interval = br.ReadInt32();
                            m_nRS232Interval = br.ReadInt32();


                            br.Close();
                        }
                        fs.Close();
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }

            if (m_strStorePath == "")
            {
                m_strStorePath = "D:\\";
            }
            if (m_strLogPath == "")
            {
                m_strStorePath = "D:\\";
            }

            string tmp = getColor16(m_nVideoColor);
            osdInfo.u32ColorVideo = Convert.ToUInt32(tmp, 16);
            tmp = "";
            tmp = getColor16(m_nJpegColor);
            osdInfo.u32CustomJpeg = Convert.ToUInt32(tmp, 16);
            loadConfig(m_strStorePath, m_bOpenGate, m_bTrigger, m_nOpenInterval,
                m_nTriggerInterval, m_nRecordInterval, m_bOpenGate2, m_bRS485, m_bRS232,
                m_nOpenInterval2, m_nRS485Interval, m_nRS232Interval);//设置图片保存路径，是否定时调用触发，定时打开道闸等，见函数实现中的说明
            ipcsdk.ICE_IPCSDK_LogConfig(m_bEnableLog, m_strLogPath);//日志配置
        }
        private void initView()
        {
            LoginForm loginForm = new LoginForm();
            loginForm.StartPosition = FormStartPosition.CenterScreen;
            loginForm.ShowDialog();
            //总宽度 439
            this.listView1.Columns.Add(" ", 30, HorizontalAlignment.Left);
            this.listView1.Columns.Add("时间", 130, HorizontalAlignment.Left);
            this.listView1.Columns.Add("车牌",93, HorizontalAlignment.Left);
            this.listView1.Columns.Add("类型", 93, HorizontalAlignment.Left);
            this.listView1.Columns.Add("抬杆方式", 93, HorizontalAlignment.Left);

           
            usernameLabel.Text = MainForm.username;

            OutputPrintInfo("登陆成功");
            OutputPrintInfo("用户名：" + MainForm.username);
            OutputPrintInfo("userId：" + MainForm.userId);
            OutputPrintInfo("accessToken：" + MainForm.accessToken);
        }
        private void setTimer()
        {
            System.Timers.Timer t = new System.Timers.Timer(1000 * 60);//实例化Timer类，设置间隔时间为1000毫秒 就是1秒；
            t.Elapsed += new System.Timers.ElapsedEventHandler(queryAct);//到达时间的时候执行事件；
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；

        }

        private void queryAct(object source, System.Timers.ElapsedEventArgs e)
        {
            //OutputPrintInfo("执行同步");
            //拉取预约数据表信息到本地
            syncAppointTable();

            //拉取车辆表到本地
            syncCarTable();

            //上传过车记录表到云端
            syncRecordTable();

            lastPlateNumber = "";

        }


        //同步过车数据
        private void syncRecordTable()
        {
            //查询本地未上传数据
            List<CarRecordModel> dataList = queryUnUpLoadRecord();

            //将本地未上传数据上传
            for (int i = 0; i < dataList.Count; i++)
            {
                CarRecordModel carRecordModel = dataList[i];
                uploadRecord(carRecordModel);
            }

        }

        private void uploadRecord(CarRecordModel model)
        {
            bool signSuccess = true;
            try
            {
                MySqlCommand mySqlCom = DBHelper.getMySqlCommand2();
                string sqlStr = string.Format("insert into vehiclego (id, plate_number, passtime, isWhite, userName, isOffLine, isUpLoaded, go_style, entrance_guard_position_id) values('"
                                                                               + model.cid + "', '"
                                                                               + model.strNum + "' , '"
                                                                               + model.nCapTime + "','"
                                                                               + model.isWhite + "' ,'"
                                                                               + model.manager + "' ,'"
                                                                               + model.isOffLine + "' ,'"
                                                                               + model.isUpLoaded + "' ,'"
                                                                               + model.goStyle + "' ,'"
                                                                               + model.entrance_guard_position_id + "')");

                mySqlCom.CommandText = sqlStr;
                mySqlCom.CommandType = CommandType.Text;
                mySqlCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                signSuccess = false;
                DBHelper.WriteLog(ex.ToString());
            }
            finally
            {
                DBHelper.getMySqlConnection2().Close();
                if (signSuccess)
                {
                    updateStatusForUpLoad(model.cid);

                }
            }
        }

        //更新上传过的记录的上传状态
        private void updateStatusForUpLoad(string cid)
        {
            try
            {
                MySqlCommand mySqlCom = DBHelper.getMySqlCommand();
                string sqlStr = string.Format("update vehiclego set isUpLoaded = '1' where id = '" + cid + "'");

                mySqlCom.CommandText = sqlStr;
                mySqlCom.CommandType = CommandType.Text;
                mySqlCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                DBHelper.WriteLog(ex.ToString());
            }
            finally
            {
                DBHelper.getMySqlConnection().Close();
            }
        }

        private List<CarRecordModel> queryUnUpLoadRecord()
        {
            List<CarRecordModel> dataList = new List<CarRecordModel>();
            try
            {
                //查询本地数据库中未上传的记录
                MySqlCommand mySqlCom = DBHelper.getMySqlCommand();
                string sqlStr = string.Format("select * from vehiclego where isUpLoaded = '0'");
                mySqlCom.CommandText = sqlStr;
                MySqlDataReader sdr = mySqlCom.ExecuteReader();
                while (sdr.Read())
                {
                    CarRecordModel carRecordModel = new CarRecordModel();
                    carRecordModel.cid = sdr["id"].ToString();
                    carRecordModel.strNum = sdr["plate_number"].ToString();
                    carRecordModel.nCapTime = (DateTime)sdr["passtime"];
                    carRecordModel.goStyle = sdr["go_style"].ToString();
                    carRecordModel.isWhite = sdr["isWhite"].ToString();
                    carRecordModel.manager = sdr["userName"].ToString();
                    carRecordModel.isOffLine = sdr["isOffLine"].ToString();
                    carRecordModel.isUpLoaded = sdr["isUpLoaded"].ToString();
                    carRecordModel.entrance_guard_position_id = sdr["entrance_guard_position_id"].ToString();

                    dataList.Add(carRecordModel);
                }

            }
            catch (Exception ex)
            {
                DBHelper.WriteLog(ex.ToString());
            }
            finally
            {
                DBHelper.getMySqlConnection().Close();
            }
            return dataList;
        }

        //同步车辆列表
        private void syncCarTable()
        {
            //查询当前本地数据库中最新的一条的appoint_time值
            DateTime? nowCarMaxTime = queryCarMaxTime();
            //根据appoint_time值判断本地数据库有无数据
            string sqlStrForQuery = "";
            if (nowCarMaxTime != null)
            {
                //有则从最大的之后开始查
                sqlStrForQuery = string.Format("select * from vehicle where update_time >'" + nowCarMaxTime + "'");
            }
            else
            {
                //没有则全查
                sqlStrForQuery = string.Format("select * from vehicle");
            }

            //拉回来的新数据 list
            List<CarModel> dataList = queryCarNewDownLoadData(sqlStrForQuery);

            //将拉回来的数据存入本地数据库
            for (int i = 0; i < dataList.Count; i++)
            {
                insertTheNewCarDataToLocal(dataList[i]);
            }
        }

        private void insertTheNewCarDataToLocal(CarModel model)
        {
            MySqlConnection mySqlConn = null;
            try
            {
                mySqlConn = new MySqlConnection(DBHelper.getSqlStr1());
                mySqlConn.Open();
                MySqlCommand mySqlCom = mySqlConn.CreateCommand();
                string sqlStr = string.Format("insert into vehicle (ID,plate_number,isDelete,update_time) values('" + model.ID + "', '"
                                                                               + model.plate_number + "' , '"
                                                                                + model.isDelete + "' , '"
                                                                               + model.update_time + "') ON DUPLICATE KEY UPDATE isDelete =" + model.isDelete);

                mySqlCom.CommandText = sqlStr;
                mySqlCom.CommandType = CommandType.Text;
                mySqlCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                DBHelper.WriteLog(ex.ToString());
            }
            finally
            {
                mySqlConn.Close();
            }
        }

        private List<CarModel> queryCarNewDownLoadData(string sqlStrForQuery)
        {
            List<CarModel> dataList = new List<CarModel>();
            MySqlConnection mySqlConn = null;
            try
            {
                mySqlConn = new MySqlConnection(DBHelper.getSqlStr2());
                mySqlConn.Open();
                MySqlCommand mySqlCom = mySqlConn.CreateCommand();

                mySqlCom.CommandText = sqlStrForQuery;
                MySqlDataReader sdr = mySqlCom.ExecuteReader();
                while (sdr.Read())
                {
                    CarModel carModel = new CarModel();
                    carModel.ID = sdr["ID"].ToString();
                    carModel.plate_number = sdr["plate_number"].ToString();
                    carModel.isDelete = (int)sdr["isDelete"];
                    string b = sdr["update_time"].ToString();

                    if (b.Length > 0)
                    {
                        carModel.update_time = (DateTime?)sdr["update_time"];
                        dataList.Add(carModel);
                    }
                                       
                }
            }
            catch (Exception ex)
            {
                DBHelper.WriteLog(ex.ToString());
            }
            finally
            {
                mySqlConn.Close();

            }
            return dataList;
        }

        private DateTime? queryCarMaxTime()
        {
            DateTime? nowCarMaxTime = null;
            MySqlConnection mySqlConn = null;
            try
            {
                mySqlConn = new MySqlConnection(DBHelper.getSqlStr1());
                mySqlConn.Open();
                MySqlCommand mySqlCom = mySqlConn.CreateCommand();
                string sqlStr = string.Format("select max(update_time) from vehicle");

                mySqlCom.CommandText = sqlStr;
                MySqlDataReader sdr = mySqlCom.ExecuteReader();
                while (sdr.Read())
                {
                    string b = sdr[0].ToString();
                    if (b.Length > 0)
                    {
                        nowCarMaxTime = (DateTime)sdr[0];
                    }

                }
            }
            catch (Exception ex)
            {
                DBHelper.WriteLog(ex.ToString());
            }
            finally
            {
                mySqlConn.Close();
            }
            return nowCarMaxTime;
        }

        //同步注册数据
        private void syncAppointTable()
        {
            //查询当前本地数据库中最新的一条的appoint_time值
            DateTime? nowMaxTime = queryMaxTime();

            //根据appoint_time值判断本地数据库有无数据
            string sqlStrForQuery = "";
            if (nowMaxTime != null)
            {
                //有则从最大的之后开始查
                sqlStrForQuery = string.Format("select * from subscribe where appoint_time >'" + nowMaxTime + "'");
            }
            else
            {
                //没有则全查
                sqlStrForQuery = string.Format("select * from subscribe");
            }


            //拉回来的新数据 list
            List<SubscribeModel> dataList = queryNewDownLoadData(sqlStrForQuery);

            //将拉回来的数据存入本地数据库
            for (int i = 0; i < dataList.Count; i++)
            {
                insertTheNewDataToLocal(dataList[i]);
            }


        }

        private DateTime? queryMaxTime()
        {
            DateTime? nowMaxTime = null;
            MySqlConnection mySqlConn = null;
            try
            {
                mySqlConn = new MySqlConnection(DBHelper.getSqlStr1());
                mySqlConn.Open();
                MySqlCommand mySqlCom = mySqlConn.CreateCommand();
                string sqlStr = string.Format("select max(appoint_time) from subscribe");

                mySqlCom.CommandText = sqlStr;
                MySqlDataReader sdr = mySqlCom.ExecuteReader();
                while (sdr.Read())
                {
                    string b = sdr[0].ToString();
                    if (b.Length > 0)
                    {
                        nowMaxTime = (DateTime)sdr[0];
                    }

                }
            }
            catch (Exception ex)
            {
                DBHelper.WriteLog(ex.ToString());
            }
            finally
            {
                mySqlConn.Close();
            }
            return nowMaxTime;
        }

        private void insertTheNewDataToLocal(SubscribeModel model)
        {
            MySqlConnection mySqlConn = null;
            try
            {
                mySqlConn = new MySqlConnection(DBHelper.getSqlStr1());
                mySqlConn.Open();
                MySqlCommand mySqlCom = mySqlConn.CreateCommand();
                string sqlStr = string.Format("insert into subscribe (id,appoint_man,appoint_time,appoint_type,begin_time,end_time,appoint_platnumber,appoint_reason,driver_name,isDelete,driver_phone) values('" + model.id + "', '"
                                                                               + model.appoint_man + "' , '"
                                                                               + model.appoint_time + "','"
                                                                               + model.appoint_type + "' ,'"
                                                                               + model.begin_time + "' ,'"
                                                                               + model.end_time + "' ,'"
                                                                               + model.appoint_platnumber + "' ,'"
                                                                               + model.appoint_reason + "' ,'"
                                                                               + model.driver_name + "' ,'"
                                                                               + model.isDelete + "' ,'"
                                                                               + model.driver_phone + "') ON DUPLICATE KEY UPDATE isDelete ="+model.isDelete);

               

                mySqlCom.CommandText = sqlStr;
                mySqlCom.CommandType = CommandType.Text;
                mySqlCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                DBHelper.WriteLog(ex.ToString());
            }
            finally
            {
                mySqlConn.Close();
            }
        }

        private List<SubscribeModel> queryNewDownLoadData(string sqlStrForQuery)
        {
            MySqlConnection mySqlConn = null;
            List<SubscribeModel> dataList = new List<SubscribeModel>();
            try
            {
                mySqlConn = new MySqlConnection(DBHelper.getSqlStr2());
                mySqlConn.Open();
                MySqlCommand mySqlCom = mySqlConn.CreateCommand();

                mySqlCom.CommandText = sqlStrForQuery;
                MySqlDataReader sdr = mySqlCom.ExecuteReader();
                while (sdr.Read())
                {
                    SubscribeModel subscribeModel = new SubscribeModel();
                    subscribeModel.id = sdr["id"].ToString();
                    subscribeModel.appoint_man = sdr["appoint_man"].ToString();
                    subscribeModel.appoint_time = (DateTime)sdr["appoint_time"];
                    subscribeModel.appoint_type = Convert.ToInt16(sdr["appoint_type"]);
                    subscribeModel.begin_time = (DateTime)sdr["begin_time"];
                    subscribeModel.end_time = (DateTime?)sdr["end_time"];
                    subscribeModel.appoint_platnumber = sdr["appoint_platnumber"].ToString();
                    subscribeModel.appoint_reason = sdr["appoint_reason"].ToString();
                    subscribeModel.driver_name = sdr["driver_name"].ToString();
                    subscribeModel.driver_phone = sdr["driver_phone"].ToString();
                    subscribeModel.isDelete = (int)sdr["isDelete"];


                    
                    dataList.Add(subscribeModel);
                }
            }
            catch (Exception ex)
            {
                DBHelper.WriteLog(ex.ToString());
            }
            finally
            {
                mySqlConn.Close();

            }
            return dataList;
        }








        //获取连接状态线程
        private void getStatus()
        {
            while (true)
            {
                Thread.Sleep(1000);
                for (int i = 0; i < 4; i++)
                {
                    if (pUid[i] != IntPtr.Zero || bClose)
                    {
                        //mutexThread.WaitOne();
                        nCurrentStatus[i] = ipcsdk.ICE_IPCSDK_GetStatus(pUid[i]);//获取连接状态
                        //mutexThread.ReleaseMutex();
                        if (nCurrentStatus[i] != nStatus[i] && pUid[i] != IntPtr.Zero)
                        {
                            //mutexThread.WaitOne();
                            ipcsdk.ICE_IPCSDK_GetDevID(pUid[i], strMac[i]);//获取相机mac地址
                            //mutexThread.ReleaseMutex();
                            nStatus[i] = nCurrentStatus[i];
                            IAsyncResult syncResult = this.BeginInvoke(updateStatus, i, 0);//委托，显示连接信息


                        }
                    }
                }
            }
        }

        //通过从配置文件中读取出来的参数，进行相关操作
        private void loadConfig(string strPath, int nOpenGate, int nTrigger, int nOpenGateInterval, int nTriggerInterval, int nRecordInterval,
            int nOpenGate2, int nRS485, int nRS232, int nOpenGate2Interval, int nRS485Interval, int nRS232Interval)
        {
            //mutexMsg.WaitOne();
            m_strStorePath = strPath;//图片保存路径
            m_bOpenGate = nOpenGate;
            m_bTrigger = nTrigger;
            m_nOpenInterval = nOpenGateInterval;
            m_nTriggerInterval = nTriggerInterval;
            m_nRecordInterval = nRecordInterval;

            m_bOpenGate2 = nOpenGate2;
            m_bRS485 = nRS485;
            m_bRS232 = nRS232;
            m_nOpenInterval2 = nOpenGate2Interval;
            m_nRS485Interval = nRS485Interval;
            m_nRS232Interval = nRS232Interval;

            if (m_strStorePath == "")
            {
                m_strStorePath = "D:\\";
            }
        }


        //得到的RGB颜色值格式为0xrrggbb，要转成0xbbggrr格式
        private string getColor16(string color)
        {
            string realColor = "";
            realColor = color[4].ToString() + color[5].ToString() + color[2].ToString() + color[3].ToString() + color[0].ToString() + color[1].ToString();
            return realColor;
        }

        //将收到的实时识别数据和断网识别数据显示在界面上
        public void showCount(string strIP, string strNum, string strColor, uint nVehicleColor,
            uint nAlarmType, short nVehiclType, uint nCapTime, int index, string strLogName)
        {
            if (strNum == lastPlateNumber)
            {
                return;
            }
            lastPlateNumber = strNum;
            insertToTable(nCapTime, strNum, "1");
         }

         private void uploadNowPlat(object strNum)
        {

            string serverIp = ConfigurationManager.AppSettings["serverIp"].ToString();


            string strURL = serverIp + "/mobile/updateTransportByPlatNum";

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("platNum", strNum);

            string str = HttpHelper.HttpPost(strURL, param);
            if (str == "accessToken")
            {
                LoginForm loginForm = new LoginForm();
                loginForm.StartPosition = FormStartPosition.CenterScreen;
                loginForm.ShowDialog();
            }

        }

        public void insertToTable(uint nCapTime, string strNum, string insertType)
        {
            if (insertType == "1")
            {
                labelPlate.Text = strNum;
            }
            else if (insertType == "2")
            {
                this.outPlateLabel.Text = strNum;
            }

            string resultNum = "";
            MySqlConnection mySqlConn = null;

            try
            {
                DateTime d = DateTime.Now;
                mySqlConn = new MySqlConnection(DBHelper.getSqlStr1());
                mySqlConn.Open();
                MySqlCommand mySqlCom = mySqlConn.CreateCommand();
                string sqlStr = string.Format("select plate_number from vehicle where plate_number = '" + strNum + "' and isDelete = 0");


                mySqlCom.CommandText = sqlStr;
                MySqlDataReader sdr = mySqlCom.ExecuteReader();
                while (sdr.Read())
                {
                    resultNum = sdr["plate_number"].ToString();
                }
            }
            catch (Exception ex)
            {
                DBHelper.WriteLog(ex.ToString());
            }
            finally
            {
                mySqlConn.Close();
            }

            if(resultNum.Length == 0){
                try
                {
                    //查询是否已经注册
                    DateTime d = DateTime.Now;
                    MySqlCommand mySqlCom = DBHelper.getMySqlCommand3();
                    string sqlStr = string.Format("select * from subscribe where appoint_platnumber = '" + strNum + "' and('" + d + "' BETWEEN begin_time AND end_time) and isDelete = 0");


                    mySqlCom.CommandText = sqlStr;
                    MySqlDataReader sdr = mySqlCom.ExecuteReader();
                    while (sdr.Read())
                    {
                        resultNum = sdr["appoint_platnumber"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    DBHelper.WriteLog(ex.ToString());
                }
                finally
                {
                    DBHelper.getMySqlConnection3().Close();
                }
            }

            if (nCapTime == 0)//实时数据
            {
                if (resultNum == strNum)//已经注册
                {
                    DateTime d = DateTime.Now;
                    //string dStr = d.ToString("yyyy-MM-dd HH:mm:ss");
                   
                    if (pUid[0] != IntPtr.Zero)
                    {
                        ipcsdk.ICE_IPCSDK_OpenGate(pUid[0]);//开闸
                        OutputPrintInfo("已注册车辆" + strNum + "开闸成功！");
                    }
                    insertWhiteRecordToTable(strNum, DateTime.Now, "1", "", "0", insertType);

                }
                else //未注册
                {
                    if (signView)
                    {
                        return;
                    }
                    signView = true;
                    //弹框确认 手动抬杆
                    UnRegisterConfirmForm unRegisterConfirm = new UnRegisterConfirmForm();
                    unRegisterConfirm.strNum = strNum;
                    unRegisterConfirm.typeStr = insertType;
                    unRegisterConfirm.confirmManName = MainForm.username;
                    unRegisterConfirm.MyEvent += new MyDelegate(unRegisterCarBackAct);
                    unRegisterConfirm.MyEvent2 += new MyDelegate2(unRegisterCarBackAct2);

                    unRegisterConfirm.StartPosition = FormStartPosition.WindowsDefaultLocation;
                    unRegisterConfirm.Show();

                }
            }
            else //离线上传数据 记录存到数据库 并且标注是离线数据
            {
                //直接将数据入库
                DateTime beginAt = new DateTime(1970, 1, 1, 0, 0, 0);
                beginAt = beginAt.AddSeconds(nCapTime);
                if (resultNum == strNum)//已经注册
                {
                    insertWhiteRecordToTable(strNum, beginAt, "1", "", "1", insertType);
                }
                else //未注册
                {
                    insertWhiteRecordToTable(strNum, beginAt, "2", "", "1", insertType);
                }
            }


        }

        public void insertWhiteRecordToTable(string strNum, DateTime nCapTime, string goStyle, string manager, string isOffLine, string isWhile)
        {
            //Thread threadStatus2 = new Thread(new ParameterizedThreadStart(uploadNowPlat));

            //threadStatus2.Start(strNum);

            //存储过车数据
            string CId = Guid.NewGuid().ToString(); ;
            if (CId == null)
            {
                MessageBox.Show("获取CID失败！");
            }
            else
            {
                MySqlConnection mySqlConn = null;
                try
                {
                    mySqlConn = new MySqlConnection(DBHelper.getSqlStr1());
                    mySqlConn.Open();
                    MySqlCommand mySqlCom = mySqlConn.CreateCommand();


                    string sqlStr = string.Format("insert into vehiclego (id, plate_number, passtime, go_style, userName, isOffLine, isWhite, isUpLoaded, entrance_guard_position_id) values('"
                                                                              + CId + "', '"
                                                                              + strNum + "' , '"
                                                                              + nCapTime + "','"
                                                                              + goStyle + "' ,'"
                                                                              + manager + "' ,'"
                                                                              + isOffLine + "' ,'"
                                                                              + isWhile + "' ,'"
                                                                              + "0" + "' ,'"
                                                                              + ConfigurationManager.AppSettings["camera"].ToString() + "')");

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.CommandType = CommandType.Text;
                    mySqlCom.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    DBHelper.WriteLog(ex.ToString());
                }
                finally
                {
                    mySqlConn.Close();
                    queryTodyRecord();
                }
            }
        }

        //未注册车辆确认后的回调事件
        public void unRegisterCarBackAct(string strNum, string confirmMan, string typeStr)
        {
            signView = false;

            if (pUid[0] != IntPtr.Zero)
            {
                ipcsdk.ICE_IPCSDK_OpenGate(pUid[0]);//开闸
                OutputPrintInfo("未注册车辆" + strNum + "开闸成功！开闸人：" + confirmMan);
            }
            insertWhiteRecordToTable(strNum, DateTime.Now, "2", confirmMan, "0", typeStr);
        }

        public void unRegisterCarBackAct2(string strNum, string confirmMan, string typeStr)
        {
            signView = false;

        }


        //显示相机连接状态， 打开道闸2成功，发送RS485、RS232数据成功
        public void showStatus(int index, int type)
        {
            switch (type)
            {
                case 0://连接状态
                    if (nStatus[index] == 1)
                    {
                        labelStatus.Text = "在线 " + strMac[index];
                    }
                    else
                        labelStatus.Text = "离线 " + strMac[index];
                    break;
            }
        }

        private bool m_bExit = false;
        private bool bClose = false;

        public void SDK_OnFrame(System.IntPtr pvParam, uint u32Timestamp, System.IntPtr pu8DataY,
           System.IntPtr pu8DataU, System.IntPtr pu8DataV, int s32LinesizeY, int s32LinesizeU,
           int s32LinesizeV, int s32Width, int s32Height)
        {
            int index = (int)pvParam;
            if (m_bExit || bClose)
                return;
            on_frame(u32Timestamp, pu8DataY, pu8DataU, pu8DataV, s32LinesizeY,
                s32LinesizeU, s32LinesizeV, s32Width, s32Height, index);
        }

        private int[] frame_count = new int[4] { 0, 0, 0, 0 };
        public void on_frame(UInt32 u32Timestamp,
            IntPtr pu8DataY, IntPtr pu8DataU, IntPtr pu8DataV,
            Int32 s32LinesizeY, Int32 s32LinesizeU, Int32 s32LinesizeV,
            Int32 s32Width, Int32 s32Height, Int32 i)
        {
            if (m_bExit)
                return;

            mutex.WaitOne();
            string strDir = m_strStorePath + @"抓拍_C#Frame\" + ConfigurationManager.AppSettings["cameraIp"].ToString() + @"\" + DateTime.Now.ToString("yyyyMMdd");
            if (!Directory.Exists(strDir))
            {
                Directory.CreateDirectory(strDir);
            }

            string strPicName = strDir + @"\" + "test.bmp";

            if (0 == (frame_count[i] % 30))
            {

                try
                {
                    //拷贝数据
                    byte[] datay = new byte[s32Width * s32Height];
                    for (int j = 0; j < s32Height; j++)
                        Marshal.Copy((IntPtr)pu8DataY + j * s32LinesizeY, datay, j * s32Width, s32Width);

                    byte[] datau = new byte[s32Width * s32Height / 4];
                    for (int j = 0; j < s32Height / 2; j++)
                        Marshal.Copy((IntPtr)pu8DataU + j * s32LinesizeU, datau, j * s32Width / 2, s32Width / 2);

                    byte[] datav = new byte[s32Width * s32Height / 4];
                    for (int j = 0; j < s32Height / 2; j++)
                        Marshal.Copy((IntPtr)pu8DataV + j * s32LinesizeV, datav, j * s32Width / 2, s32Width / 2);

                    byte[] rgb24 = new byte[s32Width * s32Height * 3];

                    util.Convert(s32Width, s32Height, datay, datau, datav, ref rgb24);
                    //存图
                    FileStream fs = new FileStream(strPicName, FileMode.Create, FileAccess.Write);
                    BinaryWriter bw = new BinaryWriter(fs);

                    bw.Write('B');
                    bw.Write('M');
                    bw.Write(rgb24.Length + 54);
                    bw.Write(0);
                    bw.Write(54);
                    bw.Write(40);
                    bw.Write(s32Width);
                    bw.Write(s32Height);
                    bw.Write((ushort)1);
                    bw.Write((ushort)24);
                    bw.Write(0);
                    bw.Write(rgb24.Length);
                    bw.Write(0);
                    bw.Write(0);
                    bw.Write(0);
                    bw.Write(0);

                    bw.Write(rgb24, 0, rgb24.Length);
                    bw.Close();
                    fs.Close();

                }
                catch (System.Exception ex)
                {
                    //MessageBox.Show("frame" + ex.Message);
                }

                frame_count[i] = 0;
            }
            frame_count[i]++;
            mutex.ReleaseMutex();
        }


        //实时抓拍
        public void SDK_OnPlate(System.IntPtr pvParam,
                    [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string pcIP,
                    [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string pcNumber,
                    [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string pcColor,
                    System.IntPtr pcPicData, uint u32PicLen, System.IntPtr pcCloseUpPicData, uint u32CloseUpPicLen,
                    short nSpeed, short nVehicleType, short nReserved1, short nReserved2,
                    float fPlateConfidence, uint u32VehicleColor, uint u32PlateType, uint u32VehicleDir, uint u32AlarmType,
                    uint u32SerialNum, uint uCapTime, uint u32ResultHigh, uint u32ResultLow)
        {
            int index = (int)pvParam;
            if (m_bExit || bClose)
                return;
            on_plate(pcIP, pcNumber, pcColor, pcPicData, u32PicLen, pcCloseUpPicData, u32CloseUpPicLen,
                nSpeed, nVehicleType, nReserved1, nReserved2, fPlateConfidence,
                u32VehicleColor, u32PlateType, u32VehicleDir, u32AlarmType, 0, index, u32ResultHigh, u32ResultLow);
        }


        public void on_plate(string bstrIP, string bstrNumber, string bstrColor, IntPtr vPicData, UInt32 nPicLen,
          IntPtr vCloseUpPicData, UInt32 nCloseUpPicLen, short nSpeed, short nVehicleType, short nReserved1, short nReserved2, Single fPlateConfidence,
          UInt32 nVehicleColor, UInt32 nPlateType, UInt32 nVehicleDir, UInt32 nAlarmType, UInt32 nCapTime, Int32 index, uint u32ResultHigh, uint u32ResultLow)
        {
            if (m_bExit)
                return;
#if VERSION32
            IntPtr vdcPtr = (IntPtr)u32ResultLow;
#else
            ulong tmp = ((ulong)u32ResultHigh << 32) + (ulong)u32ResultLow;
            IntPtr vdcPtr = (IntPtr)tmp;
#endif
            if (vdcPtr != IntPtr.Zero)
            {
                //将数据拷贝到ICE_VDC_PICTRUE_INFO_S结构体
                vdcInfo = (ICE_VDC_PICTRUE_INFO_S)Marshal.PtrToStructure(vdcPtr, typeof(ICE_VDC_PICTRUE_INFO_S));

                //获得车款结构体指针，并拷贝
                if (vdcInfo.pstVbrResult != IntPtr.Zero)
                {
                    vbrResult = (ICE_VBR_RESULT_S)Marshal.PtrToStructure(vdcInfo.pstVbrResult, typeof(ICE_VBR_RESULT_S));
                    if (vbrResult.szLogName.Length == 0)
                        vbrResult.szLogName = "未知";
                    //委托，用于显示识别数据(showCount)
                    this.BeginInvoke(updatePlateInfo, bstrIP, bstrNumber, bstrColor,
                        nVehicleColor, nAlarmType, nVehicleType, nCapTime, index, vbrResult.szLogName);
                }
                else
                    this.BeginInvoke(updatePlateInfo, bstrIP, bstrNumber, bstrColor,
                        nVehicleColor, nAlarmType, nVehicleType, nCapTime, index, "");//委托，用于显示识别数据(showCount)
            }
            else
                this.BeginInvoke(updatePlateInfo, bstrIP, bstrNumber, bstrColor,
                    nVehicleColor, nAlarmType, nVehicleType, nCapTime, index, "");//委托，用于显示识别数据(showCount)

            if (nPicLen > 0)//全景图数据长度不为0
            {
                IntPtr ptr2 = (IntPtr)vPicData;
                byte[] datajpg2 = new byte[nPicLen];
                Marshal.Copy(ptr2, datajpg2, 0, datajpg2.Length);//拷贝图片数据
                //存图
                if (vdcInfo.pstVbrResult != IntPtr.Zero)
                    storePic(datajpg2, bstrIP, bstrNumber, false, nCapTime, vbrResult.fResFeature, vbrResult.szLogName);
                else
                    storePic(datajpg2, bstrIP, bstrNumber, false, nCapTime, null, "");
            }


            if (nCloseUpPicLen > 0)//车牌图数据长度不为0
            {
                IntPtr ptr = (IntPtr)vCloseUpPicData;
                byte[] datajpg = new byte[nCloseUpPicLen];
                Marshal.Copy(ptr, datajpg, 0, datajpg.Length);//拷贝图片数据
                //存图
                if (vdcInfo.pstVbrResult != IntPtr.Zero)
                    storePic(datajpg, bstrIP, bstrNumber, true, nCapTime, null, vbrResult.szLogName);
                else
                    storePic(datajpg, bstrIP, bstrNumber, true, nCapTime, null, "");
            }
        }

        //存图
        public void storePic(byte[] picData, string strIP, string strNumber, bool bIsPlate, UInt32 nCapTime, float[] fResFuture, string strLogName)
        {
            DateTime dt = new DateTime();
            if (nCapTime == 0)
            {
                dt = DateTime.Now;
            }
            else
            {
                dt = DateTime.Parse("1970-01-01 08:00:00").AddSeconds(nCapTime);
            }

            string strDir = m_strStorePath + @"抓拍_C#\" + strIP + @"\" + dt.ToString("yyyyMMdd");
            if (!Directory.Exists(strDir))
            {
                Directory.CreateDirectory(strDir);
            }

            string strPicName;
            if (strLogName.Length != 0)
                strPicName = strDir + @"\" + dt.ToString("yyyyMMddHHmmss") + "_" + strLogName + "_" + strNumber;
            else
                strPicName = strDir + @"\" + dt.ToString("yyyyMMddHHmmss") + "_" + strNumber;
            if (bIsPlate)//车牌图，图片名后缀加_plate
                strPicName += "_plate";
            //string tmp = strPicName;
            strPicName += ".jpg";
            if (File.Exists(strPicName))//如果图片名存在，则在文件名末尾加数字以分辨，如XXX_1.jpg;XXX_2.jpg
            {
                int count = 1;
                while (count <= 10)
                {
                    strPicName = strDir + @"\" + dt.ToString("yyyyMMddHHmmss") + "_" + strNumber;
                    if (bIsPlate)
                    {
                        strPicName += "_plate";
                    }
                    strPicName += "_" + count.ToString() + ".jpg";

                    if (!File.Exists(strPicName))
                    {
                        break;
                    }
                    count++;
                }
            }
            //存图
            try
            {
                FileStream fs = new FileStream(strPicName, FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(picData);
                bw.Close();
                fs.Close();
            }
            catch (System.Exception ex)
            {

            }

            //保存特征码
            if (bIsPlate || null == fResFuture)
                return;

            string strFileName = strDir + @"\" + "vbr_record.txt";

            string strContent = "";
            //将车辆特征码存到字符串中
            for (int i = 0; i < 20; i++)
            {
                if (i != 0)
                    strContent += " ";
                strContent += fResFuture[i].ToString("0.000000");
            }
            //将车辆特征码数据追加到文件中
            try
            {
                StreamWriter sw = new StreamWriter(strFileName, true, Encoding.Unicode);
                if (null != sw)
                {
                    strContent = dt.ToString("yyyyMMddHHmmss") + "_" + strLogName + "_" + strNumber + ".jpg" + " " + strNumber + " " + strContent + "\r\n";
                    sw.Write(strContent);
                    sw.Close();
                }
            }
            catch (System.Exception ex)
            {

            }

        }

        //断网续传
        public void SDK_OnPastPlate(System.IntPtr pvParam,
            [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string pcIP,
            uint u32CapTime,
            [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string pcNumber,
            [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string pcColor,
            System.IntPtr pcPicData, uint u32PicLen, System.IntPtr pcCloseUpPicData, uint u32CloseUpPicLen, short s16PlatePosLeft,
            short s16PlatePosTop, short s16PlatePosRight, short s16PlatePosBottom, float fPlateConfidence, uint u32VehicleColor,
            uint u32PlateType, uint u32VehicleDir, uint u32AlarmType, uint u32Reserved1, uint u32Reserved2, uint u32Reserved3,
            uint u32Reserved4)
        {
            int index = (int)pvParam;
            if (m_bExit || bClose)
                return;
            on_plate(pcIP, pcNumber, pcColor, pcPicData, u32PicLen, pcCloseUpPicData, u32CloseUpPicLen,
                s16PlatePosLeft, s16PlatePosTop, s16PlatePosRight, s16PlatePosBottom, fPlateConfidence,
                u32VehicleColor, u32PlateType, u32VehicleDir, u32AlarmType, u32CapTime, index, u32Reserved2, u32Reserved3);
        }

        public void SDK_OnSerialPort(System.IntPtr pvParam,
           [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string pcIP,
           System.IntPtr pcData, uint u32Len)
        {
            if (m_bExit)
                return;

            int index = (int)pvParam;
            IntPtr tmp = pcData;
            byte[] dataPort2 = new byte[u32Len];
            Marshal.Copy(tmp, dataPort2, 0, dataPort2.Length);//拷贝串口数据
            string strPort = BitConverter.ToString(dataPort2);
            strPort = strPort.Replace("-", " ");
            //委托，用于在界面上显示收到的串口数据
            IAsyncResult syncResult = this.BeginInvoke(updatePortInfo, pcIP, u32Len, index, strPort, 0);
        }

        public void SDK_OnSerialPortRS232(System.IntPtr pvParam,
            [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string pcIP,
            System.IntPtr pcData, uint u32Len)
        {
            if (m_bExit)
                return;
            int index = (int)pvParam;
            IntPtr tmp = pcData;
            byte[] dataPort2 = new byte[u32Len];
            Marshal.Copy(tmp, dataPort2, 0, dataPort2.Length);//拷贝串口数据
            string strPort = BitConverter.ToString(dataPort2);
            strPort = strPort.Replace("-", " ");
            //委托，用于在界面上显示收到的串口数据
            IAsyncResult syncResult = this.BeginInvoke(updatePortInfo, pcIP, u32Len, index, strPort, 1);
        }

        private void queryTodyRecord()
        {
            listView1.Items.Clear();
            MySqlConnection mySqlConn = null;
            try
            {
                List<CarRecordModel> dataList = new List<CarRecordModel>();
                DateTime d = DateTime.Now;
                string dStr = d.ToString("yyyy-MM-dd");
                //查询今日过车记录
                mySqlConn = new MySqlConnection(DBHelper.getSqlStr1());
                mySqlConn.Open();
                MySqlCommand mySqlCom = mySqlConn.CreateCommand();
                string sqlStr = string.Format("select * from vehiclego where passtime like '%" + dStr + "%' order by passtime ASC");
                mySqlCom.CommandText = sqlStr;
                MySqlDataReader sdr = mySqlCom.ExecuteReader();
                while (sdr.Read())
                {
                    CarRecordModel carRecordModel = new CarRecordModel();
                    carRecordModel.cid = sdr["id"].ToString();
                    carRecordModel.strNum = sdr["plate_number"].ToString();
                    carRecordModel.nCapTime = (DateTime)sdr["passtime"];
                    carRecordModel.goStyle = sdr["go_style"].ToString();
                    carRecordModel.isWhite = sdr["isWhite"].ToString();
                    dataList.Add(carRecordModel);
                }
                for (int i = dataList.Count() - 1; i >= 0; i--)
                {
                    CarRecordModel carRecordModel = dataList[i];
                    listView1.Items.Add((i+1).ToString());
                    listView1.Items[dataList.Count() - 1 - i].SubItems.Add(carRecordModel.nCapTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    listView1.Items[dataList.Count() - 1 - i].SubItems.Add(carRecordModel.strNum);
                    listView1.Items[dataList.Count() - 1 - i].SubItems.Add((carRecordModel.isWhite == "1") ? "进门" : "出门");
                    listView1.Items[dataList.Count() - 1 - i].SubItems.Add((carRecordModel.goStyle == "1") ? "自动" : "手动");

                }
            }
            catch (Exception ex)
            {
                DBHelper.WriteLog(ex.ToString());
            }
            finally
            {
                mySqlConn.Close();
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {

        }

       

        private void button_openGate_Click(object sender, EventArgs e)
        {
            if (pUid[0] != IntPtr.Zero)
            {
                uint a = ipcsdk.ICE_IPCSDK_OpenGate(pUid[0]);//开闸
                ipcsdk.ICE_IPCSDK_ControlAlarmOut(pUid[0], 0);
            }
        }

        private void buttonStartVideo1_Click(object sender, EventArgs e)
        {
            if (pUid[0] == IntPtr.Zero)
                return;

            if (0 == ipcsdk.ICE_IPCSDK_GetStatus(pUid[0]))//获取相机连接状态
                return;

            if (!bPreview[0])//开始视频
            {
                IntPtr videoHwnd = pictureBox1.Handle;
                if (videoHwnd != IntPtr.Zero)
                {
                    UInt32 ret = ipcsdk.ICE_IPCSDK_StartStream(pUid[0], 1, (UInt32)videoHwnd);//开始视频
                }
                //buttonRecord1.Enabled = true;
                buttonStartVideo1.Text = "结束视频";
                bPreview[0] = true;
            }
            else//关闭视频
            {
                ipcsdk.ICE_IPCSDK_StopStream(pUid[0]);//关闭视频
                buttonStartVideo1.Text = "开始视频";
                bPreview[0] = false;
                //buttonRecord1.Enabled = false;
            }
        }

        private void OutputPrintInfo(string msg)
        {
            Action act = () =>
            {
                try
                {
                    string strAddDsp = "[" + DateTime.Now.ToString() + "] " + msg + "\n";
                    m_richTextPrintInfo.Focus();
                    m_richTextPrintInfo.Text += strAddDsp;
                    m_richTextPrintInfo.SelectionStart = m_richTextPrintInfo.Text.Length - strAddDsp.Length;
                    m_richTextPrintInfo.SelectionLength = strAddDsp.Length;
                    m_richTextPrintInfo.SelectionColor = Color.Blue;
                }
                catch (System.Exception ex)
                {

                }

            };

            if (m_richTextPrintInfo.InvokeRequired)
            {
                m_richTextPrintInfo.Invoke(act);
            }
            else
            {
                act();
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.isQiehuan = true;
            loginForm.StartPosition = FormStartPosition.CenterScreen;
            loginForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //弹框确认 手动抬杆
            AddAppointForm addAppointForm = new AddAppointForm();
            addAppointForm.StartPosition = FormStartPosition.CenterScreen;
            addAppointForm.ShowDialog();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            upbutton.Visible = true;
            downbutton.Visible = true;
            leftbutton.Visible = true;
            rightbutton.Visible = true;
            rightupbutton.Visible = true;
            rightdownbutton.Visible = true;
            leftupbutton.Visible = true;
            leftdownbutton.Visible = true;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            upbutton.Visible = false;
            downbutton.Visible = false;
            leftbutton.Visible = false;
            rightbutton.Visible = false;
            rightupbutton.Visible = false;
            rightdownbutton.Visible = false;
            leftupbutton.Visible = false;
            leftdownbutton.Visible = false;
        }

        private void leftupbutton_MouseDown(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.UP_LEFT, 0, 4 + 1);
        }

        private void leftupbutton_MouseUp(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.UP_LEFT, 1, 4 + 1);
        }

        private void upbutton_MouseDown(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_UP, 0, 4 + 1);
        }

        private void upbutton_MouseUp(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_UP, 1, 4 + 1);
        }

        private void rightupbutton_MouseDown(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.UP_RIGHT, 0, 4 + 1);
        }

        private void rightupbutton_MouseUp(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.UP_RIGHT, 1, 4 + 1);
        }

        private void rightbutton_MouseDown(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_RIGHT, 0, 4 + 1);
        }

        private void rightbutton_MouseUp(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_RIGHT, 1, 4 + 1);
        }

        private void rightdownbutton_MouseDown(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.DOWN_RIGHT, 0, 4 + 1);
        }

        private void rightdownbutton_MouseUp(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.DOWN_RIGHT, 1, 4 + 1);
        }

        private void downbutton_MouseDown(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_DOWN, 0, 4 + 1);
        }

        private void downbutton_MouseUp(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_DOWN, 1, 4 + 1);
        }

        private void leftdownbutton_MouseDown(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.DOWN_LEFT, 0, 4 + 1);
        }

        private void leftdownbutton_MouseUp(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.DOWN_LEFT, 1, 4 + 1);
        }

        private void leftbutton_MouseDown(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_LEFT, 0, 4 + 1);
        }

        private void leftbutton_MouseUp(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_LEFT, 1, 4 + 1);
        }

        private void leftupbutton_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            VideoPewViewForm videoPewViewForm = new VideoPewViewForm();
            //videoPewViewForm.m_lUserID = m_lUserID;
            videoPewViewForm.DeviceInfo = DeviceInfo;
            videoPewViewForm.Show();
        }

        private void panel1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.ZOOM_IN, 0);
                //CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.ZOOM_IN, 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.ZOOM_OUT, 0);
                //CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.ZOOM_OUT, 1);
            }

            System.Timers.Timer t = new System.Timers.Timer(500);
            t.Elapsed += new System.Timers.ElapsedEventHandler(stopMouseWheelAct);
            t.AutoReset = false;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }

        private void stopMouseWheelAct(object source, System.Timers.ElapsedEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.ZOOM_IN, 1);
            CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.ZOOM_OUT, 1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pUid[0] != IntPtr.Zero)
            {
                //uint a = ipcsdk.ICE_IPCSDK_OpenGate(pUid[0]);//开闸
                uint a = ipcsdk.ICE_IPCSDK_ControlAlarmOut(pUid[0], 1);


                OutputPrintInfo("");

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

       
    }






    public class util
    {
        private static int width;
        private static int height;
        private static int length;
        private static int v;  //v值的起始位置
        private static int u;  //u值的起始位置
        private static int rdif, invgdif, bdif;
        private static int[] Table_fv1;
        private static int[] Table_fv2;
        private static int[] Table_fu1;
        private static int[] Table_fu2;
        private static int[] rgb = new int[3];
        private static int m, n, i, j, hfWidth;
        private static bool addHalf = true;
        private static int py;
        private static int pos, pos1;//dopod 595 图像调整
        private static byte temp;

        public static void YV12ToRGB(int iWidth, int iHeight)
        {
            Table_fv1 = new int[256] { -180, -179, -177, -176, -174, -173, -172, -170, -169, -167, -166, -165, -163, -162, -160, -159, -158, -156, -155, -153, -152, -151, -149, -148, -146, -145, -144, -142, -141, -139, -138, -137, -135, -134, -132, -131, -130, -128, -127, -125, -124, -123, -121, -120, -118, -117, -115, -114, -113, -111, -110, -108, -107, -106, -104, -103, -101, -100, -99, -97, -96, -94, -93, -92, -90, -89, -87, -86, -85, -83, -82, -80, -79, -78, -76, -75, -73, -72, -71, -69, -68, -66, -65, -64, -62, -61, -59, -58, -57, -55, -54, -52, -51, -50, -48, -47, -45, -44, -43, -41, -40, -38, -37, -36, -34, -33, -31, -30, -29, -27, -26, -24, -23, -22, -20, -19, -17, -16, -15, -13, -12, -10, -9, -8, -6, -5, -3, -2, 0, 1, 2, 4, 5, 7, 8, 9, 11, 12, 14, 15, 16, 18, 19, 21, 22, 23, 25, 26, 28, 29, 30, 32, 33, 35, 36, 37, 39, 40, 42, 43, 44, 46, 47, 49, 50, 51, 53, 54, 56, 57, 58, 60, 61, 63, 64, 65, 67, 68, 70, 71, 72, 74, 75, 77, 78, 79, 81, 82, 84, 85, 86, 88, 89, 91, 92, 93, 95, 96, 98, 99, 100, 102, 103, 105, 106, 107, 109, 110, 112, 113, 114, 116, 117, 119, 120, 122, 123, 124, 126, 127, 129, 130, 131, 133, 134, 136, 137, 138, 140, 141, 143, 144, 145, 147, 148, 150, 151, 152, 154, 155, 157, 158, 159, 161, 162, 164, 165, 166, 168, 169, 171, 172, 173, 175, 176, 178 };
            Table_fv2 = new int[256] { -92, -91, -91, -90, -89, -88, -88, -87, -86, -86, -85, -84, -83, -83, -82, -81, -81, -80, -79, -78, -78, -77, -76, -76, -75, -74, -73, -73, -72, -71, -71, -70, -69, -68, -68, -67, -66, -66, -65, -64, -63, -63, -62, -61, -61, -60, -59, -58, -58, -57, -56, -56, -55, -54, -53, -53, -52, -51, -51, -50, -49, -48, -48, -47, -46, -46, -45, -44, -43, -43, -42, -41, -41, -40, -39, -38, -38, -37, -36, -36, -35, -34, -33, -33, -32, -31, -31, -30, -29, -28, -28, -27, -26, -26, -25, -24, -23, -23, -22, -21, -21, -20, -19, -18, -18, -17, -16, -16, -15, -14, -13, -13, -12, -11, -11, -10, -9, -8, -8, -7, -6, -6, -5, -4, -3, -3, -2, -1, 0, 0, 1, 2, 2, 3, 4, 5, 5, 6, 7, 7, 8, 9, 10, 10, 11, 12, 12, 13, 14, 15, 15, 16, 17, 17, 18, 19, 20, 20, 21, 22, 22, 23, 24, 25, 25, 26, 27, 27, 28, 29, 30, 30, 31, 32, 32, 33, 34, 35, 35, 36, 37, 37, 38, 39, 40, 40, 41, 42, 42, 43, 44, 45, 45, 46, 47, 47, 48, 49, 50, 50, 51, 52, 52, 53, 54, 55, 55, 56, 57, 57, 58, 59, 60, 60, 61, 62, 62, 63, 64, 65, 65, 66, 67, 67, 68, 69, 70, 70, 71, 72, 72, 73, 74, 75, 75, 76, 77, 77, 78, 79, 80, 80, 81, 82, 82, 83, 84, 85, 85, 86, 87, 87, 88, 89, 90, 90 };
            Table_fu1 = new int[256] { -44, -44, -44, -43, -43, -43, -42, -42, -42, -41, -41, -41, -40, -40, -40, -39, -39, -39, -38, -38, -38, -37, -37, -37, -36, -36, -36, -35, -35, -35, -34, -34, -33, -33, -33, -32, -32, -32, -31, -31, -31, -30, -30, -30, -29, -29, -29, -28, -28, -28, -27, -27, -27, -26, -26, -26, -25, -25, -25, -24, -24, -24, -23, -23, -22, -22, -22, -21, -21, -21, -20, -20, -20, -19, -19, -19, -18, -18, -18, -17, -17, -17, -16, -16, -16, -15, -15, -15, -14, -14, -14, -13, -13, -13, -12, -12, -11, -11, -11, -10, -10, -10, -9, -9, -9, -8, -8, -8, -7, -7, -7, -6, -6, -6, -5, -5, -5, -4, -4, -4, -3, -3, -3, -2, -2, -2, -1, -1, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7, 8, 8, 8, 9, 9, 9, 10, 10, 11, 11, 11, 12, 12, 12, 13, 13, 13, 14, 14, 14, 15, 15, 15, 16, 16, 16, 17, 17, 17, 18, 18, 18, 19, 19, 19, 20, 20, 20, 21, 21, 22, 22, 22, 23, 23, 23, 24, 24, 24, 25, 25, 25, 26, 26, 26, 27, 27, 27, 28, 28, 28, 29, 29, 29, 30, 30, 30, 31, 31, 31, 32, 32, 33, 33, 33, 34, 34, 34, 35, 35, 35, 36, 36, 36, 37, 37, 37, 38, 38, 38, 39, 39, 39, 40, 40, 40, 41, 41, 41, 42, 42, 42, 43, 43 };
            Table_fu2 = new int[256] { -227, -226, -224, -222, -220, -219, -217, -215, -213, -212, -210, -208, -206, -204, -203, -201, -199, -197, -196, -194, -192, -190, -188, -187, -185, -183, -181, -180, -178, -176, -174, -173, -171, -169, -167, -165, -164, -162, -160, -158, -157, -155, -153, -151, -149, -148, -146, -144, -142, -141, -139, -137, -135, -134, -132, -130, -128, -126, -125, -123, -121, -119, -118, -116, -114, -112, -110, -109, -107, -105, -103, -102, -100, -98, -96, -94, -93, -91, -89, -87, -86, -84, -82, -80, -79, -77, -75, -73, -71, -70, -68, -66, -64, -63, -61, -59, -57, -55, -54, -52, -50, -48, -47, -45, -43, -41, -40, -38, -36, -34, -32, -31, -29, -27, -25, -24, -22, -20, -18, -16, -15, -13, -11, -9, -8, -6, -4, -2, 0, 1, 3, 5, 7, 8, 10, 12, 14, 15, 17, 19, 21, 23, 24, 26, 28, 30, 31, 33, 35, 37, 39, 40, 42, 44, 46, 47, 49, 51, 53, 54, 56, 58, 60, 62, 63, 65, 67, 69, 70, 72, 74, 76, 78, 79, 81, 83, 85, 86, 88, 90, 92, 93, 95, 97, 99, 101, 102, 104, 106, 108, 109, 111, 113, 115, 117, 118, 120, 122, 124, 125, 127, 129, 131, 133, 134, 136, 138, 140, 141, 143, 145, 147, 148, 150, 152, 154, 156, 157, 159, 161, 163, 164, 166, 168, 170, 172, 173, 175, 177, 179, 180, 182, 184, 186, 187, 189, 191, 193, 195, 196, 198, 200, 202, 203, 205, 207, 209, 211, 212, 214, 216, 218, 219, 221, 223, 225 };
            width = iWidth;
            height = iHeight;
            length = iWidth * iHeight;
            v = length;//nYLen
            u = v + (length >> 2);
            hfWidth = iWidth >> 1;
            addHalf = true;
        }

        public static bool Convert(int cwidth, int cheight, byte[] yv12y, byte[] yv12u, byte[] yv12v, ref byte[] rgb24)
        {
            try
            {
                YV12ToRGB(cwidth, cheight);
                if (yv12y.Length == 0 || rgb24.Length == 0)
                    return false;
                m = -width;
                n = -hfWidth;
                for (int y = 0; y < height; y++)
                {
                    if (y == 139)
                    {
                    }
                    m += width;
                    if (addHalf)
                    {
                        n += hfWidth;
                        addHalf = false;
                    }
                    else
                    {
                        addHalf = true;
                    }
                    for (int x = 0; x < width; x++)
                    {
                        i = m + x;
                        j = n + (x >> 1);
                        py = (int)yv12y[i];
                        rdif = Table_fv1[(int)yv12v[j]];
                        invgdif = Table_fu1[(int)yv12u[j]] + Table_fv2[(int)yv12v[j]];
                        bdif = Table_fu2[(int)yv12u[j]];

                        rgb[2] = py + rdif;//R
                        rgb[1] = py - invgdif;//G
                        rgb[0] = py + bdif;//B

                        j = v - width - m + x;
                        i = (j << 1) + j;

                        // copy this pixel to rgb data
                        for (j = 0; j < 3; j++)
                        {

                            if (rgb[j] >= 0 && rgb[j] <= 255)
                            {
                                rgb24[i + j] = (byte)rgb[j];
                            }
                            else
                            {
                                rgb24[i + j] = (byte)((rgb[j] < 0) ? 0 : 255);
                            }

                        }
                        if (x % 4 == 3)
                        {
                            pos = (m + x - 1) * 3;
                            pos1 = (m + x) * 3;
                            temp = rgb24[pos];
                            rgb24[pos] = rgb24[pos1];
                            rgb24[pos1] = temp;

                            temp = rgb24[pos + 1];
                            rgb24[pos + 1] = rgb24[pos1 + 1];
                            rgb24[pos1 + 1] = temp;

                            temp = rgb24[pos + 2];
                            rgb24[pos + 2] = rgb24[pos1 + 2];
                            rgb24[pos1 + 2] = temp;
                        }
                    }
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
            return true;
        }
    }
}
