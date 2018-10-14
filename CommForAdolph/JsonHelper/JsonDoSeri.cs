using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace CommForAdolph.JsonHelper
{
    public class JsonResultGet
    {
        /// <summary>
        /// Post 带有参数请求Json的方法
        /// </summary>
        /// <param name="PARAMETER">请求的参数</param>
        /// <param name="POSTURL">请求的URL地址</param>
        /// <returns>返回json字符串</returns>
        public static JObject RequestJsonByUrlPost(string PARAMETER, string POSTURL)
        {
            JObject jo = new JObject();
            try
            {
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(PARAMETER);
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(POSTURL);
                myRequest.Method = "Post";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = data.Length;
                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string sJson = reader.ReadToEnd();
                jo = JObject.Parse(sJson);
            }
            catch (Exception ex)
            {
                
            }
            return jo;
        }
        /// <summary>
        /// Get 请求的方法
        /// </summary>
        /// <param name="geturl">请求的URL地址</param>
        /// <returns>返回json字符串</returns>
        public static JObject RequestJsonByUrlGet(string geturl)
        {
            JObject jo = new JObject();
            try
            {
                HttpWebRequest http = WebRequest.Create(geturl) as HttpWebRequest;
                HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

                string sJson = reader.ReadToEnd();
                http.Abort();
                ArrayList ObjList = new ArrayList();
                //将字符串转换成Json对象
                jo = JObject.Parse(sJson);
            }
            catch (Exception ex)
            {
                
            }
            return jo;
        }
    }

    public class JsonDoSeri<T>
    {
        /// <summary>
        /// 转换对象为JSON格式数据
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>字符格式的JSON数据</returns>
        public static string GetJSON<T>(object obj)
        {
            string result = String.Empty;
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    serializer.WriteObject(ms, obj);
                    result = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>
        /// 转换List<T>的数据为JSON格式
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="vals">列表值</param>
        /// <returns>JSON格式数据</returns>
        public static string JSON<T>(List<T> vals)
        {
            System.Text.StringBuilder st = new System.Text.StringBuilder();
            try
            {
                DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(T));

                foreach (T city in vals)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        s.WriteObject(ms, city);
                        st.Append(System.Text.Encoding.UTF8.GetString(ms.ToArray()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return st.ToString();
        }
        /// <summary>
        /// JSON格式字符转换为T类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T ParseFormByJson<T>(string jsonStr)
        {
            T obj = Activator.CreateInstance<T>();
            using (System.IO.MemoryStream ms =
            new MemoryStream(Encoding.UTF8.GetBytes(jsonStr)))
            {
                DataContractJsonSerializer serializer =
                new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }




        public static string JSON1<SendData>(List<SendData> vals)
        {
            StringBuilder st = new StringBuilder();
            try
            {
                DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(SendData));

                foreach (SendData city in vals)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        s.WriteObject(ms, city);
                        st.Append(Encoding.UTF8.GetString(ms.ToArray()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return st.ToString();
        }

    }
}
