using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using HotelVp.Common.DBUtility;
/// <summary>
///PlatForm 的摘要说明
/// </summary>
[DataObject(true)]
public class PlatForm
{
	public PlatForm()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    ///  得到所有角色
    /// </summary>
    /// <param name="userName">用户名称</param>
    /// <returns></returns>
    [DataObjectMethod(DataObjectMethodType.Select, true)]
    static public DataSet GetPlatForm()
    {
        string sql = "select PLATFORM_CODE,PLATFORM_NAME from T_LM_B_PLATFORM order by CREATE_TIME desc";
        DataSet ds = DbHelperOra.Query(sql, false);
        return ds;             
    }
}