using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Configuration;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;


namespace HotelVp.JobConsole.DataAccess
{
    public abstract class PushMsgDA
    {
        public static DataSet GetPushMsgTaskInfo(string TaskID)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetPushMsgTaskList");
            cmd.SetParameterValue("@TaskID", TaskID);
            return cmd.ExecuteDataSet();
        }


        public static DataSet GetPushMsgList(string TaskID, string PushType, string PushUsers)
        {
            DataSet dsPushMsg = new DataSet();
            if (!UpdateSendPushStatus(TaskID, "2", "1"))
            {
                return dsPushMsg;
            }

            if ("0".Equals(PushType))
            {
                dsPushMsg = GetPushMsgByAllUser();
            }
            else if ("1".Equals(PushType))
            {
                dsPushMsg = GetPushMsgByUserGroup(PushUsers);
            }
            else if ("2".Equals(PushType))
            {
                //CommonDA.InsertEventHistory("Insert freek开始");
                SetPushMsgByFileUpload(PushUsers, TaskID);
                //CommonDA.InsertEventHistory("Insert freek完成");
                //CommonDA.InsertEventHistory("select freek user开始");
                dsPushMsg = GetPushMsgByFileUpLoad(TaskID);
                //CommonDA.InsertEventHistory("select freek user完成");
            }

            return dsPushMsg;
        }

        public static DataSet GetPushMsgByAllUser()
        {
            DataSet dsResult = new DataSet();
            dsResult = HotelVp.Common.DBUtility.DbManager.Query("PushMsg", "t_lm_user_all_list", false);
            return dsResult;
        }

        public static DataSet GetPushMsgByUserGroup(string userGroups)
        {
            string strUserGroupList = string.Empty;
            string[] userGroupList = userGroups.Split(',');

            foreach (string strTemp in userGroupList)
            {
                if (String.IsNullOrEmpty(strTemp.Trim()))
                {
                    continue;
                }
                strUserGroupList = strUserGroupList + strTemp.Trim().Substring(1, strTemp.IndexOf(']') - 1) + ",";
            }

            DataSet dsResult = new DataSet();
            OracleParameter[] lmParm ={
                                    new OracleParameter("UserGroupList",OracleType.VarChar)
                                };

            lmParm[0].Value = strUserGroupList;
            dsResult = HotelVp.Common.DBUtility.DbManager.Query("PushMsg", "t_lm_user_group_list", false, lmParm);
            return dsResult;
        }

        public static DataSet GetPushMsgByFileUpLoad(string taskID)
        {
            DataSet dsResult = new DataSet();
            OracleParameter[] lmParm ={
                                    new OracleParameter("TaskID",OracleType.VarChar)
                                };

            lmParm[0].Value = taskID;
            dsResult = HotelVp.Common.DBUtility.DbManager.Query("PushMsg", "t_lm_user_files_list", false, lmParm);
            return dsResult;
        }

        public static int ClearTempPushTelPhone(string taskID)
        {
            DataSet dsResult = new DataSet();
            OracleParameter[] lmParm ={
                                    new OracleParameter("TaskID",OracleType.VarChar)
                                };

            lmParm[0].Value = taskID;
            return HotelVp.Common.DBUtility.DbManager.ExecuteSql("PushMsg", "t_lm_user_files_clear", lmParm);
        }

        public static void SetPushMsgByFileUpload(string fileName, string taskID)
        {
            string strFilePath = System.IO.Path.Combine(ConfigurationManager.AppSettings["UploadFilePath"].ToString(), fileName);
            DataTable dtResult = new DataTable();

            if (!System.IO.File.Exists(strFilePath))
            {
                return;
            }

            dtResult = LoadExcelToDataTable(strFilePath);

            if (dtResult.Rows.Count >0)
            {
                string strSQL = XmlSqlAnalyze.GotSqlTextFromXml("PushMsg", "insert_lm_user_group_list");
                int iCount = 0;
                int MaxLength = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxLength"].ToString())) ? 1000 : int.Parse(ConfigurationManager.AppSettings["MaxLength"].ToString());
                List<CommandInfo> cmdList = new List<CommandInfo>();

                foreach (DataRow drUser in dtResult.Rows)
                {
                    CommandInfo cminfo = new CommandInfo();
                    cminfo.CommandText = strSQL;
                    OracleParameter[] lmParm ={
                                    new OracleParameter("TelPhone",OracleType.VarChar),
                                    new OracleParameter("TaskID",OracleType.VarChar)
                                };

                    lmParm[0].Value = drUser[0].ToString();
                    lmParm[1].Value = taskID;
                    cminfo.Parameters = lmParm;
                    cmdList.Add(cminfo);
                    iCount = iCount + 1;
                    if (MaxLength == iCount)
                    {
                        try
                        {
                            DbHelperOra.ExecuteSqlTran(cmdList);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        iCount = 0;
                        cmdList.Clear();
                    }
                }

                if (iCount > 0)
                {
                    try
                    {
                        DbHelperOra.ExecuteSqlTran(cmdList);
                    }
                    catch
                    {
                    }
                }
            }
        }

        public static DataTable LoadExcelToDataTable(string filename)
        {
            DataTable dtResult = new DataTable();
            //连接字符串  
            string sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties=Excel 12.0;";

            //string sConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + @";Extended Properties=""Excel 12.0;HDR=YES;""";

            //String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filename + ";" + "Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
            OleDbConnection myConn = new OleDbConnection(sConnectionString);
            myConn.Open();
            DataTable sheetNames = myConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            string sheetName = (sheetNames.Rows.Count > 0) ? sheetNames.Rows[0][2].ToString() : "";

            if (String.IsNullOrEmpty(sheetName))
            {
                return dtResult;
            }
            string strCom = " SELECT * FROM [" + sheetName + "]";

            OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn);
            myCommand.Fill(dtResult);
            myConn.Close();
            return dtResult;
        }

        public static bool UpdateSendPushStatus(string taskID, string status, string oldStatus)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("UpdateSendPushStatus");
            cmd.SetParameterValue("@TaskID", taskID);
            cmd.SetParameterValue("@Status", status);
            cmd.SetParameterValue("@OldStatus", oldStatus);
            if (cmd.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int InsertPushPlanActionHistory(string taskID, string telPhone, string deviceToken, string result)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertPushPlanActionHistory");
            cmd.SetParameterValue("@TaskID", taskID);
            cmd.SetParameterValue("@TelPhone", telPhone);
            cmd.SetParameterValue("@DeviceToken", deviceToken);
            cmd.SetParameterValue("@Result", result);
            cmd.ExecuteNonQuery();

            if ("发送成功".Equals(result))
            {
                DataCommand sucCmd = DataCommandManager.GetDataCommand("UpdatePushPlanSuccCount");
                sucCmd.SetParameterValue("@TaskID", taskID);
                sucCmd.ExecuteNonQuery();
            }

            return 1;
        }

        public static bool CheckPushPlanActionHistory(string taskID, string deviceToken)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("CheckPushPlanActionHistory");
            cmd.SetParameterValue("@TaskID", taskID);
            cmd.SetParameterValue("@DeviceToken", deviceToken);
            DataSet dsResult = cmd.ExecuteDataSet();

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}