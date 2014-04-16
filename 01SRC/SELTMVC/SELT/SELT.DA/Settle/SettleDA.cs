using System;
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
using HotelVp.SELT.Domain.Entity;

namespace HotelVp.SELT.Domain.DA
{
    public abstract class SettleDA
    {
        public static SettleEntity ImportCollectList(SettleEntity settleEntity)
        {
            SettleDBEntity dbParm = (settleEntity.SettleDBEntity.Count > 0) ? settleEntity.SettleDBEntity[0] : new SettleDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("ImportCollectList");
            cmd.SetParameterValue("@PAYMENTTYPE", dbParm.PaymentType);
            //cmd.SetParameterValue("@TOTALAMOUNT", dbParm.TotalUploadCount);
            cmd.SetParameterValue("@UPLOADFILENAME", dbParm.UploadFileName);
            cmd.SetParameterValue("@IMPORTTYPE", dbParm.ImportType);
            cmd.SetParameterValue("@TOTALCOUNT", dbParm.TotalUploadCount);
            cmd.SetParameterValue("@CREATEUSER", settleEntity.LogMessages.Userid);

            settleEntity.Result = cmd.ExecuteNonQuery();
            int collectID = (int)cmd.GetParameterValue("@CollectID");
            decimal decTotalUploadAmount = 0;

            foreach (DataRow drRow in dbParm.UpLoadList.Rows)
            {
                DataCommand unitCmd = DataCommandManager.GetDataCommand("ImportWriteOffList");
                unitCmd.SetParameterValue("@COLLECTID", collectID);
                unitCmd.SetParameterValue("@DEALTIME", drRow[1].ToString().Trim());
                unitCmd.SetParameterValue("@PAYNAME", drRow[8].ToString().Trim());
                unitCmd.SetParameterValue("@PAYACCOUNT", drRow[9].ToString().Trim());
                unitCmd.SetParameterValue("@INTOAMOUNT", drRow[5].ToString().Trim());
                unitCmd.SetParameterValue("@DETAILSERIALNUM", drRow[12].ToString().Trim());
                unitCmd.SetParameterValue("@SUMMARY", drRow[10].ToString().Trim());
                unitCmd.SetParameterValue("@REMARK", drRow[11].ToString().Trim());
                unitCmd.SetParameterValue("@CREATEUSER", settleEntity.LogMessages.Userid);
                unitCmd.ExecuteNonQuery();
                decTotalUploadAmount = decTotalUploadAmount + (String.IsNullOrEmpty(drRow[5].ToString().Trim()) ? 0 : decimal.Parse(drRow[5].ToString().Trim()));

            }

            DataCommand upCmd = DataCommandManager.GetDataCommand("UpdateCollectList");
            upCmd.SetParameterValue("@COLLECTID", collectID);
            upCmd.SetParameterValue("@TOTALAMOUNT", decTotalUploadAmount);
            settleEntity.Result = cmd.ExecuteNonQuery();
            return settleEntity;
        }

        //public static SettleEntity AddCollectItem(SettleEntity settleEntity)
        //{
        //    DataCommand unitCmd = DataCommandManager.GetDataCommand("AddCollectItem");
        //    //unitCmd.SetParameterValue("@", dbParm.);
        //    //unitCmd.SetParameterValue("@", dbParm.);
        //    //unitCmd.SetParameterValue("@", dbParm.);
        //    //unitCmd.SetParameterValue("@", dbParm.);
        //    //unitCmd.SetParameterValue("@", dbParm.);
        //    settleEntity.Result = unitCmd.ExecuteNonQuery();
        //    return settleEntity;
        //}

