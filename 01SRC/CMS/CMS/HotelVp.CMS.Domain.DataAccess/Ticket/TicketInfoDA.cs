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
using HotelVp.CMS.Domain.Entity;
//using HotelVp.CMS.Domain.Resource;

namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class TicketInfoDA
    {
        //public static TicketInfoEntity CommonHotelGroupSelect(TicketInfoEntity ticketInfoEntity)
        //{
        //    ticketInfoEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("TicketInfo", "t_lm_b_common_hotelgrouplist", false);
        //    return ticketInfoEntity;
        //}


        public static TicketInfoEntity BindTicketInfoList(TicketInfoEntity ticketInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("PACKAGENAME",OracleType.VarChar), 
                                    new OracleParameter("AMOUNTFROM",OracleType.Int32),
                                    new OracleParameter("AMOUNTTO",OracleType.Int32),
                                    new OracleParameter("STARTDATE",OracleType.VarChar),
                                    new OracleParameter("ENDDATE",OracleType.VarChar),
                                    new OracleParameter("PACKAGETYPE",OracleType.VarChar),
                                    new OracleParameter("TICKETDT",OracleType.VarChar)
                                };

            TicketInfoDBEntity dbParm = (ticketInfoEntity.TicketInfoDBEntity.Count > 0) ? ticketInfoEntity.TicketInfoDBEntity[0] : new TicketInfoDBEntity();

            if (String.IsNullOrEmpty(dbParm.PackageName))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = dbParm.PackageName;
            }

            if (String.IsNullOrEmpty(dbParm.AmountFrom))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.AmountFrom;
            }

            if (String.IsNullOrEmpty(dbParm.AmountTo))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.AmountTo;
            }

            if (String.IsNullOrEmpty(dbParm.Pickfromdate))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = dbParm.Pickfromdate;
            }

            if (String.IsNullOrEmpty(dbParm.Picktodate))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = dbParm.Picktodate;
            }

            if (String.IsNullOrEmpty(dbParm.PackageType))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = dbParm.PackageType;
            }

            if (String.IsNullOrEmpty(dbParm.TicketTime))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = dbParm.TicketTime;
            }

            //已过领用期  0  > 领用最大时间

            //已过使用期  1  > 使用最大时间

            //当前可领用  2  领用开始 < 当前时间 < 领用结束

            //当前可使用  3  使用开始 < 当前时间 < 使用结束

           

            DataSet dsResult = new DataSet();

            DataTable dtAllPg = new DataTable();
            dtAllPg = DbManager.Query("Ticket", "t_lm_ticket_all_type", true, parm).Tables[0].Copy();
            dtAllPg.TableName = "ALLPG";

            DataTable dtAllUser = new DataTable();
            dtAllUser = DbManager.Query("Ticket", "t_lm_ticket_all_user", true, parm).Tables[0].Copy();
            dtAllUser.TableName = "ALLUSER";

            DataTable dtOrdUser = new DataTable();
            dtOrdUser = DbManager.Query("Ticket", "t_lm_ticket_ord_user", true, parm).Tables[0].Copy();
            dtOrdUser.TableName = "ORDUSER";

            DataTable dtAllOrd = new DataTable();
            dtAllOrd = DbManager.Query("Ticket", "t_lm_ticket_all_ord", true, parm).Tables[0].Copy();
            dtAllOrd.TableName = "ALLORD";

            DataTable dtSucOrd = new DataTable();
            dtSucOrd = DbManager.Query("Ticket", "t_lm_ticket_suc_ord", true, parm).Tables[0].Copy();
            dtSucOrd.TableName = "SUCORD";

            dsResult.Tables.Add(dtAllPg);
            dsResult.Tables.Add(dtAllUser);
            dsResult.Tables.Add(dtOrdUser);
            dsResult.Tables.Add(dtAllOrd);
            dsResult.Tables.Add(dtSucOrd);

            ticketInfoEntity.QueryResult = dsResult;
            return ticketInfoEntity;
        }
    }
}