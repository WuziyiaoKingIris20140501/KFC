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

namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class WebAutoCompleteDA
    {
        public static DataSet GetWebCompleteList(string TypeString, string ValueString, string cityName)
        {
            switch (TypeString.ToLower())
            {
                case "sales":
                    return GetSalesManagerList(ValueString);
                case "hvpinventorycontrol":
                    return GetInventoryControlManagerList(ValueString);
                case "approveduser"://订单审核人员
                    return GetApprovedUserList(ValueString);
                case "user":
                    return GetUserManagerList(ValueString);
                case "orderapro":
                    return GetOrderAproManagerList(ValueString);
                case "hotelgroup":
                    return GetHotelGroupList(ValueString);

                case "cruise":
                    return GetCruiseList(ValueString);


                default:
                    return GetCommonCompleteList(TypeString, ValueString, cityName);
            }
            //string strSQL = "t_b_auto_" + TypeString.ToString().ToLower();

            //OracleParameter[] parm ={
            //                        new OracleParameter("PARAMETERS",OracleType.VarChar)
            //                    };
            //parm[0].Value = ValueString.ToString();

            //return HotelVp.Common.DBUtility.DbManager.Query("WebAutoComplete", strSQL, false, parm);
        }

        public static DataSet GetCommonCompleteList(string TypeString, string ValueString, string cityName)
        {
            string strSQL = "t_b_auto_" + TypeString.ToString().ToLower();

            if (!"hvptaginfo".Equals(TypeString))
            {
                OracleParameter[] parm ={
                                    new OracleParameter("PARAMETERS",OracleType.VarChar)
                                };
                parm[0].Value = ValueString.ToString();
                return HotelVp.Common.DBUtility.DbManager.Query("WebAutoComplete", strSQL, false, parm);
            }
            else
            {
                OracleParameter[] parm ={
                                    new OracleParameter("PARAMETERS",OracleType.VarChar),
                                     new OracleParameter("CITYNAME",OracleType.VarChar)
                                };
                parm[0].Value = ValueString.ToString();
                if (string.IsNullOrEmpty(cityName))
                {
                    parm[1].Value = DBNull.Value;
                }
                else
                {
                    parm[1].Value = cityName.ToString().ToLower();
                }
                return HotelVp.Common.DBUtility.DbManager.Query("WebAutoComplete", strSQL, false, parm);
            }
        }

        public static DataSet GetCruiseList(string ValueString)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("WebAutoCruiseSelect");
            cmd.SetParameterValue("@PARAMETERS", ValueString);
            DataSet dsResult = cmd.ExecuteDataSet();
            return dsResult;
        }

        public static DataSet GetSalesManagerList(string ValueString)
        {
            string RoleID = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["SalesRoleID"])) ? "5" : ConfigurationManager.AppSettings["SalesRoleID"].ToString().Trim();
            DataCommand cmd = DataCommandManager.GetDataCommand("WebAutoSalesMangeListSelect");
            cmd.SetParameterValue("@PARAMETERS", ValueString);
            cmd.SetParameterValue("@RoleID", RoleID);
            DataSet dsResult = cmd.ExecuteDataSet();
            return dsResult;
        }

        //订单审核人员
        public static DataSet GetApprovedUserList(string ValueString)
        {
            string RoleID = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["ApprovedControlID"])) ? "7" : ConfigurationManager.AppSettings["ApprovedControlID"].ToString().Trim();
            DataCommand cmd = DataCommandManager.GetDataCommand("WebAutoSalesMangeListSelect");
            cmd.SetParameterValue("@PARAMETERS", ValueString);
            cmd.SetParameterValue("@RoleID", RoleID);
            DataSet dsResult = cmd.ExecuteDataSet();
            return dsResult;
        }

        public static DataSet GetInventoryControlManagerList(string ValueString)
        {
            string RoleID = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["InventoryControlID"])) ? "7" : ConfigurationManager.AppSettings["InventoryControlID"].ToString().Trim();
            DataCommand cmd = DataCommandManager.GetDataCommand("WebAutoSalesMangeListSelect");
            cmd.SetParameterValue("@PARAMETERS", ValueString);
            cmd.SetParameterValue("@RoleID", RoleID);
            DataSet dsResult = cmd.ExecuteDataSet();
            return dsResult;
        }

        public static DataSet GetUserManagerList(string ValueString)
        {
            DataCommand cmd = DataCommandManager.GetDataCommand("WebAutoUserMangeListSelect");
            cmd.SetParameterValue("@PARAMETERS", ValueString);
            DataSet dsResult = cmd.ExecuteDataSet();
            return dsResult;
        }

        public static DataSet GetOrderAproManagerList(string ValueString)
        {
            string RoleID = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["OrderAproID"])) ? "11" : ConfigurationManager.AppSettings["OrderAproID"].ToString().Trim();
            DataCommand cmd = DataCommandManager.GetDataCommand("WebAutoSalesMangeListSelect");
            cmd.SetParameterValue("@PARAMETERS", ValueString);
            cmd.SetParameterValue("@RoleID", RoleID);
            DataSet dsResult = cmd.ExecuteDataSet();
            return dsResult;
        }

        public static DataSet GetHotelGroupList(string ValueString)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("PARAMETERS",OracleType.VarChar)
                                };
            parm[0].Value = ValueString.ToString();

            return HotelVp.Common.DBUtility.DbManager.Query("WebAutoComplete", "t_lm_b_hotelgroup", false, parm);
        }
    }
}