        public static SettleEntity ApproveCollectList(SettleEntity settleEntity)
        {
            SettleDBEntity dbParm = (settleEntity.SettleDBEntity.Count > 0) ? settleEntity.SettleDBEntity[0] : new SettleDBEntity();
            string strErr = string.Empty;

            // 0: 批量导入  1： 手工导入
            if ("0".Equals(dbParm.ImportActionType))
            {
                // 初次导入上传信息  只需要判断是否有重复导入
                //CheckWriteOffReset
                foreach (DataRow drRow in dbParm.ImportList.Rows)
                {
                    strErr = CheckWriteOffReset(drRow[0].ToString(), dbParm.CollectID);
                    if (!String.IsNullOrEmpty(strErr))
                    {
                        settleEntity.ErrorMSG = settleEntity.ErrorMSG + strErr;
                    }
                }

                if (!String.IsNullOrEmpty(settleEntity.ErrorMSG))
                {
                    settleEntity.ErrorMSG = "下列收款记录已经导入，请修改。" + "<br/>" + settleEntity.ErrorMSG;
                    settleEntity.Result = 2;
                    return settleEntity;
                }
            }
            else
            {
                // 调整导入  判断 当去除勾选 需判断这笔回款是否已经被销账。已销账就不可去除。 状态=2 为已经销账  0:导入 1:销账
                //                当勾选 需判断是否有重复导入 有重复导入不可再导入。
                // 页面做处理，传值过来的数据，仅仅为在 页面上数据状态变化的数据。没有变化的数据不要传递。
                foreach (DataRow drRow in dbParm.ImportList.Rows)
                {
                    // 状态 1 - > 0 只需判断是否销账
                    if ("0".Equals(drRow[1].ToString()))
                    {
                        strErr = CheckWriteOffDetail(drRow[0].ToString(), dbParm.CollectID);
                        if (!String.IsNullOrEmpty(strErr))
                        {
                            settleEntity.ErrorMSG = settleEntity.ErrorMSG + "已销账不能重新调整:" + strErr;
                        }
                    }
                    else
                    {
                        strErr = CheckWriteOffReset(drRow[0].ToString(), dbParm.CollectID);
                        if (!String.IsNullOrEmpty(strErr))
                        {
                            settleEntity.ErrorMSG = settleEntity.ErrorMSG + "重复导入:" + strErr;
                        }
                    }
                }

                if (!String.IsNullOrEmpty(settleEntity.ErrorMSG))
                {
                    settleEntity.Result = 2;
                    return settleEntity;
                }
            }

            foreach (DataRow drRow in dbParm.ImportList.Rows)
            {

                DataCommand unitCmd = DataCommandManager.GetDataCommand("ApproveCollectList");
                unitCmd.SetParameterValue("@COLLECTID", dbParm.CollectID);
                unitCmd.SetParameterValue("@WRITEOFFID", drRow[0].ToString());
                unitCmd.SetParameterValue("@INTOSTATUS", drRow[1].ToString());
                unitCmd.SetParameterValue("@INTODATE", drRow[2].ToString());//DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                unitCmd.ExecuteNonQuery();
            }
            return settleEntity;
        }

