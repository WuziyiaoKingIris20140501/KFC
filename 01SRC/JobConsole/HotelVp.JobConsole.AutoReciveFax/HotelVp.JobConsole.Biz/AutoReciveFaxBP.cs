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
using System.Net.Mail;
using System.Threading;

using HotelVp.Common.Json;
using HotelVp.Common.Json.Linq;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;

using HotelVp.JobConsole.Entity;
using HotelVp.JobConsole.DataAccess;
using HotelVp.JobConsole.ServiceAdapter;


namespace HotelVp.JobConsole.Biz
{
    public abstract class AutoReciveFaxBP
    {
        //static string _nameSpaceClass = "HotelVp.JobConsole.Biz.AutoOrderSynchronizing  Method: ";

        public static void AutoReciveFaxing()
        {
            DateTime dtBegin = new DateTime();
            dtBegin = System.DateTime.Now;

            AutoReciveFaxEntity _hotelcomparisonEntity = new AutoReciveFaxEntity();
            CommonEntity _commonEntity = new CommonEntity();
            _hotelcomparisonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
            _hotelcomparisonEntity.LogMessages.Userid = "JOB System";
            _hotelcomparisonEntity.LogMessages.Username = "JOB System";
            
            Console.WriteLine("比价JOB自动运行开始");

            int iCount = AutoFaxSA.QueryFaxList();

            Console.WriteLine("比价JOB自动运行结束 记录数:" + iCount.ToString());
            DateTime dtEnd = new DateTime();
            dtEnd = System.DateTime.Now;

            Console.WriteLine(dtEnd - dtBegin);
        }
    }
}