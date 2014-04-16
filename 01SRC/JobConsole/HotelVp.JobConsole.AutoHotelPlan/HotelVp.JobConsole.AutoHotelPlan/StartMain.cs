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
        static void Main(string[] args)
        {
            try
            {
                AutoHotelPlanBP.AutoHotelPlaning();
            }
            catch(Exception ex)
            {
                Console.WriteLine("异常结果：" + ex.Message);
                Thread.Sleep(5000);
            }
            finally 
            { 
                
            }
        }
    }
}