        public static SettleEntity ApproveAutoCompleteCollectList(SettleEntity settleEntity)
        {
            SettleDBEntity dbParm = (settleEntity.SettleDBEntity.Count > 0) ? settleEntity.SettleDBEntity[0] : new SettleDBEntity();
            DataCommand Cmd = DataCommandManager.GetDataCommand("GetAutoCompleteCollectList");
            Cmd.SetParameterValue("@COLLECTID", dbParm.CollectID);
            DataSet dsResult = Cmd.ExecuteDataSet();

            if (dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
            {
                return settleEntity;
            }

            string[] flMatch = new string[2];
            foreach (DataRow drRow in dsResult.Tables[0].Rows)
            {
                // 追加自动筛选匹配逻辑    
                // 筛选匹配值  已账单ID + '|'风格  例如：001|030|012|422
                // 筛选匹配状态  0100  已0和1表示是否匹配
                flMatch = GetWriteOffAutoCompleteVal(drRow["WRITEOFFID"].ToString(), dbParm.CollectID);
                DataCommand unitCmd = DataCommandManager.GetDataCommand("UpdateAutoCompleteCollectList");
                unitCmd.SetParameterValue("@COLLECTID", dbParm.CollectID);
                unitCmd.SetParameterValue("@WRITEOFFID", drRow[0].ToString());
                unitCmd.SetParameterValue("@FILTERMATCHID", flMatch[0]);
                unitCmd.SetParameterValue("@FILTERMATCHSTATUS", flMatch[1]);
                unitCmd.ExecuteNonQuery();
            }
            return settleEntity;
        }

        public static string[] GetWriteOffAutoCompleteVal(string WriteOffID, string CollectID)
        {
            string[] flMatch = new string[2];
            string strKey = string.Empty;
            string strStatus = string.Empty;
            string strSLID = string.Empty;
            DataCommand CmdKey1 = DataCommandManager.GetDataCommand("GetWriteOffAutoCompleteValKey1");
            CmdKey1.SetParameterValue("@COLLECTID", CollectID);
            CmdKey1.SetParameterValue("@WRITEOFFID", WriteOffID);
            DataSet dsKey1 = CmdKey1.ExecuteDataSet();
            if (dsKey1.Tables.Count > 0 && dsKey1.Tables[0].Rows.Count > 0)
            {
                strSLID = dsKey1.Tables[0].Rows[0]["SLID"].ToString();
                strKey = strKey + dsKey1.Tables[0].Rows[0]["SLID"].ToString() + "|";
                strStatus = strStatus + "1";
            }
            else 
            {
                strKey = strKey + "|";
                strStatus = strStatus + "0";
            }

            DataCommand CmdKey2 = DataCommandManager.GetDataCommand("GetWriteOffAutoCompleteValKey2");
            CmdKey2.SetParameterValue("@COLLECTID", CollectID);
            CmdKey2.SetParameterValue("@WRITEOFFID", WriteOffID);
            CmdKey2.SetParameterValue("@SLID", strSLID);
            DataSet dsKey2 = CmdKey2.ExecuteDataSet();
            if (dsKey2.Tables.Count > 0 && dsKey2.Tables[0].Rows.Count > 0)
            {
                strSLID = "''" + strSLID + "'',''" + dsKey2.Tables[0].Rows[0]["SLID"].ToString() + "''";
                strKey = strKey + dsKey2.Tables[0].Rows[0]["SLID"].ToString() + "|";
                strStatus = strStatus + "1";
            }
            else
            {
                strKey = strKey + "|";
                strStatus = strStatus + "0";
            }

            DataCommand CmdKey3 = DataCommandManager.GetDataCommand("GetWriteOffAutoCompleteValKey3");
            CmdKey3.SetParameterValue("@COLLECTID", CollectID);
            CmdKey3.SetParameterValue("@WRITEOFFID", WriteOffID);
            CmdKey3.SetParameterValue("@SLID", strSLID);
            DataSet dsKey3 = CmdKey3.ExecuteDataSet();
            if (dsKey3.Tables.Count > 0 && dsKey3.Tables[0].Rows.Count > 0)
            {
                strSLID = strSLID + ",''" + dsKey3.Tables[0].Rows[0]["SLID"].ToString() + "''";
                strKey = strKey + dsKey3.Tables[0].Rows[0]["SLID"].ToString() + "|";
                strStatus = strStatus + "1";
            }
            else
            {
                strKey = strKey + "|";
                strStatus = strStatus + "0";
            }

            DataCommand CmdKey4 = DataCommandManager.GetDataCommand("GetWriteOffAutoCompleteValKey4");
            CmdKey4.SetParameterValue("@COLLECTID", CollectID);
            CmdKey4.SetParameterValue("@WRITEOFFID", WriteOffID);
            CmdKey4.SetParameterValue("@SLID", strSLID);
            DataSet dsKey4 = CmdKey4.ExecuteDataSet();
            if (dsKey4.Tables.Count > 0 && dsKey4.Tables[0].Rows.Count > 0)
            {
                strKey = strKey + dsKey4.Tables[0].Rows[0]["SLID"].ToString();
                strStatus = strStatus + "1";
            }
            else
            {
                strStatus = strStatus + "0";
            }

            flMatch[0] = strKey;
            flMatch[1] = strStatus;
            return flMatch;
        }

        public static string CheckWriteOffReset(string WriteOffID, string CollectID)
        {
            DataCommand unitCmd = DataCommandManager.GetDataCommand("CheckWriteOffReset");
            unitCmd.SetParameterValue("@COLLECTID", CollectID);
            unitCmd.SetParameterValue("@WRITEOFFID", WriteOffID);
            DataSet dsResult = unitCmd.ExecuteDataSet();
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return dsResult.Tables[0].Rows[0]["PAYNAME"].ToString() + "-" + dsResult.Tables[0].Rows[0]["PAYACCOUNT"].ToString() + "-" + dsResult.Tables[0].Rows[0]["INTOAMOUNT"].ToString() + "-" + dsResult.Tables[0].Rows[0]["DETAILSERIALNUM"].ToString() + "<br/>";
            }
            return "";
        }

