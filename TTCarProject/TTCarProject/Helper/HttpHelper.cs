using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace TTCarProject
{
    class HttpHelper
    {
        public static string HttpPost(string Url, Dictionary<string, object> param)
        {
            Dictionary<string, object> endParam = new Dictionary<string, object>();

            Dictionary<string, object> paramHead = new Dictionary<string, object>();
            paramHead.Add("osType", "1");
            paramHead.Add("accessToken", MainForm.accessToken);
            paramHead.Add("type", "1");
            paramHead.Add("userId", MainForm.userId);
            paramHead.Add("version", "1.0");

            endParam.Add("body", param);
            endParam.Add("head", paramHead);

            string postDataStr = JsonConvert.SerializeObject(endParam);


            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/json;charset=utf-8";
                request.ServicePoint.Expect100Continue = false;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;//SecurityProtocolType.Tls1.2;
                //request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                Stream myRequestStream = request.GetRequestStream();
                //StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
                myStreamWriter.Write(postDataStr);
                myStreamWriter.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                JObject jsonObj = JObject.Parse(retString);
                string head = jsonObj["head"].ToString();
                HeaderModel headerModel = new HeaderModel();
                headerModel = (HeaderModel)JsonToObject(head, headerModel);
                
               if(Convert.ToInt16(headerModel.rspCode) == -9)
                {
                    MainForm.accessToken = null;
                    return "accessToken";
                }
                else
                {
                    return retString;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "";
            }
           
            
            
        }

        public static string HttpPostLogin(string Url, Dictionary<string, object> param)
        {
            Dictionary<string, object> endParam = new Dictionary<string, object>();

            Dictionary<string, object> paramHead = new Dictionary<string, object>();
            paramHead.Add("osType", "1");
            paramHead.Add("accessToken", MainForm.accessToken);
            paramHead.Add("type", "1");
            paramHead.Add("userId", MainForm.userId);
            paramHead.Add("version", "1.0");

            endParam.Add("body", param);
            endParam.Add("head", paramHead);

            string postDataStr = JsonConvert.SerializeObject(endParam);


            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/json;charset=utf-8";
                request.ServicePoint.Expect100Continue = false;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;//SecurityProtocolType.Tls1.2;
                //request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                Stream myRequestStream = request.GetRequestStream();
                //StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
                myStreamWriter.Write(postDataStr);
                myStreamWriter.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                JObject jsonObj = JObject.Parse(retString);
                string head = jsonObj["head"].ToString();
                HeaderModel headerModel = new HeaderModel();
                headerModel = (HeaderModel)JsonToObject(head, headerModel);

                if (Convert.ToInt16(headerModel.rspCode) == -9)
                {
                    MainForm.accessToken = null;
                    return "accessToken";
                }
                else
                {
                    MainForm.accessToken = headerModel.accessToken;
                    return retString;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "";
            }



        }


        // 从一个Json串生成对象信息  
        public static object JsonToObject(string jsonString, object obj)
        {
            return JsonConvert.DeserializeObject(jsonString, obj.GetType());
        }

        public static string GetMD5(string input)
        {
            if (input == null)
            {
                return null;
            }

            MD5 md5Hash = MD5.Create();

            // 将输入字符串转换为字节数组并计算哈希数据 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // 创建一个 Stringbuilder 来收集字节并创建字符串 
            StringBuilder sBuilder = new StringBuilder();

            // 循环遍历哈希数据的每一个字节并格式化为十六进制字符串 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // 返回十六进制字符串 
            return sBuilder.ToString();
        }
    }
}
