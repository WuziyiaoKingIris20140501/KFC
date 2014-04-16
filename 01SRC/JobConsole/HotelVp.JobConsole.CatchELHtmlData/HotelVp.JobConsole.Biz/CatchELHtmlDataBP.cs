using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Data.OracleClient;
using System.Net;

using HotelVp.Common.DBUtility;
using HotelVp.JobConsole.Entity;
using HotelVp.JobConsole.DataAccess;

namespace HotelVp.JobConsole.Biz
{
    public abstract class CatchELHtmlDataBP
    {
        public static void CatchELHtmlDataSetting()
        {
            try
            {
                //string strUrl = "http://tuan.elong.com/";
                //BlogRege blogRegex = new BlogRege();
                //string result = blogRegex.SendUrl(strUrl);
                //blogRegex.AnalysisHtml_City_1(result);
                BlogRege blogRegex = new BlogRege();
                blogRegex.AnalysisHtml_City_2();




                //string strBaseUrl = "http://www.cnblogs.com/p";
                //for (int i = 1; i <= 200; i++)
                //{
                //    string strUrl = strBaseUrl + i.ToString();
                //    BlogRege blogRegex = new BlogRege();
                //    string result = blogRegex.SendUrl(strUrl);
                //    blogRegex.AnalysisHtml(result);

                //    Console.WriteLine("获取成功");
                //}

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("");
        }
    }
}