        public static string CheckWriteOffDetail(string WriteOffID, string CollectID)
        {
            DataCommand unitCmd = DataCommandManager.GetDataCommand("GetWriteOFFColDetail");
            unitCmd.SetParameterValue("@COLLECTID", CollectID);
            unitCmd.SetParameterValue("@WRITEOFFID", WriteOffID);
            DataSet dsResult = unitCmd.ExecuteDataSet();
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return dsResult.Tables[0].Rows[0]["PAYNAME"].ToString() + "-" + dsResult.Tables[0].Rows[0]["PAYACCOUNT"].ToString() + "-" + dsResult.Tables[0].Rows[0]["INTOAMOUNT"].ToString() + "-" + dsResult.Tables[0].Rows[0]["DETAILSERIALNUM"].ToString() + "<br/>";
            }
            return "";
        }

        public static SettleEntity GetImportCollectList(SettleEntity settleEntity)
        {
            SettleDBEntity dbParm = (settleEntity.SettleDBEntity.Count > 0) ? settleEntity.SettleDBEntity[0] : new SettleDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetImportCollectList");

            cmd.SetParameterValue("@UploadStartDT", dbParm.UploadStartDT);
            cmd.SetParameterValue("@UploadEndDT", dbParm.UploadEndDT);
            cmd.SetParameterValue("@UploadAmountStart", dbParm.UploadAmountStart);
            cmd.SetParameterValue("@UploadAmountEnd", dbParm.UploadAmountEnd);
            cmd.SetParameterValue("@UploadUser", dbParm.UploadUser);

            cmd.SetParameterValue("@PageCurrent", settleEntity.PageCurrent);
            cmd.SetParameterValue("@PageSize", settleEntity.PageSize);
            cmd.SetParameterValue("@SortField", settleEntity.SortField);
            cmd.SetParameterValue("@SortType", settleEntity.SortType);

            settleEntity.SettleDBEntity[0].UpLoadList = cmd.ExecuteDataSet().Tables[0].Copy();
            settleEntity.TotalCount = (int)cmd.GetParameterValue("@TotalCount");

            return settleEntity;
        }

        public static SettleEntity GetApproveCollectList(SettleEntity settleEntity)
        {
            SettleDBEntity dbParm = (settleEntity.SettleDBEntity.Count > 0) ? settleEntity.SettleDBEntity[0] : new SettleDBEntity();
            DataCommand unitCmd = DataCommandManager.GetDataCommand("GetApproveCollectList");
            unitCmd.SetParameterValue("@COLLECTID", dbParm.CollectID);
            settleEntity.SettleDBEntity[0].ImportList  = unitCmd.ExecuteDataSet().Tables[0].Copy();
            return settleEntity;
        }
    }
}
