using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;

using HotelVp.JobConsole.Entity;

namespace HotelVp.JobConsole.DataAccess
{
    public abstract class AutoIssueCreateDA
    {
        public static string InsertIssueData(DataRow drRow)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertIssueData");
            cmd.SetParameterValue("@Title", drRow["Title"].ToString().Trim());
            cmd.SetParameterValue("@Priority", drRow["Priority"].ToString().Trim());
            cmd.SetParameterValue("@IssueType", drRow["IssueType"].ToString().Trim());
            cmd.SetParameterValue("@Assignto", drRow["Assignto"].ToString().Trim());
            cmd.SetParameterValue("@IssueStatus", drRow["IssueStatus"].ToString().Trim());
            cmd.SetParameterValue("@IsIndemnify", drRow["IsIndemnify"].ToString().Trim());
            cmd.SetParameterValue("@IndemnifyPrice", drRow["IndemnifyPrice"].ToString().Trim());
            cmd.SetParameterValue("@TicketCode", drRow["TicketCode"].ToString().Trim());
            cmd.SetParameterValue("@TicketAmount", drRow["TicketAmount"].ToString().Trim());
            cmd.SetParameterValue("@RelatedType", drRow["RelatedType"].ToString().Trim());
            cmd.SetParameterValue("@RelatedID", drRow["RelatedID"].ToString().Trim());
            cmd.SetParameterValue("@Remark", drRow["Remark"].ToString().Trim());
            cmd.SetParameterValue("@CreateUser", drRow["CreateUser"].ToString().Trim());
            string dtNow = DateTime.Now.ToString();
            cmd.SetParameterValue("@CreateTime", dtNow);
            if (cmd.ExecuteNonQuery() != 1)
            {
                return "";
            }
            string IssueID = cmd.GetParameterValue("@IssueID").ToString();
            drRow["Title"] = String.Format(drRow["Title"].ToString(), IssueID);
            InsertIssueHistory(IssueID, drRow, dtNow);
            return IssueID;
        }

        public static int InsertIssueHistory(string strIssueID, DataRow drRow, string dtNow)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertIssueHistory");
            cmd.SetParameterValue("@IssueID", strIssueID);
            cmd.SetParameterValue("@Title", drRow["Title"].ToString().Trim());
            cmd.SetParameterValue("@Priority", drRow["Priority"].ToString().Trim());
            cmd.SetParameterValue("@IssueType", drRow["IssueType"].ToString().Trim());
            cmd.SetParameterValue("@Assignto", drRow["Assignto"].ToString().Trim());
            cmd.SetParameterValue("@IssueStatus", drRow["IssueStatus"].ToString().Trim());
            cmd.SetParameterValue("@IsIndemnify", drRow["IsIndemnify"].ToString().Trim());
            cmd.SetParameterValue("@IndemnifyPrice", drRow["IndemnifyPrice"].ToString().Trim());
            cmd.SetParameterValue("@TicketCode", drRow["TicketCode"].ToString().Trim());
            cmd.SetParameterValue("@TicketAmount", drRow["TicketAmount"].ToString().Trim());
            cmd.SetParameterValue("@RelatedType", drRow["RelatedType"].ToString().Trim());
            cmd.SetParameterValue("@RelatedID", drRow["RelatedID"].ToString().Trim());
            cmd.SetParameterValue("@Remark", drRow["Remark"].ToString().Trim());
            cmd.SetParameterValue("@CreateUser", drRow["CreateUser"].ToString().Trim());
            cmd.SetParameterValue("@CreateTime", dtNow);
            return cmd.ExecuteNonQuery();
        }

        public static int UpdateIssueData(DataRow drRow)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateIssueData");
            cmd.SetParameterValue("@IssueID", drRow["IssueID"].ToString().Trim());
            cmd.SetParameterValue("@Title", drRow["Title"].ToString().Trim());
            return cmd.ExecuteNonQuery();
        }

        public static DataSet AutoListSelect(AutoIssueCreateEntity automsgcancelordEntity)
        {
            DataSet dsResult = DbManager.Query("AutoIssueCreate", "AutoListNewSelect", false);
            return dsResult;
        }

        public static DataSet AutoListSelectHotel(AutoIssueCreateEntity automsgcancelordEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("StartDt",OracleType.VarChar),
                                    new OracleParameter("EndDt",OracleType.VarChar)
                                };
            parm[0].Value = DateTime.Now.AddDays(-1).ToShortDateString() + " 04:00:00";
            parm[1].Value = DateTime.Now.ToShortDateString() + " 04:00:00";

            DataSet dsResult = DbManager.Query("AutoIssueCreate", "AutoListSelectHotel", false, parm);
            return dsResult;
        }

        public static DataSet AutoTodaySelect()
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetIssueTodayList");
            return cmd.ExecuteDataSet();
        }

        public static DataSet GetMailToList(string strUserID)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetMailToList");
            cmd.SetParameterValue("@UserID", strUserID);
            return cmd.ExecuteDataSet();
        }
    }
}