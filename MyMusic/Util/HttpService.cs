using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Util
{
    class HttpService
    {
        //单例模式
        private static HttpService instance;
        private HttpClient httpClient = new HttpClient();
        private static object _lock = new object();
        private CookieContainer cookie = new CookieContainer();

        private HttpService()
        {
        }

        public static HttpService GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new HttpService();
                    }
                }
            }
            return instance;
        }

        public async Task<string> GetJsonStringAsync(string url)
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);
            string content =  await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetJsonAfterLogIn(string Url)
        {
            WebResponse myresponse;
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            if(cookie.Count == 0)
            {
                string urlHead = "http://littlefish33.cn:3000/";
                myHttpWebRequest.CookieContainer = new CookieContainer();
                myresponse = await myHttpWebRequest.GetResponseAsync();
                //Debug.WriteLine("begin");
                //Debug.WriteLine(myresponse.Headers["Set-Cookie"]);
                CookieCollection cookies = GetAllCookiesFromHeader(myresponse.Headers["Set-Cookie"], myresponse.ResponseUri.Host);
               // Debug.WriteLine(myresponse.Headers["Set-Cookie"]);
                cookie.Add(new Uri(urlHead), cookies);
            }
            else
            {
                myHttpWebRequest.CookieContainer = cookie;
                myresponse = await myHttpWebRequest.GetResponseAsync();
            }

            Stream myResponseStream = myresponse.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Dispose();
            myResponseStream.Dispose();

            return retString;
        }

        private static CookieCollection GetAllCookiesFromHeader(string strHeader, string strHost)
        {
            ArrayList al = new ArrayList();
            CookieCollection cc = new CookieCollection();
            if (strHeader != string.Empty)
            {
                al = ConvertCookieHeaderToArrayList(strHeader);
                //Debug.WriteLine("-----------------------------");
                cc = ConvertCookieArraysToCookieCollection(al, strHost);
            }
            return cc;
        }

        private static ArrayList ConvertCookieHeaderToArrayList(string strCookHeader)
        {
            strCookHeader = strCookHeader.Replace("\r", "");
            strCookHeader = strCookHeader.Replace("\n", "");
            string[] strCookTemp = strCookHeader.Split(',');
            ArrayList al = new ArrayList();
            int i = 0;
            int n = strCookTemp.Length;
            while (i < n)
            {
                if (strCookTemp[i].IndexOf("expires=", StringComparison.OrdinalIgnoreCase) > 0)
                {
                    al.Add(strCookTemp[i] + "," + strCookTemp[i + 1]);
                    i = i + 1;
                }
                else
                {
                    al.Add(strCookTemp[i]);
                }
                i = i + 1;
            }
            for(i = 0; i < al.Count; i++)
            {
                Debug.WriteLine(al[i]);
            }
            return al;
        }

        private static CookieCollection ConvertCookieArraysToCookieCollection(ArrayList al, string strHost)
        {
            CookieCollection cc = new CookieCollection();

            int alcount = al.Count;
            string strEachCook;
            string[] strEachCookParts;
            for (int i = 0; i < alcount; i++)
            {
                strEachCook = al[i].ToString();
                strEachCookParts = strEachCook.Split(';');
                int intEachCookPartsCount = strEachCookParts.Length;
                string strCNameAndCValue = string.Empty;
                string strPNameAndPValue = string.Empty;
                string strDNameAndDValue = string.Empty;
                string[] NameValuePairTemp;
                Cookie cookTemp = new Cookie();

                for (int j = 0; j < intEachCookPartsCount; j++)
                {
                    if (j == 0)
                    {
                        strCNameAndCValue = strEachCookParts[j];
                        if (strCNameAndCValue != string.Empty)
                        {
                            int firstEqual = strCNameAndCValue.IndexOf("=");
                            string firstName = strCNameAndCValue.Substring(1, firstEqual - 1);
                            string allValue = strCNameAndCValue.Substring(firstEqual + 1, strCNameAndCValue.Length - (firstEqual + 1));
                           // Debug.WriteLine("\"" + firstName + "\"");
                            cookTemp.Name = firstName;
                            cookTemp.Value = allValue;
                        }
                        continue;
                    }
                    if (strEachCookParts[j].IndexOf("path", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        strPNameAndPValue = strEachCookParts[j];
                        if (strPNameAndPValue != string.Empty)
                        {
                            NameValuePairTemp = strPNameAndPValue.Split('=');
                            if (NameValuePairTemp[1] != string.Empty)
                            {
                                cookTemp.Path = NameValuePairTemp[1];
                            }
                            else
                            {
                                cookTemp.Path = "/";
                            }
                        }
                        continue;
                    }

                    if (strEachCookParts[j].IndexOf("domain", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        strPNameAndPValue = strEachCookParts[j];
                        if (strPNameAndPValue != string.Empty)
                        {
                            NameValuePairTemp = strPNameAndPValue.Split('=');

                            if (NameValuePairTemp[1] != string.Empty)
                            {
                                cookTemp.Domain = NameValuePairTemp[1];
                            }
                            else
                            {
                                cookTemp.Domain = strHost;
                            }
                        }
                        continue;
                    }
                }

                if (cookTemp.Path == string.Empty)
                {
                    cookTemp.Path = "/";
                }
                if (cookTemp.Domain == string.Empty)
                {
                    cookTemp.Domain = strHost;
                }
                cc.Add(cookTemp);
            }
            return cc;
        }
    }
}
