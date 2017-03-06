using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using com.school114.models;

namespace com.school114.services
{
    /// <summary>
    /// suguo.yao 2017-3-6
    /// 冀校通短信通道
    /// </summary>
    public static class MessageService
    {
        static HttpClient httpclient = null;
        static string currday = null;
        static string secretKey = null;
        public static bool submit(Message message)
        {
            try
            {
                if (httpclient == null)
                {
                    httpclient = new HttpClient();
                    httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
                    httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                }

                if (currday != DateTime.Now.ToString("yyyyMMdd"))
                {
                    currday = DateTime.Now.ToString("yyyyMMdd");
                    secretKey = utils.Secret.StringToMD5Hash(Config.loginPwd, 6);
                }

                string postData = string.Format("loginName={0}&loginPWD={1}&siID={2}&area={3}&mobiles={4}&content={5}&postTime={6:yyyyMMddHHmmss}", Config.loginName, secretKey, message.smId, message.cityCode, message.toMobile, message.content, message.plannTime);
                var content = new StringContent(postData, Encoding.GetEncoding("GBK"));

                var result = httpclient.PostAsync(Config.smsUrl, content).Result.Content.ReadAsStringAsync().Result;
                if (result == "00000")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                secretKey = null;
                currday = null;
                httpclient = null;
                if (Config.isDebug)
                    throw ex;
            }
            return false;
        }
    }
}