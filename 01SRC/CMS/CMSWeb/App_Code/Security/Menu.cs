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
///Menu 的摘要说明
/// </summary>

[DataObject(true)]
public class Menu
{
	public Menu()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    ///  得到所有第一级菜单
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    [DataObjectMethod(DataObjectMethodType.Select, true)]
    static public DataTable GetFirstMenu()
    {
        DataTable myTable = MenuBP.getFristMenu().Tables[0];
        DataRow row1 = myTable.NewRow();
        row1["Menu_Name"] = "根节点";
        row1["Menu_ID"] = "0";
        myTable.Rows.InsertAt(row1, 0);
        return myTable;
    }


   

}