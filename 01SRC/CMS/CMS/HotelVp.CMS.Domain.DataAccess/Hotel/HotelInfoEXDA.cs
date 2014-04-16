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
using System.Collections;

//using HotelVp.CMS.Domain.Resource;

namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class HotelInfoEXDA
    {
        public static HotelInfoEXEntity SelectHotelInfoEX(HotelInfoEXEntity hotelinfoexEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            HotelInfoEXDBEntity dbParm = (hotelinfoexEntity.HotelInfoEXDBEntity.Count > 0) ? hotelinfoexEntity.HotelInfoEXDBEntity[0] : new HotelInfoEXDBEntity();
            parm[0].Value = dbParm.HotelID;

            hotelinfoexEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("HotelInfoEX", "t_lm_hotel_ex_select_byhotelid", true, parm);
            return hotelinfoexEntity;
        }

        public static int InsertHotelInfoEX(HotelInfoEXEntity hotelinfoexEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("TYPE",OracleType.VarChar),
                                    new OracleParameter("LINKMAN",OracleType.VarChar),
                                    new OracleParameter("LINKTEL",OracleType.VarChar),
                                    new OracleParameter("LINKMAIL",OracleType.VarChar),
                                    new OracleParameter("LINKFAX",OracleType.VarChar),
                                    new OracleParameter("REMARK",OracleType.VarChar),
                                    new OracleParameter("EXTIME",OracleType.VarChar),
                                    new OracleParameter("EXMODE",OracleType.VarChar),
                                    new OracleParameter("CREATEUSER",OracleType.VarChar),
                                    new OracleParameter("STATUS",OracleType.VarChar),
                                };
            HotelInfoEXDBEntity dbParm = (hotelinfoexEntity.HotelInfoEXDBEntity.Count > 0) ? hotelinfoexEntity.HotelInfoEXDBEntity[0] : new HotelInfoEXDBEntity();
            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HotelID;
            }
            if (String.IsNullOrEmpty(dbParm.Type))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.Type;
            }
            if (String.IsNullOrEmpty(dbParm.LinkMan))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.LinkMan;
            }
            if (String.IsNullOrEmpty(dbParm.LinkTel))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.LinkTel;
            }
            if (String.IsNullOrEmpty(dbParm.LinkMail))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.LinkMail;
            }
            if (String.IsNullOrEmpty(dbParm.LinkFax))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = dbParm.LinkFax;
            }
            if (String.IsNullOrEmpty(dbParm.Remark))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = dbParm.Remark;
            }
            if (String.IsNullOrEmpty(dbParm.ExTime))
            {
                parm[7].Value = DBNull.Value;
            }
            else
            {
                parm[7].Value = dbParm.ExTime;
            }
            if (String.IsNullOrEmpty(dbParm.ExMode))
            {
                parm[8].Value = DBNull.Value;
            }
            else
            {
                parm[8].Value = dbParm.ExMode;
            }
            if (String.IsNullOrEmpty(dbParm.CreateUser))
            {
                parm[9].Value = DBNull.Value;
            }
            else
            {
                parm[9].Value = dbParm.CreateUser;
            }
            if (String.IsNullOrEmpty(dbParm.Status))
            {
                parm[10].Value = DBNull.Value;
            }
            else
            {
                parm[10].Value = dbParm.Status;
            }

           int i = DbManager.ExecuteSql("HotelInfoEX", "t_lm_hotel_ex_insert", parm);
           return i;
        }

        public static int UpdateHotelInfoEX(HotelInfoEXEntity hotelinfoexEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("LINKMAN",OracleType.VarChar),
                                    new OracleParameter("LINKTEL",OracleType.VarChar),
                                    new OracleParameter("LINKMAIL",OracleType.VarChar),
                                    new OracleParameter("LINKFAX",OracleType.VarChar),
                                    new OracleParameter("REMARK",OracleType.VarChar),
                                    new OracleParameter("EXTIME",OracleType.VarChar),
                                    new OracleParameter("EXMODE",OracleType.VarChar),
                                    new OracleParameter("UPDATEUSER",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("TYPE",OracleType.VarChar),
                                    new OracleParameter("STATUS",OracleType.VarChar)
                                };
            HotelInfoEXDBEntity dbParm = (hotelinfoexEntity.HotelInfoEXDBEntity.Count > 0) ? hotelinfoexEntity.HotelInfoEXDBEntity[0] : new HotelInfoEXDBEntity();
            if (String.IsNullOrEmpty(dbParm.LinkMan))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.LinkMan;
            }
            if (String.IsNullOrEmpty(dbParm.LinkTel))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.LinkTel;
            }
            if (String.IsNullOrEmpty(dbParm.LinkMail))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.LinkMail;
            }
            if (String.IsNullOrEmpty(dbParm.LinkFax))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.LinkFax;
            }
            if (String.IsNullOrEmpty(dbParm.Remark))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.Remark;
            }
            if (String.IsNullOrEmpty(dbParm.ExTime))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = dbParm.ExTime;
            }
            if (String.IsNullOrEmpty(dbParm.ExMode))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = dbParm.ExMode;
            }
            if (String.IsNullOrEmpty(dbParm.UpdateUser))
            {
                parm[7].Value = DBNull.Value;
            }
            else
            {
                parm[7].Value = dbParm.UpdateUser;
            }
            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[8].Value = DBNull.Value;
            }
            else
            {
                parm[8].Value = dbParm.HotelID;
            }
            if (String.IsNullOrEmpty(dbParm.Type))
            {
                parm[9].Value = DBNull.Value;
            }
            else
            {
                parm[9].Value = dbParm.Type;
            }
            if (String.IsNullOrEmpty(dbParm.Status))
            {
                parm[10].Value = DBNull.Value;
            }
            else
            {
                parm[10].Value = dbParm.Status;
            }

            int i = DbManager.ExecuteSql("HotelInfoEX", "t_lm_hotel_ex_update", parm);
            return i;
        }

        public static int UpdateHotelInfoEXByConsultRoom(HotelInfoEXEntity hotelinfoexEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("REMARK",OracleType.VarChar),
                                    new OracleParameter("UPDATEUSER",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("TYPE",OracleType.VarChar),
                                    new OracleParameter("LINKMAN",OracleType.VarChar),
                                    new OracleParameter("LINKTEL",OracleType.VarChar)
                                };
            HotelInfoEXDBEntity dbParm = (hotelinfoexEntity.HotelInfoEXDBEntity.Count > 0) ? hotelinfoexEntity.HotelInfoEXDBEntity[0] : new HotelInfoEXDBEntity();
            if (String.IsNullOrEmpty(dbParm.Remark))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.Remark;
            }
            if (String.IsNullOrEmpty(dbParm.UpdateUser))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.UpdateUser;
            }
            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.HotelID;
            }
            if (String.IsNullOrEmpty(dbParm.Type))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.Type;
            }
            if (String.IsNullOrEmpty(dbParm.LinkMan))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.LinkMan;
            }
            if (String.IsNullOrEmpty(dbParm.LinkTel))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = dbParm.LinkTel;
            }

            int i = DbManager.ExecuteSql("HotelInfoEX", "t_lm_hotel_ex_update_by_consultroom", parm);
            return i;
        }

        public static void InsertHotelEXHistory(HotelInfoEXEntity hotelinfoexEntity)
        {
            HotelInfoEXDBEntity dbParm = (hotelinfoexEntity.HotelInfoEXDBEntity.Count > 0) ? hotelinfoexEntity.HotelInfoEXDBEntity[0] : new HotelInfoEXDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertHotelEX");
            cmd.SetParameterValue("@HOTELID", dbParm.HotelID);
            cmd.SetParameterValue("@TYPE", dbParm.Type);
            cmd.SetParameterValue("@STATUS", dbParm.Status);
            cmd.SetParameterValue("@LINKMAN", dbParm.LinkMan);
            cmd.SetParameterValue("@LINKTEL", dbParm.LinkTel);
            cmd.SetParameterValue("@LINKFAX", dbParm.LinkFax);
            cmd.SetParameterValue("@LINKMAIL", dbParm.LinkMail);
            cmd.SetParameterValue("@EXTIME", dbParm.ExTime);
            cmd.SetParameterValue("@EXMODE", dbParm.ExMode);
            cmd.SetParameterValue("@REMARK", dbParm.Remark);
            cmd.SetParameterValue("@OPEUSER", dbParm.CreateUser); 
            cmd.ExecuteNonQuery();
            //int nid = (int)cmd.GetParameterValue("@PlanID");
            //return planID;
        }

        public static int SaveHotelInfoEX(HotelInfoEXEntity hotelinfoexEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("TYPE",OracleType.VarChar),
                                    new OracleParameter("LINKMAN",OracleType.VarChar),
                                    new OracleParameter("LINKTEL",OracleType.VarChar),
                                    new OracleParameter("LINKMAIL",OracleType.VarChar),
                                    new OracleParameter("LINKFAX",OracleType.VarChar),
                                    new OracleParameter("REMARK",OracleType.VarChar),
                                    new OracleParameter("EXTIME",OracleType.VarChar),
                                    new OracleParameter("EXMODE",OracleType.VarChar),
                                    new OracleParameter("CREATEUSER",OracleType.VarChar),
                                    new OracleParameter("STATUS",OracleType.VarChar),
                                };
            HotelInfoEXDBEntity dbParm = (hotelinfoexEntity.HotelInfoEXDBEntity.Count > 0) ? hotelinfoexEntity.HotelInfoEXDBEntity[0] : new HotelInfoEXDBEntity();
            if (String.IsNullOrEmpty(dbParm.HotelID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.HotelID;
            }
            if (String.IsNullOrEmpty(dbParm.Type))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.Type;
            }
            if (String.IsNullOrEmpty(dbParm.LinkMan))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.LinkMan;
            }
            if (String.IsNullOrEmpty(dbParm.LinkTel))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.LinkTel;
            }
            if (String.IsNullOrEmpty(dbParm.LinkMail))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.LinkMail;
            }
            if (String.IsNullOrEmpty(dbParm.LinkFax))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = dbParm.LinkFax;
            }
            if (String.IsNullOrEmpty(dbParm.Remark))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = dbParm.Remark;
            }
            if (String.IsNullOrEmpty(dbParm.ExTime))
            {
                parm[7].Value = DBNull.Value;
            }
            else
            {
                parm[7].Value = dbParm.ExTime;
            }
            if (String.IsNullOrEmpty(dbParm.ExMode))
            {
                parm[8].Value = DBNull.Value;
            }
            else
            {
                parm[8].Value = dbParm.ExMode;
            }
            if (String.IsNullOrEmpty(dbParm.CreateUser))
            {
                parm[9].Value = DBNull.Value;
            }
            else
            {
                parm[9].Value = dbParm.CreateUser;
            }
            if (String.IsNullOrEmpty(dbParm.Status))
            {
                parm[10].Value = DBNull.Value;
            }
            else
            {
                parm[10].Value = dbParm.Status;
            }

           int i = DbManager.ExecuteSql("HotelInfoEX", "t_lm_hotel_ex_save", parm);
           return i;
        }
    }
}
