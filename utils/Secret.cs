using System;
using System.Security.Cryptography;
using System.Text;

namespace com.school114.utils
{
    public static class Secret
    {
        /// <summary>
        /// suguo.yao 2015-5-6
        /// 信产密码加密算法
        /// 帐号与当前日期做与运算，取指定长度后进行32位MD5计算	
        /// </summary>	
        /// <param name="inputString"></param>	
        /// <param name="len">与运算后保留长度，不足前补0，多的话取后指定位数</param>
        /// <returns></returns>
        public static string StringToMD5Hash(string inputString, int len)
        {
            string result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(inputString))
                    throw new ArgumentNullException();
                int a = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int b = Int32.Parse(inputString);
                result = (a & b).ToString();
                if (result.Length > len)
                    result = result.Substring(result.Length - len);
                else if (result.Length < len)
                    result = result.PadLeft(len, '0');

                var md5 = MD5.Create();
                byte[] encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(result));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < encryptedBytes.Length; i++)
                {
                    sb.AppendFormat("{0:x2}", encryptedBytes[i]);
                }
                result = sb.ToString();
            }
            catch
            {
                result = null;
            }
            return result;
        }
    }
}