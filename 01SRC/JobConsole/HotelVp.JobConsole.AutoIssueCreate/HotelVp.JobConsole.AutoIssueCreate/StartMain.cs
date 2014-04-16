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
                string strActionType = string.Empty;
                if (args.Length != 0)   //有参数 
                {
                    Console.WriteLine("参数结果：" + args[0]) ;
                    strActionType = args[0];
                }

                AutoIssueCreateBP.AutoIssueCreating(strActionType);
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
