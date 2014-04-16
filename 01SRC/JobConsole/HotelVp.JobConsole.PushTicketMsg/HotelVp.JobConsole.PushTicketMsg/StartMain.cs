using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using HotelVp.JobConsole.DataAccess;
using HotelVp.JobConsole.Biz;
using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;

namespace HotelVp.JobConsole.Destination
{
    class StartMain
    {
        static int Main(string[] args)
        {
            int iresult = 1;
            try
            {
                PushTicketMsgBP.PushTicketMsgActioning();
            }
            catch(Exception ex)
            {
                Console.WriteLine("异常结果：" + ex.Message);
                CommonDA.InsertEventHistory("Que Push发送异常： Messag：" + ex.Message + "InnerException：" + ex.InnerException + "Source：" + ex.Source + "StackTrace：" + ex.StackTrace);
                iresult = 0;
                Thread.Sleep(5000);
            }
            finally 
            { 
                
            }
            return iresult;
        }
    }
}
