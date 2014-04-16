using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

using HotelVp.Common.DBUtility;
using HotelVp.Common.DataAccess;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;
/// <summary>
///MenuList 的摘要说明
/// </summary>
public class MenuList
{
	public MenuList()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    //获取
    public static string GetItem(Page page)
    {
        DataTable dt = new DataTable();

        //dt = UserPermission.GetModulesByLevel(ModuleLevel.All);

        if (HttpContext.Current.Application["Administrator"].ToString().Contains(UserSession.Current.UserAccount.ToLower()))
        {
            //dt = UserPermission.GetModulesByLevel(ModuleLevel.All);
            dt = UserPermission.GetModulesByLevelByAll(ModuleLevel.All);
        }
        else
        {
            dt = new UserPermission(UserSession.Current.UserAccount).GetModulesByLevelWithRight(ModuleLevel.All);
        }

        string munestr = "";
        DataRow[] TopMenuRows;
        string strCHorEN = string.Empty;
        TopMenuRows = dt.Select("Menu_Level=1", "Menu_OrderID asc");

        for (int i = 0; i < TopMenuRows.Length; i++)
        {
            DataRow[] SubMenuRows = dt.Select("Parent_MenuId=" + TopMenuRows[i]["Menu_ID"].ToString().Trim(), "Menu_OrderID asc");

            if (munestr == "")
            {
                munestr += "\"" + TopMenuRows[i]["Menu_Name"].ToString().Trim() + "|" + TopMenuRows[i]["Menu_ID"].ToString().Trim() + "\":";
            }
            else
            {
                munestr += ",\"" + TopMenuRows[i]["Menu_Name"].ToString().Trim() + "|" + TopMenuRows[i]["Menu_ID"].ToString().Trim() + "\":";
            }
            if (SubMenuRows.Length <= 0)//if there is no submenu,the top menue needn't to display
            {
                munestr += "\"\"";
                continue;
            }
            for (int j = 0; j < SubMenuRows.Length; j++)
            {
                if (j == 0)
                {
                    munestr += "\"" + SubMenuRows[j]["Menu_Name"].ToString().Trim() + "|" + SubMenuRows[j]["Menu_Url"].ToString().Trim() + "|" + SubMenuRows[j]["Menu_ID"].ToString().Trim();
                }
                else
                {
                    munestr += "," + SubMenuRows[j]["Menu_Name"].ToString().Trim() + "|" + SubMenuRows[j]["Menu_Url"].ToString().Trim() + "|" + SubMenuRows[j]["Menu_ID"].ToString().Trim();

                }
                DataRow[] SecondMenuRows = dt.Select("Parent_MenuId=" + SubMenuRows[j]["Menu_ID"].ToString().Trim(), "Menu_OrderID asc");
                if (SecondMenuRows.Length > 0)
                {
                    for (int k = 0; k < SecondMenuRows.Length; k++)
                    {
                        munestr += "*" + SecondMenuRows[k]["Menu_Name"].ToString().Trim() + "|" + SecondMenuRows[k]["Menu_Url"].ToString().Trim() + "|" + SecondMenuRows[k]["Menu_ID"].ToString().Trim();
                    }
                }

            }
            if (SubMenuRows.Length > 0)//if there no content ,needn't to add the end "
            {
                munestr += "\"";
            }
        }
        return "{" + munestr + "}";//the new memu tree
    }

    public static string GetItemOne(Page page, string strName)
    {
        DataTable dt = new DataTable();
        if (HttpContext.Current.Application["Administrator"].ToString().Contains(UserSession.Current.UserAccount.ToLower()))
        {
            dt = UserPermission.GetModulesByLevel(ModuleLevel.All);
        }
        else
        {
            dt = new UserPermission(UserSession.Current.UserAccount).GetModulesByLevelWithRight(ModuleLevel.All);
        }
        string munestr = "";
        DataRow[] TopMenuRows;


        TopMenuRows = dt.Select(" Menu_Level=1 and Menu_Name = '" + strName + "'", "Menu_OrderID asc");

        for (int i = 0; i < TopMenuRows.Length; i++)
        {
            DataRow[] SubMenuRows = dt.Select("Parent_MenuId=" + TopMenuRows[i]["Menu_ID"].ToString().Trim(), "Menu_OrderID asc");

            if (munestr == "")
            {
                munestr += "\"" + TopMenuRows[i]["Menu_Name"].ToString().Trim() + "|" + TopMenuRows[i]["Menu_ID"].ToString().Trim() + "\":";
            }
            else
            {
                munestr += ",\"" + TopMenuRows[i]["Menu_Name"].ToString().Trim() + "|" + TopMenuRows[i]["Menu_ID"].ToString().Trim() + "\":";
            }
            if (SubMenuRows.Length <= 0)//if there is no submenu,the top menue needn't to display
            {
                munestr += "\"\"";
                continue;
            }
            for (int j = 0; j < SubMenuRows.Length; j++)
            {
                if (j == 0)
                {
                    munestr += "\"" + SubMenuRows[j]["Menu_Name"].ToString().Trim() + "|" + SubMenuRows[j]["Menu_Url"].ToString().Trim() + "|" + SubMenuRows[j]["Menu_ID"].ToString().Trim();
                }
                else
                {
                    munestr += "," + SubMenuRows[j]["Menu_Name"].ToString().Trim() + "|" + SubMenuRows[j]["Menu_Url"].ToString().Trim() + "|" + SubMenuRows[j]["Menu_ID"].ToString().Trim();

                }
                DataRow[] SecondMenuRows = dt.Select("Parent_MenuId=" + SubMenuRows[j]["Menu_ID"].ToString().Trim(), "Menu_OrderID asc");
                if (SecondMenuRows.Length > 0)
                {
                    for (int k = 0; k < SecondMenuRows.Length; k++)
                    {
                        munestr += "*" + SecondMenuRows[k]["Menu_Name"].ToString().Trim() + "|" + SecondMenuRows[k]["Menu_Url"].ToString().Trim() + "|" + SecondMenuRows[k]["Menu_ID"].ToString().Trim();
                    }
                }

            }
            if (SubMenuRows.Length > 0)//if there no content ,needn't to add the end "
            {
                munestr += "\"";
            }
        }
        return "{" + munestr + "}";//the new memu tree
    }

    ///// <summary>
    ///// 获取菜单信息
    ///// </summary>
    ///// <returns></returns>
    //public static DataSet GetAllValidMenuByParentID(string pid)
    //{
    //    if (string.IsNullOrEmpty(pid)) pid = "0";
    //    return Maticsoft.DBUtility.DbHelperSQL.Query(string.Format("select * from CMS_SYS_MENU where Parent_MenuId ={0}", pid));        

    //}
}