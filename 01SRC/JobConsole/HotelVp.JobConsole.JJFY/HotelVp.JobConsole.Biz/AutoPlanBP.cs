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

//using HotelVp.Common.Logger;
//using HotelVp.Common.DBUtility;
using JJZX.JobConsole.Entity;
using JJZX.JobConsole.DataAccess;
using JJZX.JobConsole.ServiceAdapter;


namespace JJZX.JobConsole.Biz
{
    public abstract class AutoPlanBP
    {
        public static void AutoPlaning(string ActionType)
        {
            DateTime dtBegin = new DateTime();
            dtBegin = System.DateTime.Now;
            Console.WriteLine("锦江之星福缘充值JOB自动运行开始");
            try
            {
                int iCount = 0;
                if ("1".Equals(ActionType))
                {
                    iCount = AutoSelect();
                }
                else
                {
                    iCount = AutoUpdateStatus();
                }

                if ("1".Equals(ConfigurationManager.AppSettings["ShowYE"].ToString()))
                {
                   Hashtable hsResult = AutoHotelPlanSA.ApplyFYYEInterface();
                    Console.WriteLine("锦江之星福缘充值接口余额：" + (hsResult.ContainsKey("credit") ? hsResult["credit"].ToString() : ""));
                }


                Console.WriteLine("锦江之星福缘充值JOB自动运行 执行记录数：" + iCount.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("JOB Error:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.StackTrace);
            }

            DateTime dtEnd = new DateTime();
            dtEnd = System.DateTime.Now;

            Console.WriteLine(dtEnd - dtBegin);
        }

        private static int AutoUpdateStatus()
        {
            DataSet dsOrderList = AutoPlanDA.GetOrderYFComfirnList();

            foreach (DataRow drRow in dsOrderList.Tables[0].Rows)
            {
                ApplyFyCxInterService(drRow);
            }

            return dsOrderList.Tables[0].Rows.Count;
        }

        private static int AutoSelect()
        {
            int iMaxNum = int.Parse(ConfigurationManager.AppSettings["iMaxNum"].ToString());
            DataSet dsOrderList = AutoPlanDA.GetOrderYesterDayList();

            if (dsOrderList.Tables[0].Rows.Count == 0)
            {
                return 0;
            }

            if (dsOrderList.Tables[0].Rows.Count <= iMaxNum)
            {
                foreach (DataRow drRow in dsOrderList.Tables[0].Rows)
                {
                    ApplyFyCzInterService(drRow);
                }

                return dsOrderList.Tables[0].Rows.Count;
            }

            int iTotal = dsOrderList.Tables[0].Rows.Count - 1;
            int iRand;
            ArrayList alNum = new ArrayList();
            Random rand = new Random();

            while (alNum.Count < iMaxNum)
            {
                iRand = rand.Next(0, iTotal);
                if (!alNum.Contains(iRand))
                {
                    alNum.Add(iRand);
                }
            }

            for (int i = 0; i < alNum.Count; i++)
            {
                ApplyFyCzInterService(dsOrderList.Tables[0].Rows[(int)alNum[i]]);
            }

            return alNum.Count;
        }

        private static void ApplyFyCzInterService(DataRow drRow)
        {
            AutoPlanEntity _autohotelplanEntity = new AutoPlanEntity();
            _autohotelplanEntity.AutoHotelPlanDBEntity = new List<AutoHotelPlanDBEntity>();
            AutoHotelPlanDBEntity appcontentDBEntity = new AutoHotelPlanDBEntity();
            _autohotelplanEntity.AutoHotelPlanDBEntity.Add(appcontentDBEntity);

            string AgentID = ConfigurationManager.AppSettings["AgentID"].ToString();
            string Money = ConfigurationManager.AppSettings["FYMON"].ToString();

            appcontentDBEntity.AgentId = AgentID;
            appcontentDBEntity.Money = Money;
            appcontentDBEntity.OrderId = drRow["RESVID"].ToString().Trim();
            appcontentDBEntity.MobileNum = drRow["MOBILE"].ToString().Trim();
            appcontentDBEntity.UNIT_ID = drRow["UNIT_ID"].ToString().Trim();
            appcontentDBEntity.ORDER_STATUS = drRow["ORDER_STATUS"].ToString().Trim();
            appcontentDBEntity.NAME = drRow["NAME"].ToString().Trim();
            appcontentDBEntity.ARRD = drRow["ARRD"].ToString().Trim();
            appcontentDBEntity.DEPD = drRow["DEPD"].ToString().Trim();
            appcontentDBEntity.CHANNEL = drRow["CHANNEL"].ToString().Trim();
            appcontentDBEntity.PLATFORM_CODE = drRow["PLATFORM_CODE"].ToString().Trim();
            appcontentDBEntity.GUEST_ID = drRow["GUEST_TD"].ToString().Trim(); ;

            _autohotelplanEntity.AutoHotelPlanDBEntity.Add(appcontentDBEntity);
            AutoHotelPlanSA.ApplyFYCZInterface(_autohotelplanEntity);
        }

        private static void ApplyFyCxInterService(DataRow drRow)
        {
            AutoPlanEntity _autohotelplanEntity = new AutoPlanEntity();
            _autohotelplanEntity.AutoHotelPlanDBEntity = new List<AutoHotelPlanDBEntity>();
            AutoHotelPlanDBEntity appcontentDBEntity = new AutoHotelPlanDBEntity();
            _autohotelplanEntity.AutoHotelPlanDBEntity.Add(appcontentDBEntity);

            string AgentID = ConfigurationManager.AppSettings["AgentID"].ToString();

            appcontentDBEntity.AgentId = AgentID;
            appcontentDBEntity.OrderId = drRow["RESVID"].ToString().Trim();
            appcontentDBEntity.MobileNum = drRow["MOBILE"].ToString().Trim();

            _autohotelplanEntity.AutoHotelPlanDBEntity.Add(appcontentDBEntity);
            AutoHotelPlanSA.ApplyFYCXInterface(_autohotelplanEntity);
        }
    }
}