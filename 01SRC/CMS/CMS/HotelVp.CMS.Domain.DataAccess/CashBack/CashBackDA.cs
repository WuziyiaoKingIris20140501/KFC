using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;
using HotelVp.CMS.Domain.Entity;

//using HotelVp.CMS.Domain.Resource;

namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class CashBackDA
    {
        public static CashBackEntity BindOrderInfo(CashBackEntity cashBackEntity)
        {
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            OracleParameter[] parm ={
                                    new OracleParameter("ORDERNO",OracleType.VarChar)
                                };
            parm[0].Value = dbParm.OrderNo;

            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_order_info_list_by_order", false, parm);

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                DataSet dsCash = GetCashBackByOrderNoStatus(dbParm.OrderNo);

                if (dsCash.Tables.Count > 0 && dsCash.Tables[0].Rows.Count > 0)
                {
                    dsResult.Tables[0].Rows[0]["CashTaskStatus"] = dsCash.Tables[0].Rows[0]["status"].ToString();
                    dsResult.Tables[0].Rows[0]["CashSN"] = dsCash.Tables[0].Rows[0]["CashSN"].ToString(); 
                }
                else
                {
                    dsResult.Tables[0].Rows[0]["CashTaskStatus"] = "2";
                    dsResult.Tables[0].Rows[0]["CashSN"] = "";
                }
            }

            cashBackEntity.QueryResult = dsResult;
            return cashBackEntity;
        }

        public static CashBackEntity BindOrderInfoByUser(CashBackEntity cashBackEntity)
        {
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            OracleParameter[] parm ={
                                    new OracleParameter("USERID",OracleType.VarChar)
                                };
            parm[0].Value = dbParm.UserID;
            cashBackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_order_info_list_by_user", false, parm);
            return cashBackEntity;
        }

        public static CashBackEntity UpdateCashBackRequest(CashBackEntity cashBackEntity)
        {
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            OracleParameter[] parm ={
                                    new OracleParameter("SN",OracleType.VarChar),
                                    new OracleParameter("BANKNAME",OracleType.VarChar),
                                    new OracleParameter("BANKBRANCH",OracleType.VarChar),
                                    new OracleParameter("BANKCARDNUMBER",OracleType.VarChar),
                                    new OracleParameter("BANKCARDOWNER",OracleType.VarChar),
                                    new OracleParameter("PHONENUMBER",OracleType.VarChar),
                                    new OracleParameter("ALIPAYACCOUNT",OracleType.VarChar),
                                    new OracleParameter("REMARK",OracleType.VarChar),
                                    new OracleParameter("APPLYTYPE",OracleType.VarChar),
                                    new OracleParameter("USERNAME",OracleType.VarChar)
                                };
            parm[0].Value = dbParm.ID;

            if ("0".Equals(dbParm.BackCashType))
            {
                parm[1].Value = dbParm.BankName;
                parm[2].Value = dbParm.BankBranch;
                parm[3].Value = dbParm.BankCardNumber;
                parm[4].Value = dbParm.BankOwner;

                parm[5].Value = DBNull.Value;
                parm[6].Value = DBNull.Value;
                parm[9].Value = DBNull.Value;
            }
            else if ("1".Equals(dbParm.BackCashType))
            {
                parm[1].Value = DBNull.Value;
                parm[2].Value = DBNull.Value;
                parm[3].Value = DBNull.Value;
                parm[4].Value = DBNull.Value;

                parm[5].Value = dbParm.BackTel;

                parm[6].Value = DBNull.Value;
                parm[9].Value = DBNull.Value;
            }
            else if ("2".Equals(dbParm.BackCashType))
            {
                parm[1].Value = DBNull.Value;
                parm[2].Value = DBNull.Value;
                parm[3].Value = DBNull.Value;
                parm[4].Value = DBNull.Value;
                parm[5].Value = DBNull.Value;

                parm[6].Value = dbParm.BackBao;
                parm[9].Value = dbParm.BackBaoName;
            }

            parm[7].Value = dbParm.Remark;

            if ("0".Equals(dbParm.BackCashType))
            {
                parm[8].Value = "1";
            }
            else if ("1".Equals(dbParm.BackCashType))
            {
                parm[8].Value = "3";
            }
            else if ("2".Equals(dbParm.BackCashType))
            {
                parm[8].Value = "2";
            }
            
            DbManager.ExecuteSql("CashBack", "t_lm_cash_info_update", parm);
            return cashBackEntity;
        }

        public static CashBackEntity UpdateCashBackHis(CashBackEntity cashBackEntity)
        {
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            OracleParameter[] parm ={
                                    new OracleParameter("SN",OracleType.VarChar)
                                };
            parm[0].Value = dbParm.ID;
            DbManager.ExecuteSql("CashBack", "t_lm_cash_his_info_update", parm);
            return cashBackEntity;
        }

        public static bool ChkBackCashVal(CashBackEntity cashBackEntity)
        {
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            OracleParameter[] parm ={
                                    new OracleParameter("USERID",OracleType.VarChar)
                                };
            parm[0].Value = dbParm.UserID;
            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_order_info_list_by_user", false, parm);
            decimal decCan = 0;
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                decCan = String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["can_applictaion_amount"].ToString()) ? 0 : decimal.Parse(dsResult.Tables[0].Rows[0]["can_applictaion_amount"].ToString());
            }
            decimal decAmount = String.IsNullOrEmpty(dbParm.BackCashAmount) ? 0 : decimal.Parse(dbParm.BackCashAmount);

            if (decCan >= decAmount)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public static DataSet GetCashBackByOrderNoStatus(string orderNo)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetCashBackByOrderNoHistory");
            cmd.SetParameterValue("@OrderNo", orderNo);
            DataSet dsResult = cmd.ExecuteDataSet();

            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return GetCashBackStatus(dsResult.Tables[0].Rows[0]["Cash_SN"].ToString());
            }
            else
            {
                return new DataSet();
            }
        }

        public static DataSet GetCashBackStatus(string CashSN)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CashSN",OracleType.VarChar)
                                };
            parm[0].Value = CashSN;

            return HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_cash_status", false, parm);

            //if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            //{
            //    return dsResult.Tables[0].Rows[0][0].ToString();
            //}
            //else
            //{
            //    return "2";
            //}
        }

        public static int InsertCashBackHistory(CashBackEntity cashBackEntity)
        {
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertCashBackHistory");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@OrderNo", dbParm.OrderNo);
            cmd.SetParameterValue("@UserID", dbParm.UserID);
            cmd.SetParameterValue("@CashSN", dbParm.ID);
            cmd.SetParameterValue("@CreateUser", cashBackEntity.LogMessages.Username);
            return cmd.ExecuteNonQuery();
        }












        public static CashBackEntity CommonHotelGroupSelect(CashBackEntity cashBackEntity)
        {
            cashBackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_common_hotelgrouplist", false);
            return cashBackEntity;
        }

        public static CashBackEntity CommonCitySelect(CashBackEntity cashBackEntity)
        {
            cashBackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_common_citylist", false);
            return cashBackEntity;
        }

        public static CashBackEntity CommonProvincialSelect(CashBackEntity cashBackEntity)
        {
            cashBackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_common_provinciallist", false);
            return cashBackEntity;
        }
        
        public static int HotelSave(CashBackEntity cashBackEntity)
        {
            if (cashBackEntity.CashBackDBEntity.Count == 0)
            {
                return 0;
            }

            if (cashBackEntity.LogMessages == null)
            {
                return 0;
            }

            string strSQLString = "";
            if (CheckInsert(cashBackEntity) > 0)
            {
                strSQLString = "t_lm_b_hotelinfo_update";
            }
            else
            {
                strSQLString = "t_lm_b_hotelinfo_insert";
            }

            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();

            OracleParameter[] lmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("HOTELNM",OracleType.VarChar),
                                    new OracleParameter("GROUPID",OracleType.VarChar),
                                    new OracleParameter("STARRATING",OracleType.VarChar),
                                    new OracleParameter("DIAMONDRATING",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("OPENDATE",OracleType.VarChar),
                                    new OracleParameter("REPAIRDATE",OracleType.VarChar),
                                    new OracleParameter("ADDRESS",OracleType.VarChar),
                                    new OracleParameter("WEBSITE",OracleType.VarChar),
                                    //new OracleParameter("PHONE",OracleType.VarChar),
                                    //new OracleParameter("FAX",OracleType.VarChar),
                                    //new OracleParameter("CONTACTPER",OracleType.VarChar),
                                    //new OracleParameter("CONTACTEMAIL",OracleType.VarChar),
                                    new OracleParameter("SIMPLEDESCZH",OracleType.VarChar),
                                    new OracleParameter("DESCZH",OracleType.VarChar),
                                    new OracleParameter("EVALUATION",OracleType.VarChar),
                                    new OracleParameter("ONLINESTATUS",OracleType.VarChar),
                                    new OracleParameter("AUTOTRUST",OracleType.VarChar),
                                    new OracleParameter("FOGSTATUS",OracleType.VarChar),
                                    new OracleParameter("HOTELNMEN",OracleType.VarChar),
                                    new OracleParameter("LONGITUDE",OracleType.VarChar),
                                    new OracleParameter("LATITUDE",OracleType.VarChar)
                                };

            lmParm[0].Value = dbParm.HotelID;
            lmParm[1].Value = dbParm.Name_CN;
            lmParm[2].Value = dbParm.HotelGroup;
            lmParm[3].Value = dbParm.StarRating;
            lmParm[4].Value = dbParm.DiamondRating;
            lmParm[5].Value = dbParm.City;
            lmParm[6].Value = dbParm.OpenDate;
            lmParm[7].Value = dbParm.RepairDate;
            if (String.IsNullOrEmpty(dbParm.AddRess))
            {
                lmParm[8].Value = DBNull.Value;
            }
            else
            {
                lmParm[8].Value = dbParm.AddRess;
            }

            if (String.IsNullOrEmpty(dbParm.WebSite))
            {
                lmParm[9].Value = DBNull.Value;
            }
            else
            {
                lmParm[9].Value = dbParm.WebSite;
            }

            //if (String.IsNullOrEmpty(dbParm.Phone))
            //{
            //    lmParm[10].Value = DBNull.Value;
            //}
            //else
            //{
            //    lmParm[10].Value = dbParm.Phone;
            //}

            //if (String.IsNullOrEmpty(dbParm.Fax))
            //{
            //    lmParm[11].Value = DBNull.Value;
            //}
            //else
            //{
            //    lmParm[11].Value = dbParm.Fax;
            //}

            //if (String.IsNullOrEmpty(dbParm.ContactPer))
            //{
            //    lmParm[12].Value = DBNull.Value;
            //}
            //else
            //{
            //    lmParm[12].Value = dbParm.ContactPer;
            //}

            //if (String.IsNullOrEmpty(dbParm.ContactEmail))
            //{
            //    lmParm[13].Value = DBNull.Value;
            //}
            //else
            //{
            //    lmParm[13].Value = dbParm.ContactEmail;
            //}

            if (String.IsNullOrEmpty(dbParm.SimpleDescZh))
            {
                lmParm[10].Value = DBNull.Value;
            }
            else
            {
                lmParm[10].Value = dbParm.SimpleDescZh;
            }

            if (String.IsNullOrEmpty(dbParm.DescZh))
            {
                lmParm[11].Value = DBNull.Value;
            }
            else
            {
                lmParm[11].Value = dbParm.DescZh;
            }

            if (String.IsNullOrEmpty(dbParm.Evaluation))
            {
                lmParm[12].Value = DBNull.Value;
            }
            else
            {
                lmParm[12].Value = dbParm.Evaluation;
            }

            if (String.IsNullOrEmpty(dbParm.Status))
            {
                lmParm[13].Value = DBNull.Value;
            }
            else
            {
                lmParm[13].Value = dbParm.Status;
            }

            lmParm[14].Value = dbParm.AutoTrust;

            lmParm[15].Value = dbParm.FogStatus;

            if (String.IsNullOrEmpty(dbParm.Name_EN))
            {
                lmParm[16].Value = DBNull.Value;
            }
            else
            {
                lmParm[16].Value = dbParm.Name_EN;
            }

            lmParm[17].Value = dbParm.Longitude;

            lmParm[18].Value = dbParm.Latitude;

            DbManager.ExecuteSql("CashBack", strSQLString, lmParm);

            return 1;
        }

        public static CashBackEntity BindHotelList(CashBackEntity cashBackEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            parm[0].Value = dbParm.HotelID;

            cashBackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_hotelinfo_bind", false, parm);
            return cashBackEntity;
        }

        public static int UpdateHotelSalesUserAccount(string HotelID, string UserAccount)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("USERCODE",OracleType.VarChar)
                                };
            parm[0].Value = HotelID;
            parm[1].Value = UserAccount;
            return HotelVp.Common.DBUtility.DbManager.ExecuteSql("CashBack", "t_lm_b_hotelinfo_sales_update", parm);
        }

        public static CashBackEntity ChkLMPropHotelList(CashBackEntity cashBackEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            parm[0].Value = dbParm.HotelID;

            cashBackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_hotelinfo_prop_chk", false, parm);
            return cashBackEntity;
        }

        public static CashBackEntity ChkLMPropHotelExam(CashBackEntity cashBackEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            parm[0].Value = dbParm.HotelID;

            cashBackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_hotelinfo_prop_exam_chk", false, parm);
            return cashBackEntity;
        }

        public static CashBackEntity ReadFogHotelInfo(CashBackEntity cashBackEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            parm[0].Value = dbParm.HotelID;

            cashBackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_hotelinfo_fog", false, parm);
            return cashBackEntity;
        }

        public static CashBackEntity GetSalesManager(CashBackEntity cashBackEntity)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("GetSalesManagerHistory");
            cmd.SetParameterValue("@HotelID", cashBackEntity.CashBackDBEntity[0].HotelID);
            cashBackEntity.QueryResult = cmd.ExecuteDataSet();
            return cashBackEntity;
        }

        //public static CashBackEntity SelectTypeDetail(CashBackEntity cashBackEntity)
        //{
        //    OracleParameter[] parm ={
        //                            new OracleParameter("ID",OracleType.VarChar)
        //                        };
        //    CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
        //    parm[0].Value = dbParm.ID;

        //    cashBackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_hotel_facilities_detal", parm);
        //    return cashBackEntity;
        //}

        
        //public static CashBackEntity ServiceTypeSelect(CashBackEntity cashBackEntity)
        //{
        //    cashBackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_hotel_service");
        //    return cashBackEntity;
        //}

        

        //public static CashBackEntity FacilitiesTypeSelect(CashBackEntity cashBackEntity)
        //{
        //    cashBackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_hotel_facilities");
        //    return cashBackEntity;
        //}

        public static int CheckInsert(CashBackEntity cashBackEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            parm[0].Value = dbParm.HotelID;

            cashBackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_hotelinfo_insertcheck", false, parm);

            if (cashBackEntity.QueryResult.Tables.Count > 0 && cashBackEntity.QueryResult.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        //public static int Insert(CashBackEntity cashBackEntity)
        //{
        //    if (cashBackEntity.CashBackDBEntity.Count == 0)
        //    {
        //        return 0;
        //    }

        //    if (cashBackEntity.LogMessages == null)
        //    {
        //        return 0;
        //    }

        //    if (CheckInsert(cashBackEntity) > 0)
        //    {
        //        return 2;
        //    }

        //    CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();

        //    CommandInfo InsertLmPaymentInfo = new CommandInfo();
        //    OracleParameter[] lmParm ={
        //                            new OracleParameter("ID",OracleType.Number),
        //                            new OracleParameter("NAMECN",OracleType.VarChar),
        //                            new OracleParameter("TYPE",OracleType.VarChar),
        //                            new OracleParameter("CODE",OracleType.VarChar),
        //                        };

        //    lmParm[0].Value = getMaxIDfromSeq("t_lm_b_facilities_SEQ");
        //    lmParm[1].Value = dbParm.Name_CN;
        //    if (dbParm.Type.Equals("0"))
        //    {
        //        lmParm[2].Value = "S";
        //    }
        //    else
        //    {
        //        lmParm[2].Value = "F";
        //    }
        //    lmParm[3].Value = 'P' + lmParm[0].Value.ToString().PadLeft(4, '0');
        //    DbManager.ExecuteSql("CashBack", "t_lm_b_hotel_facilities_insert", lmParm);

        //    return 1;
        //}


        //public static int HotelInsert(CashBackEntity cashBackEntity)
        //{
        //    if (cashBackEntity.CashBackDBEntity.Count == 0)
        //    {
        //        return 0;
        //    }

        //    if (cashBackEntity.LogMessages == null)
        //    {
        //        return 0;
        //    }

        //    CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();

        //    List<CommandInfo> sqlList = new List<CommandInfo>();

        //    CommandInfo UpdateCsHotelInfo = new CommandInfo();
        //    OracleParameter[] lmParm ={
        //                            new OracleParameter("HOTELID",OracleType.VarChar)                         
        //                        };
        //    lmParm[0].Value = dbParm.HotelID;
        //    UpdateCsHotelInfo.SqlName = "CashBack";
        //    UpdateCsHotelInfo.SqlId = "t_lm_b_hotel_service_save_update";
        //    UpdateCsHotelInfo.Parameters = lmParm;
        //    sqlList.Add(UpdateCsHotelInfo);

            

        //    //OracleParameter[] lmParm ={
        //    //                        new OracleParameter("ID",OracleType.Number),
        //    //                        new OracleParameter("NAMECN",OracleType.VarChar)                               
        //    //                    };

        //    //lmParm[0].Value = getMaxIDfromSeq("t_lm_b_facilities_hotel_SEQ");
        //    //lmParm[1].Value = dbParm.Name_CN;
        //    //DbManager.ExecuteSql("CashBack", "t_lm_b_hotel_service_save", lmParm);
        //    DbManager.ExecuteSqlTran(sqlList);
        //    return 1;
        //}

        //public static int Update(CashBackEntity cashBackEntity)
        //{
        //    if (cashBackEntity.CashBackDBEntity.Count == 0)
        //    {
        //        return 0;
        //    }

        //    if (cashBackEntity.LogMessages == null)
        //    {
        //        return 0;
        //    }

        //    if (CheckUpdate(cashBackEntity) > 0)
        //    {
        //        return 2;
        //    }

        //    CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
        //    OracleParameter[] parm ={
        //                            new OracleParameter("ID",OracleType.Number),
        //                            new OracleParameter("NAMECN",OracleType.VarChar),
        //                            new OracleParameter("ONLINESTATUS",OracleType.VarChar)

        //                        };
        //    parm[0].Value = dbParm.ID;
        //    parm[1].Value = dbParm.Name_CN;
        //    parm[2].Value = dbParm.Status;
           
        //    DbManager.ExecuteSql("CashBack", "t_lm_b_hotel_facilities_update", parm);
        //    return 1;
        //}

        //public static int CheckUpdate(CashBackEntity cashBackEntity)
        //{
        //    OracleParameter[] parm ={
        //                            new OracleParameter("ID",OracleType.Number),
        //                            new OracleParameter("CHKNAME",OracleType.VarChar),
        //                            new OracleParameter("TYPE",OracleType.VarChar)                                
        //                        };

        //    CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();

        //    parm[0].Value = int.Parse(dbParm.ID.ToString());
        //    parm[1].Value = dbParm.Name_CN;
        //   if (dbParm.Type.Equals("0"))
        //    {
        //        parm[2].Value = "S";
        //    }
        //    else
        //    {
        //        parm[2].Value = "F";
        //    }
           
        //    cashBackEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_hotel_facilities_update_check", parm);
        //    if (cashBackEntity.QueryResult.Tables.Count > 0 && cashBackEntity.QueryResult.Tables[0].Rows.Count > 0)
        //    {
        //        return 1;
        //    }
        //    return 0;
        //}

        public static int UpdateHotelSalesList(CashBackEntity cashBackEntity)
        {
            if (cashBackEntity.CashBackDBEntity.Count == 0)
            {
                return 0;
            }

            if (cashBackEntity.LogMessages == null)
            {
                return 0;
            }

            if (!CheckUpdate(cashBackEntity))
            {
                return 2;
            }

            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            OracleParameter[] lmParm ={
                                   new OracleParameter("PHONE",OracleType.VarChar),
                                    new OracleParameter("FAX",OracleType.VarChar),
                                    new OracleParameter("CONTACTPER",OracleType.VarChar),
                                    new OracleParameter("CONTACTEMAIL",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                    //,
                                    //new OracleParameter("USERCODE",OracleType.VarChar)
                                };

            if (String.IsNullOrEmpty(dbParm.Phone))
            {
                lmParm[0].Value = DBNull.Value;
            }
            else
            {
                lmParm[0].Value = dbParm.Phone;
            }

            if (String.IsNullOrEmpty(dbParm.Fax))
            {
                lmParm[1].Value = DBNull.Value;
            }
            else
            {
                lmParm[1].Value = dbParm.Fax;
            }

            if (String.IsNullOrEmpty(dbParm.ContactPer))
            {
                lmParm[2].Value = DBNull.Value;
            }
            else
            {
                lmParm[2].Value = dbParm.ContactPer;
            }

            if (String.IsNullOrEmpty(dbParm.ContactEmail))
            {
                lmParm[3].Value = DBNull.Value;
            }
            else
            {
                lmParm[3].Value = dbParm.ContactEmail;
            }

            lmParm[4].Value = dbParm.HotelID;

            //if (String.IsNullOrEmpty(dbParm.SalesID))
            //{
            //    lmParm[5].Value = DBNull.Value;
            //}
            //else
            //{
            //    lmParm[5].Value = dbParm.SalesID;
            //}
            DbManager.ExecuteSql("CashBack", "t_lm_b_hotel_sales_update", lmParm);
            InsertSalesAndHistory(cashBackEntity);
            UpdateLMSalesHistory(cashBackEntity);
            return 1;
        }

        public static CashBackEntity GetSalesManagerSettingHistory(CashBackEntity cashBackEntity)
        {
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetSalesManagerSettingHistory");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cashBackEntity.QueryResult = cmd.ExecuteDataSet();
            return cashBackEntity;
        }

        public static int InsertSalesAndHistory(CashBackEntity cashBackEntity)
        {
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertSalesAndHistory");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@UserCode", dbParm.SalesID);
            cmd.SetParameterValue("@StartDTime", dbParm.SalesStartDT);
            cmd.SetParameterValue("@EndDTime", dbParm.SalesEndDT);
            cmd.SetParameterValue("@CreateUser", cashBackEntity.LogMessages.Username);
            cmd.SetParameterValue("@Fax", dbParm.Fax);
            cmd.SetParameterValue("@Per", dbParm.ContactPer);
            cmd.SetParameterValue("@Tel", dbParm.Phone);
            cmd.SetParameterValue("@Email", dbParm.ContactEmail);
            return cmd.ExecuteNonQuery(); ;
        }

        public static int InsertBalanceHistory(CashBackEntity cashBackEntity)
        {
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            string[] RoomList = dbParm.HRoomList.Split(',');

            DateTime InDateFrom = DateTime.Parse(dbParm.InDateFrom);
            DateTime InDateTo = DateTime.Parse(dbParm.InDateTo);
             //dbParm.InDateFrom  dbParm.InDateTo
            while (InDateFrom <= InDateTo)
            {
                foreach (string room in RoomList)
                {
                    if (String.IsNullOrEmpty(room.Trim()))
                    {
                        continue;
                    }

                    DataCommand cmd = DataCommandManager.GetDataCommand("InsertBalanceHistory");
                    cmd.SetParameterValue("@HotelID", dbParm.HotelID);
                    cmd.SetParameterValue("@PriceCode", dbParm.PriceCode);
                    cmd.SetParameterValue("@RoomCode", room);
                    cmd.SetParameterValue("@InDate", InDateFrom.ToShortDateString());
                    cmd.SetParameterValue("@BalType", dbParm.BalType);
                    cmd.SetParameterValue("@BalValue", dbParm.BalValue);
                    cmd.SetParameterValue("@CreateUser", cashBackEntity.LogMessages.Username);
                    cmd.ExecuteNonQuery();
                }

                InDateFrom = InDateFrom.AddDays(1);
            }
            return 1;
        }

        public static int UpdateLMSalesHistory(CashBackEntity cashBackEntity)
        {
            if (cashBackEntity.CashBackDBEntity.Count == 0)
            {
                return 0;
            }

            if (cashBackEntity.LogMessages == null)
            {
                return 0;
            }

            if (CheckHistoryUpdate(cashBackEntity))
            {
                DeleteLMSalesHistory(cashBackEntity);
            }

            InsertLMSalesHistory(cashBackEntity);
            return 1;
        }

        public static int InsertLMSalesHistory(CashBackEntity cashBackEntity)
        {
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            OracleParameter[] lmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("SALESID",OracleType.VarChar)
                                };
            lmParm[0].Value = dbParm.HotelID;

            if (String.IsNullOrEmpty(dbParm.SalesID))
            {
                lmParm[1].Value = DBNull.Value;
            }
            else
            {
                lmParm[1].Value = dbParm.SalesID;
            }

            DbManager.ExecuteSql("CashBack", "t_lm_sales_history_save", lmParm);
            return 1;
        }

        public static int DeleteLMSalesHistory(CashBackEntity cashBackEntity)
        {
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            OracleParameter[] dlmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("SALESID",OracleType.VarChar)
                                };
            dlmParm[0].Value = dbParm.HotelID;

            if (String.IsNullOrEmpty(dbParm.SalesID))
            {
                dlmParm[1].Value = DBNull.Value;
            }
            else
            {
                dlmParm[1].Value = dbParm.SalesID;
            }

            DbManager.ExecuteSql("CashBack", "t_lm_sales_history_delete", dlmParm);
            return 1;
        }

        public static bool CheckUpdate(CashBackEntity cashBackEntity)
        {
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("CheckUpdateSalesID");
            cmd.SetParameterValue("@UserCode", dbParm.SalesID);
            string RoleID = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["SalesRoleID"])) ? "5" : ConfigurationManager.AppSettings["SalesRoleID"].ToString().Trim();
            cmd.SetParameterValue("@RoleID", RoleID);
            DataSet dsResult = cmd.ExecuteDataSet();
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool CheckHistoryUpdate(CashBackEntity cashBackEntity)
        {
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            OracleParameter[] dlmParm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("SALESID",OracleType.VarChar)
                                };
            dlmParm[0].Value = dbParm.HotelID;

            if (String.IsNullOrEmpty(dbParm.SalesID))
            {
                dlmParm[1].Value = DBNull.Value;
            }
            else
            {
                dlmParm[1].Value = dbParm.SalesID;
            }
            DataSet dsResult = DbManager.Query("CashBack", "t_lm_sales_history_select", false, dlmParm);
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }


        public static string GetColsNameByRoomTypeCode(string HotelID, string room_type_code)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("ROOMCD",OracleType.VarChar)
                                };
            parm[0].Value = HotelID;
            parm[1].Value = room_type_code;

            DataSet dsResult = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_balanceroom_roomnm", true, parm);
            if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                return dsResult.Tables[0].Rows[0]["name_zh"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static CashBackEntity ExportBalanceRoomHistory(CashBackEntity cashBackEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("ROOMCD",OracleType.VarChar),
                                    new OracleParameter("STARTDT",OracleType.VarChar),
                                    new OracleParameter("ENDDT",OracleType.VarChar)
                                };
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            parm[0].Value = dbParm.HotelID;

            if (String.IsNullOrEmpty(dbParm.HRoomList))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.HRoomList;
            }

            if (String.IsNullOrEmpty(dbParm.InDateFrom))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.InDateFrom;
            }

            if (String.IsNullOrEmpty(dbParm.InDateTo))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.InDateTo;
            }

            DataSet dsResult = new DataSet();
            DataSet dsRoomList = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_balancerom_roomlist", true, parm);
            DataSet dsDataList = HotelVp.Common.DBUtility.DbManager.Query("CashBack", "t_lm_b_balancerom_select", true, parm);

            System.Collections.Hashtable htRoomNm = new System.Collections.Hashtable();
            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add("EFFECTDT");
            foreach (DataRow drCol in dsRoomList.Tables[0].Rows)
            {
                dsResult.Tables[0].Columns.Add(drCol["rate_code"].ToString().ToUpper() + "-" + drCol["room_type_code"].ToString().ToUpper());

                if (!htRoomNm.ContainsKey(drCol["rate_code"].ToString().ToUpper() + "-" + drCol["room_type_code"].ToString().ToUpper()))
                {
                    htRoomNm.Add(drCol["rate_code"].ToString().ToUpper() + "-" + drCol["room_type_code"].ToString().ToUpper(), GetColsNameByRoomTypeCode(dbParm.HotelID, drCol["room_type_code"].ToString()));
                }
            }

            string strDate = string.Empty;
            foreach (DataRow drVal in dsDataList.Tables[0].Rows)
            {
                if (!strDate.Equals(drVal["EFFECTDATE"].ToString()))
                {
                    strDate = drVal["EFFECTDATE"].ToString();
                    DataRow[] drList = dsDataList.Tables[0].Select("EFFECTDATE='" + strDate + "'");

                    if (drList.Count() == 0)
                    {
                        continue;
                    }

                    DataRow drRow = dsResult.Tables[0].NewRow();
                    drRow["EFFECTDT"] = strDate;
                    string strColNM = string.Empty;
                    foreach (DataRow drTemp in drList)
                    {
                        strColNM = drTemp["rate_code"].ToString().ToUpper() + "-" + drTemp["room_type_code"].ToString().ToUpper();
                        drRow[strColNM] = ("42".Equals(drTemp["commision_mode"].ToString())) ? drTemp["commision"].ToString() + "%" : drTemp["commision"].ToString() + "元";
                    }
                    dsResult.Tables[0].Rows.Add(drRow);
                }
            }

            dsResult.Tables[0].Columns["EFFECTDT"].ColumnName = "日期/房型";

            for (int i = 1; i < dsResult.Tables[0].Columns.Count; i++)
            {
                dsResult.Tables[0].Columns[i].ColumnName = dsResult.Tables[0].Columns[i].ColumnName.Substring(0, dsResult.Tables[0].Columns[i].ColumnName.IndexOf('-') + 1) + htRoomNm[dsResult.Tables[0].Columns[i].ColumnName].ToString();
            }

            cashBackEntity.QueryResult = dsResult;
            return cashBackEntity;
        }

        public static CashBackEntity EditAlipayName(CashBackEntity cashBackEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ALIPAYACCOUNTNAME",OracleType.VarChar),
                                    new OracleParameter("SN",OracleType.VarChar)
                                };
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();

            if (String.IsNullOrEmpty(dbParm.AlipayName))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.AlipayName;
            }
            if (String.IsNullOrEmpty(dbParm.Sn))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.Sn;
            }

            cashBackEntity.Result= HotelVp.Common.DBUtility.DbManager.ExecuteSql("CashBack", "t_lm_cash_edit_alipayname", parm);
            return cashBackEntity;
        }

        public static CashBackEntity GetCashBackHistoryByEventHistory(CashBackEntity cashBackEntity)
        {
            CashBackDBEntity dbParm = (cashBackEntity.CashBackDBEntity.Count > 0) ? cashBackEntity.CashBackDBEntity[0] : new CashBackDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetCashBackHistoryByEventHistory");
            cmd.SetParameterValue("@EVENTID", dbParm.Sn);
            cmd.SetParameterValue("@EVENTTYPE", dbParm.Type);
            cashBackEntity.QueryResult =  cmd.ExecuteDataSet();
            return cashBackEntity;
        }

        //通过sequence查询得到下一个ID值,数据库为Oracle
        public static int getMaxIDfromSeq(string sequencename)
        {
            int seqID = 1;
            string sql = "select " + sequencename + ".nextval from dual";
            object obj = DbHelperOra.GetSingle(sql, false);
            if (obj != null)
            {
                seqID = Convert.ToInt32(obj);
            }
            return seqID;
        }
    }
}