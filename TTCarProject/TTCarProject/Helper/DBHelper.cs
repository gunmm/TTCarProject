using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;





namespace TTCarProject
{
    class DBHelper
    {
        private static MySqlConnection mySqlConn = null;
        private static MySqlConnection mySqlConn2 = null;
        private static MySqlConnection mySqlConn3 = null;
        private static string sqlStr1 = null;
        private static string sqlStr2 = null;


        public static void WriteLog(string strLog)
        {

            string sFilePath = System.Environment.CurrentDirectory + "\\log\\" + DateTime.Now.ToString("yyyyMM");//logfilePath
            string sFileName = "rizhi" + DateTime.Now.ToString("dd") + ".log";
            sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
            if (!Directory.Exists(sFilePath))//验证路径是否存在
            {
                Directory.CreateDirectory(sFilePath);
                //不存在则创建
            }

            try
            {
                FileStream fs;
                StreamWriter sw;
                if (File.Exists(sFileName))
                //验证文件是否存在，有则追加，无则创建
                {
                    fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
                }
                else
                {
                    fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
                }
                sw = new StreamWriter(fs);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "   ---   " + strLog);
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                
            }
           
        }




        //判断数据库是否存在
        public static bool judgeDataBase()
        {

            MySqlConnection myCon = new MySqlConnection("Host =localhost;Username=root;Password=bnscroot#..");
            myCon.Open();

            MySqlCommand myCmd = new MySqlCommand("show databases like 'ttcarproject';", myCon);
            MySqlDataReader sdr = myCmd.ExecuteReader();
            string str = "";
            while (sdr.Read())
            {
                str = sdr[0].ToString();
            }
            myCon.Close();

            if (str == "ttcarproject")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //判断表是否存在
        public static bool judgeTable()
        {

            MySqlConnection myCon = new MySqlConnection("Host =localhost;Database=ttcarproject;Username=root;Password=bnscroot#..");
            myCon.Open();

            MySqlCommand myCmd = new MySqlCommand("show TABLES;", myCon);
            MySqlDataReader sdr = myCmd.ExecuteReader();
            int count = 0;
            while (sdr.Read())
            {
                string str = sdr[0].ToString();
                if (str == "subscribe" || str == "vehiclego")
                {
                    count++;
                }
            }
            myCon.Close();

            if (count == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //创建数据库
        public static void createDataBase()
        {

            MySqlConnection myCon = new MySqlConnection("Host =localhost;Username=root;Password=bnscroot#..");
            myCon.Open();

            MySqlCommand myCmd = new MySqlCommand("CREATE DATABASE ttcarproject DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;", myCon);
            myCmd.ExecuteNonQuery();
            myCon.Close();

        }

        //创建表
        public static void createTable()
        {

            MySqlConnection myCon = new MySqlConnection("Host =localhost;Database=ttcarproject;Username=root;Password=bnscroot#..");
            myCon.Open();

            MySqlCommand myCmd = new MySqlCommand("DROP TABLE IF EXISTS subscribe;" +
                                                  "CREATE TABLE subscribe(id varchar(36) NOT NULL,"+
                                                                          "appoint_man varchar(32),"+
                                                                          "appoint_time datetime,"+
                                                                          "appoint_type smallint(6),"+
                                                                          "begin_time datetime,"+
                                                                          "end_time datetime,"+
                                                                          "appoint_platnumber varchar(24),"+
                                                                          "appoint_reason varchar(256),"+
                                                                          "driver_name varchar(24),"+
                                                                          "driver_phone varchar(24),"+
                                                                          "PRIMARY KEY(id)"+
                                                                          ");"+
                                                  "DROP TABLE IF EXISTS vehiclego;" +
                                                  "CREATE TABLE vehiclego(id varchar(36) NOT NULL," +
                                                                         "entrance_guard_position_id varchar(36)," +
                                                                         "plate_number varchar(16),"+
                                                                         "passtime datetime,"+
                                                                         "go_style smallint(2),"+
                                                                         "go_reson smallint(2),"+
                                                                         "userName varchar(32),"+
                                                                         "isWhite smallint(2),"+
                                                                         "isOffLine smallint(2),"+
                                                                         "isUpLoaded smallint(2),"+
                                                                         " PRIMARY KEY(id)"+
                                                                         ")", myCon);
            myCmd.ExecuteNonQuery();
            myCon.Close();

        }

       // bnscroot#..
        public static string getSqlStr1()
        {
            if (sqlStr1 == null)
            {
                string localDataPass = ConfigurationManager.AppSettings["localDataPass"].ToString();
                sqlStr1 = "Host =localhost;Database=ttcarproject;Username=root;Password="+ localDataPass + ";Charset=utf8";
            }
            return sqlStr1;
        }

        public static string getSqlStr2()
        {
            if (sqlStr2 == null)
            {
                string dataBaseIp = ConfigurationManager.AppSettings["dataBaseIp"].ToString();

                sqlStr2 = "Host=" + dataBaseIp + ";Database=weighoutsite;Username=root;Password=123456;Charset=utf8";
            }
            return sqlStr2;
        }


        public static MySqlConnection getMySqlConnection()
        {
            if (mySqlConn == null)
            {
                string localDataPass = ConfigurationManager.AppSettings["localDataPass"].ToString();
                mySqlConn = new MySqlConnection("Host =localhost;Database=ttcarproject;Username=root;Password="+ localDataPass + ";Charset=utf8");
            }
            return mySqlConn;
        }

        public static MySqlConnection getMySqlConnection2()
        {
            if (mySqlConn2 == null)
            {
                string dataBaseIp = ConfigurationManager.AppSettings["dataBaseIp"].ToString();


                mySqlConn2 = new MySqlConnection("Host="+ dataBaseIp + ";Database=weighoutsite;Username=root;Password=123456;Charset=utf8");
            }
            return mySqlConn2;
        }

        public static MySqlConnection getMySqlConnection3()
        {
            if (mySqlConn3 == null)
            {
                string localDataPass = ConfigurationManager.AppSettings["localDataPass"].ToString();
                mySqlConn3 = new MySqlConnection("Host =localhost;Database=ttcarproject;Username=root;Password="+localDataPass+";Charset=utf8");
            }
            return mySqlConn3;
        }

        public static MySqlCommand getMySqlCommand()
        {
            DBHelper.getMySqlConnection().Open();
            MySqlCommand mycom = DBHelper.getMySqlConnection().CreateCommand();
            return mycom;
        }

        public static MySqlCommand getMySqlCommand2()
        {
            DBHelper.getMySqlConnection2().Open();
            MySqlCommand mycom = DBHelper.getMySqlConnection2().CreateCommand();
            return mycom;
        }

        public static MySqlCommand getMySqlCommand3()
        {
            DBHelper.getMySqlConnection3().Open();
            MySqlCommand mycom = DBHelper.getMySqlConnection3().CreateCommand();
            return mycom;
        }

        public static string getNowCId()
        {

            try
            {
                string maxCId = null;
                MySqlCommand mySqlCom = DBHelper.getMySqlCommand();
                string sqlStr = string.Format("select max(id) from PassingVehicle");

                mySqlCom.CommandText = sqlStr;
                //mySqlCom.CommandType = CommandType.Text;
                MySqlDataReader sdr = mySqlCom.ExecuteReader();
                while (sdr.Read())
                {
                    maxCId = sdr[0].ToString();
                }
                if (maxCId == null || maxCId == "")
                {
                    maxCId = "C00000001";
                }
                else
                {
                    string temp = maxCId.Substring(1);
                    long i = Convert.ToInt64(temp);
                    i++;
                    temp = i.ToString();
                    int length = temp.Length;
                    if (length<=8)
                    {
                        for (int j = 0; j < 8 - length; j++)
                        {
                            temp = "0" + temp;
                        }
                    }
                    
                    temp = "C" + temp;
                    maxCId = temp;
                }
                return maxCId;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                DBHelper.getMySqlConnection().Close();
            }
        }
    }
}
