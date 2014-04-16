using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JJZX.JobConsole.Biz;
using System.Threading;

namespace HotelVp.JobConsole.Destination
{
    class StartMain
    {
        static void Main(string[] args)
        {
            try
            {
                string ActionType = string.Empty;
                if (args.Length != 0)   //有参数 
                {
                    Console.WriteLine("参数结果：" + args[0]);
                    ActionType = args[0];
                    AutoPlanBP.AutoPlaning(ActionType);
                }
                else
                {
                    Console.WriteLine("请输入参数： 1 为抽取幸运用户充值  2 为充值用户同步充值结果" );
                    Thread.Sleep(5000);
                }
                
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
