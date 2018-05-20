using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Reflection;
using System.Net;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows;
using System.Threading;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace MvvmExmaple
{
    public class GetWebData
    {
        public TempleInfo m_pTempInfoData = new TempleInfo();

        private void threadProc(object sender)
        {
            GetTempleInfobyWebService();
        }

        public GetWebData()
        {

            Thread thr = new Thread(threadProc);
            thr.IsBackground = true;
            thr.Start();

            //GetTempleInfobyWebService();
           
        }

        public string NoHTML(string Htmlstring)  //替换HTML标记
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<span[^>]*?>.*?</span>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(middot|#183);", "·", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(deg|#176);", "°", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(macr|#175);", "ˉ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&ldquo;", "\"", RegexOptions.IgnoreCase);//保留【 “ 】的标点符合
            Htmlstring = Regex.Replace(Htmlstring, @"&rdquo;", "\"", RegexOptions.IgnoreCase);//保留【 ” 】的标点符合
            Htmlstring = Regex.Replace(Htmlstring, "&#[^>]*;", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?marquee[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?object[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?param[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?embed[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?table[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?tr[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?th[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "<p[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</p[^>]*>", "\n\r", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?a[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?tbody[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?li[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?span[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?div[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?th[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?td[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?script[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "(javascript|jscript|vbscript|vbs):", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "on(mouse|exit|error|click|key)", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "<\\?xml[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "<\\/?[a-z]+:[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?font[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?b[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?u[^>]*>", "", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, "</?i[^>]*>", "", RegexOptions.IgnoreCase);
//            Htmlstring = Regex.Replace(Htmlstring, "</?i[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "</?strong[^>]*>", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            //Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }

        //获取前半部分link的字符串长度
        public string getLinkByPic()
        {
            string strDomino = ConfigurationManager.AppSettings["domino"];
            string strPort = ConfigurationManager.AppSettings["port"];
            string strLinkMode = ConfigurationManager.AppSettings["LinkMod"];
            string strInterfacelink = strLinkMode + strDomino + @":" + strPort + @"/";
            return strInterfacelink;
        }

        //获取全部link的字符串长度
        public string getFullpathPicLink(string URLfromDatastruct)
        {
            string str3 = URLfromDatastruct.Substring(2, (URLfromDatastruct.Length - 2));
            return getLinkByPic() + str3;
        }

        //寺庙信息
        public void GetTempleInfobyWebService()
        {
            m_pTempInfoData.body = null;

            string ssTempleInfo = getInfoFromInterFace("TempleInfo_Interface", "Interface_Param", "Interface_id");
            if (ssTempleInfo.Length > 0)
            {

                m_pTempInfoData = JsonConvert.DeserializeObject<TempleInfo>(ssTempleInfo);

                if (m_pTempInfoData.body.data != null)
                {
                    m_pTempInfoData.body.data.info = NoHTML(m_pTempInfoData.body.data.info);
                    m_pTempInfoData.body.data.detail = NoHTML(m_pTempInfoData.body.data.detail);

                    m_pTempInfoData.body.data.url = getFullpathPicLink(m_pTempInfoData.body.data.url);
                }

            }
            
        }
       

        //获取服务器Link
        public string setBaseWebLinkPath()
        {
            string strDomino = ConfigurationManager.AppSettings["domino"];
            string strPort = ConfigurationManager.AppSettings["port"];
            string strurl = ConfigurationManager.AppSettings["url"];
            string strLinkMode = ConfigurationManager.AppSettings["LinkMod"];
            string strInterfacelink = strLinkMode + strDomino + @":" + strPort + strurl + @"/";
            return strInterfacelink;
        }
        //获取接口和参数
        public string getWebInterFaceLinkPath(string Inferface_Field, string Param_Field, string Id_Field)
        {
            string strInterfaceName = ConfigurationManager.AppSettings[Inferface_Field];
            string strParamName = ConfigurationManager.AppSettings[Param_Field];
            string strIdName = ConfigurationManager.AppSettings[Id_Field];
            string strInterfacelink = strInterfaceName + @"?" + strParamName + @"=" + strIdName;
            return strInterfacelink;
        }

        //获取所有Link
        public string getFullLink(string Inferface_Field, string Param_Field, string Id_Field)
        {
            string strBaseWebLink = setBaseWebLinkPath();
            string strInterfaceLink = getWebInterFaceLinkPath(Inferface_Field, Param_Field, Id_Field);
            string strFullInterface = strBaseWebLink + strInterfaceLink;
            return strFullInterface;
        }

        //获取所有Link的信息
        public string getInfoFromInterFace(string Inferface_Field, string Param_Field, string Id_Field)
        {
            string strFullInterface = getFullLink(Inferface_Field, Param_Field, Id_Field);
            return HttpGet(strFullInterface, Inferface_Field);
        }

        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            // 总是接受    
            return true;

        }
        public string HttpGet(string url, string Inferface_Field)
        {
            try
            {
                Encoding encoding = Encoding.UTF8;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                if (request.Pipelined != false)
                {
                    request.Method = "GET";
                    request.Accept = "text/html, application/xhtml+xml, */*";
                    request.ContentType = "application/json";
                    request.Timeout = 5000;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
                
            }
            catch (WebException ex)
            {
//                string strerr = ex.Message;
            }
            return "";
        }
    }
}
