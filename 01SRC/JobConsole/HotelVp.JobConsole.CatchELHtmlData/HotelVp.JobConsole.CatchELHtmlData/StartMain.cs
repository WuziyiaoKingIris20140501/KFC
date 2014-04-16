using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HotelVp.JobConsole.Biz;
using System.Threading;

namespace HotelVp.JobConsole.Destination
{
    class StartMain
    {
        static int Main(string[] args)
        {
            int iresult = 1;
            try
            {
                CatchELHtmlDataBP.CatchELHtmlDataSetting();
            }
            catch(Exception ex)
            {
                Console.WriteLine("异常结果：" + ex.Message);
                iresult = 0;
            }
            finally 
            { 
                
            }
            return iresult;
        }
    }
}
