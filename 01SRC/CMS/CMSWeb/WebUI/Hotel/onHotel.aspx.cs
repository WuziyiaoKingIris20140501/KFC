using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OracleClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Hotel_onHotel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }   

    /// <summary>
    /// 设置上线酒店
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string getHotelList = this.txtHotelID.Text;
       
        if (String.IsNullOrEmpty(getHotelList))
        {
            return;
        }

         List<string> sqlList = new List<string>();
         string[] strLIst = getHotelList.Split(',');
         string sql = "update T_LM_B_PROP T SET T.ONLINE_STATUS='1' where t.prop =";
         string strSQL = "";
        foreach (string par in strLIst)
        {
            if (!String.IsNullOrEmpty(par.Trim()))
            {
                strSQL = sql + par.Trim().Replace("'", "").Replace("(", "").Replace(")", "");
                sqlList.Add(strSQL);
                strSQL = "";
            }
        }
        try
        {
            Maticsoft.DBUtility.DbHelperOra.ExecuteSqlTran(sqlList);
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert( '上线成功！' );", true);
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert( '上线失败！' );", true);
        }
    }
}