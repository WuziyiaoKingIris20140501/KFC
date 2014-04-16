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
    public abstract class InvoiceDA
    {
        public static InvoiceEntity GetInvoiceList(InvoiceEntity invoiceEntity)
        {
            InvoiceDBEntity dbParm = (invoiceEntity.InvoiceDBEntity.Count > 0) ? invoiceEntity.InvoiceDBEntity[0] : new InvoiceDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetInvoiceList");

            cmd.SetParameterValue("@UnitName", dbParm.UnitName);
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@HotelGroup", dbParm.HotelGroup);
            cmd.SetParameterValue("@CityID", dbParm.CityID);
            cmd.SetParameterValue("@UnitCharge", dbParm.UnitCharge);
            
            cmd.SetParameterValue("@OrderID", dbParm.OrderID);
            cmd.SetParameterValue("@SlMonth", dbParm.SlMonth);
            cmd.SetParameterValue("@InvoiceStatus", dbParm.InvoiceStatus);
            
            cmd.SetParameterValue("@PageCurrent", invoiceEntity.PageCurrent);
            cmd.SetParameterValue("@PageSize", invoiceEntity.PageSize);
            cmd.SetParameterValue("@SortField", invoiceEntity.SortField);
            cmd.SetParameterValue("@SortType", invoiceEntity.SortType);
            
            invoiceEntity.InvoiceDBEntity[0].InvoiceList = cmd.ExecuteDataSet();
            invoiceEntity.TotalCount = (int)cmd.GetParameterValue("@TotalCount");

            return invoiceEntity;
        }

        public static InvoiceEntity ExportUnOpenInvoiceList(InvoiceEntity invoiceEntity)
        {
            InvoiceDBEntity dbParm = (invoiceEntity.InvoiceDBEntity.Count > 0) ? invoiceEntity.InvoiceDBEntity[0] : new InvoiceDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("ExportUnOpenInvoiceList");

            cmd.SetParameterValue("@UnitName", dbParm.UnitName);
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@HotelGroup", dbParm.HotelGroup);
            cmd.SetParameterValue("@CityID", dbParm.CityID);
            cmd.SetParameterValue("@UnitCharge", dbParm.UnitCharge);

            cmd.SetParameterValue("@OrderID", dbParm.OrderID);
            cmd.SetParameterValue("@SlMonth", dbParm.SlMonth);
            cmd.SetParameterValue("@InvoiceStatus", dbParm.InvoiceStatus);
            
            invoiceEntity.InvoiceDBEntity[0].InvoiceExportList = cmd.ExecuteDataSet();
            return invoiceEntity;
        }

        public static InvoiceEntity ImportOpenInvoiceList(InvoiceEntity invoiceEntity)
        {
            InvoiceDBEntity dbParm = (invoiceEntity.InvoiceDBEntity.Count > 0) ? invoiceEntity.InvoiceDBEntity[0] : new InvoiceDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("ImportOpenInvoiceList");

            //cmd.SetParameterValue("@", dbParm.);
            //cmd.SetParameterValue("@", dbParm.);
            //cmd.SetParameterValue("@", dbParm.);
            //cmd.SetParameterValue("@", dbParm.);
            //cmd.SetParameterValue("@", dbParm.);
            //cmd.SetParameterValue("@", dbParm.);
            //cmd.SetParameterValue("@", dbParm.);
            //cmd.SetParameterValue("@", dbParm.);
            //cmd.SetParameterValue("@", dbParm.);
            //cmd.SetParameterValue("@", dbParm.);

            invoiceEntity.Result = cmd.ExecuteNonQuery();
            return invoiceEntity;
        }

        public static InvoiceEntity GetInvoiceDetail(InvoiceEntity invoiceEntity)
        {
            InvoiceDBEntity dbParm = (invoiceEntity.InvoiceDBEntity.Count > 0) ? invoiceEntity.InvoiceDBEntity[0] : new InvoiceDBEntity();
            if ("0".Equals(dbParm.InvoiceDataType))
            {
                return GetUnitInvoiceDetail(invoiceEntity);
            }
            else
            {
                return GetConfirmInvoiceDetail(invoiceEntity);
            }
           
        }

        public static InvoiceEntity GetUnitInvoiceDetail(InvoiceEntity invoiceEntity)
        {
            //InvoiceDBEntity dbParm = (invoiceEntity.InvoiceDBEntity.Count > 0) ? invoiceEntity.InvoiceDBEntity[0] : new InvoiceDBEntity();
            //OracleParameter[] parm ={
            //                         new OracleParameter("UnitID",OracleType.VarChar)
            //                    };
            //parm[0].Value = dbParm.UnitID;
            //DataSet dsResult = DbManager.Query("SettleInfo", "t_lm_settlement_unit_detail_select", true, parm);
            //invoiceEntity.InvoiceDBEntity[0].InvoiceDetail = dsResult;
            //return invoiceEntity;

            InvoiceDBEntity dbParm = (invoiceEntity.InvoiceDBEntity.Count > 0) ? invoiceEntity.InvoiceDBEntity[0] : new InvoiceDBEntity();
            DataCommand slCmd = DataCommandManager.GetDataCommand("GetSLDetial");
            slCmd.SetParameterValue("@SLID", dbParm.SLID);
            invoiceEntity.InvoiceDBEntity[0].InvoiceDetail = slCmd.ExecuteDataSet();
            return invoiceEntity;
        }

        public static InvoiceEntity GetConfirmInvoiceDetail(InvoiceEntity invoiceEntity)
        {
            InvoiceDBEntity dbParm = (invoiceEntity.InvoiceDBEntity.Count > 0) ? invoiceEntity.InvoiceDBEntity[0] : new InvoiceDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetConfirmInvoiceDetail");
            cmd.SetParameterValue("@SLID", dbParm.SLID);
            cmd.SetParameterValue("@INVOICEID", dbParm.InvoiceID);
            invoiceEntity.InvoiceDBEntity[0].InvoiceDetail = cmd.ExecuteDataSet();
            return invoiceEntity;
        }

        public static InvoiceEntity ConfirmInvoiceDetail(InvoiceEntity invoiceEntity)
        {
            InvoiceDBEntity dbParm = (invoiceEntity.InvoiceDBEntity.Count > 0) ? invoiceEntity.InvoiceDBEntity[0] : new InvoiceDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("ConfirmInvoiceDetail");
            cmd.SetParameterValue("@SLID", dbParm.SLID);
            cmd.SetParameterValue("@INVOICENM", dbParm.InvoiceName);
            cmd.SetParameterValue("@INVOICEPROJECT", dbParm.InvoiceProject);
            cmd.SetParameterValue("@INVOICEAMOUNT", dbParm.InvoiceAmount);
            cmd.SetParameterValue("@ZIPADDRESS", dbParm.ZipAddress);
            cmd.SetParameterValue("@INVOICENUM", dbParm.InvoiceNum);
            cmd.SetParameterValue("@ZIPNUM", dbParm.ZipNum);
            cmd.SetParameterValue("@CREATEUSER", invoiceEntity.LogMessages.Userid);
            invoiceEntity.Result = cmd.ExecuteNonQuery();
            int invoiceID = (int)cmd.GetParameterValue("@InvoiceID");

            DataCommand slCmd = DataCommandManager.GetDataCommand("UpdateSLInvoiceNum");
            slCmd.SetParameterValue("@SLID", dbParm.SLID);
            slCmd.SetParameterValue("@INVOICEID", invoiceID);
            slCmd.SetParameterValue("@INVOICESTATUS", "1");
            slCmd.SetParameterValue("@OPEUSER", invoiceEntity.LogMessages.Userid);
            slCmd.ExecuteNonQuery();

            // 更新清结算表内的发票ID和发票状态
            return invoiceEntity;
        }

        public static InvoiceEntity ReConfirmInvoiceDetail(InvoiceEntity invoiceEntity)
        {
            InvoiceDBEntity dbParm = (invoiceEntity.InvoiceDBEntity.Count > 0) ? invoiceEntity.InvoiceDBEntity[0] : new InvoiceDBEntity();

            DataCommand slCmd = DataCommandManager.GetDataCommand("UpdateSLInvoiceList");
            slCmd.SetParameterValue("@SLID", dbParm.SLID);
            slCmd.SetParameterValue("@OPEUSER", invoiceEntity.LogMessages.Userid);
            slCmd.ExecuteNonQuery();

            DataCommand cmd = DataCommandManager.GetDataCommand("ReConfirmInvoiceDetail");
            cmd.SetParameterValue("@SLID", dbParm.SLID);
            cmd.SetParameterValue("@INVOICENM", dbParm.InvoiceName);
            cmd.SetParameterValue("@INVOICEPROJECT", dbParm.InvoiceProject);
            cmd.SetParameterValue("@INVOICEAMOUNT", dbParm.InvoiceAmount);
            cmd.SetParameterValue("@ZIPADDRESS", dbParm.ZipAddress);
            cmd.SetParameterValue("@INVOICENUM", dbParm.InvoiceNum);
            cmd.SetParameterValue("@ISREOPEN", dbParm.InvoiceNum);
            cmd.SetParameterValue("@REOPENREASON", dbParm.InvoiceNum);
            cmd.SetParameterValue("@OLDINVOICENUM", dbParm.InvoiceNum);
            cmd.SetParameterValue("@ZIPNUM", dbParm.InvoiceNum);
            cmd.SetParameterValue("@ISREZIP", dbParm.InvoiceNum);
            cmd.SetParameterValue("@OLDZIPNUM", dbParm.InvoiceNum);
            cmd.SetParameterValue("@CREATEUSER", invoiceEntity.LogMessages.Userid);

            invoiceEntity.Result = cmd.ExecuteNonQuery();
             int invoiceID = (int)cmd.GetParameterValue("@InvoiceID");

            DataCommand slsCmd = DataCommandManager.GetDataCommand("UpdateSLInvoiceNum");
            slsCmd.SetParameterValue("@SLID", dbParm.SLID);
            slsCmd.SetParameterValue("@INVOICEID", invoiceID);
            slsCmd.SetParameterValue("@INVOICESTATUS", "1");
            slsCmd.SetParameterValue("@OPEUSER", invoiceEntity.LogMessages.Userid);
            slsCmd.ExecuteNonQuery();

            return invoiceEntity;
        }
    }
}
