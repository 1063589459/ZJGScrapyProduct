using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ScrapyProductInfo
{
    public class Utility
    {
        public static string GetResponse(string url)
        {
            string strContent = "";
            try
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/html";
                request.AllowAutoRedirect = true;//设置自动重定向

                request.ServicePoint.Expect100Continue = true;//设置自动重定向
                request.MaximumAutomaticRedirections = 50;//允许最大重定向次数
                request.CookieContainer = new CookieContainer();
                //request.Timeout = 30000;
                //request.ReadWriteTimeout = 30000;

                ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
                {
                    return true;
                };

                request.UserAgent = "Mozilla / 5.0(Linux; Android 8.0; STF - AL10 Build / HUAWEISTF - AL10; wv) AppleWebKit / 537.36(KHTML, like Gecko) Version / 4.0 Chrome / 53.0.2785.143 Crosswalk / 24.53.595.0 XWEB / 359 MMWEBSDK / 21 Mobile Safari/ 537.36 MicroMessenger / 6.6.7.1321(0x26060739) NetType / WIFI Language / zh_CN MicroMessenger / 6.6.7.1321(0x26060739) NetType / WIFI Language / zh_CN";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var heards = request.GetResponse().Headers;
                Stream myResponseStream = response.GetResponseStream();
                using (StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8))
                {
                    string retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();
                    return retString;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return strContent;
        }

        public static string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), dataEncode);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return ret;
        }

        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }

        public static int DownloadFile(string folder, string url)
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse webresponse = (HttpWebResponse)webRequest.GetResponse();
                if (webresponse.StatusCode == HttpStatusCode.OK)
                {
                    System.Drawing.Image image = System.Drawing.Image.FromStream(webresponse.GetResponseStream());
                    string filePath = Path.Combine(folder, Path.GetFileName(url));
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    image.Save(filePath);
                    image.Dispose();

                }
                return 1;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("下载失败", ex);
            }
            return 0;

        }

        public static string ReplaceWinIllegalName(string strName, bool blnReplaceDot)
        {
            if (string.IsNullOrEmpty(strName))
            {
                return string.Empty;
            }
            else
            {

                strName = strName.Replace("/", "_")
                                 .Replace(":", "_")
                                 .Replace("\t", "_")
                                 .Replace("\n", "_")
                                 .Replace("\r", "_")
                                 .Replace("\0", "_")
                                 .Replace("\b", "_")
                                 .Replace("©", "_")
                                 .Replace("®", "_")
                                 .Replace("™", "_")
                                 .Replace("℠", "_")
                                 .Replace("$", "_")
                                 .Replace("\"", "_")
                                 .Replace("\'", "_")
                                 .Replace("#", "_")
                                 .Replace("?", "_");

                if (blnReplaceDot)
                    strName = strName.Replace(".", "_");

                strName = strName.Replace("\\", "_")
                            .Replace("\"", "_")
                            .Replace("/", "_")
                            .Replace(":", "_")
                            .Replace("*", "_")
                            .Replace("?", "_")
                            .Replace("\"", "_")
                            .Replace("<", "_")
                            .Replace(">", "_")
                            .Replace("|", "_")
                            .Replace(Convert.ToChar(160).ToString(), "_")
                            .Replace("\t", "_")
                            .Replace("\n", "_")
                            .Replace("\r", "_");


                return strName;
            }
        }

        public static int DownloadFileByUrl(string url)
        {
            int iResult = 0;
            try
            {
                FileStream fs = new FileStream(url, FileMode.Open);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
            }
            catch (Exception)
            {

            }
            return iResult;
        }
    }
}