using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelVp.CMS.Domain.Entity.eBooking;
using HotelVp.CMS.Domain.ServiceAdapter.eBooking;
using System.Data;
using System.Web.Services;
using System.Text;

public partial class WebUI_eBooking_ReviewUserManagement : System.Web.UI.Page
{
    eBookingUserEntity _eBookingUserEntity = new eBookingUserEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        string a = "";
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        ViewState["loginName"] = txtUserName.Value;
        ViewState["hotelID"] = this.HidSelectHotelID.Value;
        AspNetPager1.CurrentPageIndex = 1;

        LoadData();
    }

    private void LoadData()
    {
        _eBookingUserEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _eBookingUserEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _eBookingUserEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _eBookingUserEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _eBookingUserEntity.eBookingUserDBEntity = new List<eBookingUserDBEntity>();
        eBookingUserDBEntity eBookingUserDBEntity = new eBookingUserDBEntity();

        eBookingUserDBEntity.LoginName = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["loginName"].ToString())) ? null : ViewState["loginName"].ToString();
        eBookingUserDBEntity.HotelId = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["hotelID"].ToString())) ? null : ViewState["hotelID"].ToString();

        eBookingUserDBEntity.PageSize = gridViewCSList.PageSize;
        eBookingUserDBEntity.PageNum = AspNetPager1.CurrentPageIndex;

        _eBookingUserEntity.eBookingUserDBEntity.Add(eBookingUserDBEntity);
        DataSet dsResult = eBookingUserSA.eBookingUserQuery(_eBookingUserEntity).QueryResult;
        gridViewCSList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSList.DataKeyNames = new string[] { "USERNAME" };//主键
        gridViewCSList.DataBind();

        AspNetPager1.PageSize = gridViewCSList.PageSize;
        AspNetPager1.RecordCount = int.Parse(dsResult.Tables[0].Rows[0]["RECORDCOUNT"].ToString());
    }


    protected void gridViewCSList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //this.gridViewRegion.PageIndex = e.NewPageIndex;
        //BindGridView();

        //执行循环，保证每条数据都可以更新
        for (int i = 0; i <= gridViewCSList.Rows.Count; i++)
        {
            //首先判断是否是数据行 
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'");
                //当鼠标移开时还原背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            }
        }
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        LoadData();
    }

    [WebMethod]
    public static string GetUserHotel(string userName)
    {
        DataTable dtResult = eBookingUserSA.eBookingUserQuery(userName).Tables[0];
        return ToJson(dtResult);
    }

    [WebMethod]
    public static string UpdateOrSave(string type, string userName, string userPwd, string userTel, string hotelIds, string remark, string userID)
    {
        string operatorId = UserSession.Current.UserAccount;
        if (type == "1")
        {
            //修改  
            return eBookingUserSA.eBookingUserUpdate(userID, userName, userPwd, hotelIds, remark, userTel, operatorId);
        }
        else
        {
            //增加
            return eBookingUserSA.eBookingUserSave(userName, userPwd, hotelIds, remark, userTel, operatorId);
        }
    }

    #region DataTable 转换 JSON
    public static string ToJson(DataTable dt)
    {
        StringBuilder jsonString = new StringBuilder();
        jsonString.Append("[");
        if (dt != null && dt.Rows.Count > 0)
        {
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
        }
        jsonString.Append("]");
        return jsonString.ToString();
    }

    private static string StringFormat(string str, Type type)
    {
        if (type == typeof(string))
        {
            str = String2Json(str);
            str = "\"" + str + "\"";
        }
        else if (type == typeof(DateTime))
        {
            str = "\"" + str + "\"";
        }
        else if (type == typeof(bool))
        {
            str = str.ToLower();
        }
        else if (type != typeof(string) && string.IsNullOrEmpty(str))
        {
            str = "\"" + str + "\"";
        }
        return str;
    }

    private static string String2Json(String s)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            char c = s.ToCharArray()[i];
            switch (c)
            {
                case '\"':
                    sb.Append("\\\""); break;
                case '\\':
                    sb.Append("\\\\"); break;
                case '/':
                    sb.Append("\\/"); break;
                case '\b':
                    sb.Append("\\b"); break;
                case '\f':
                    sb.Append("\\f"); break;
                case '\n':
                    sb.Append("\\n"); break;
                case '\r':
                    sb.Append("\\r"); break;
                case '\t':
                    sb.Append("\\t"); break;
                default:
                    sb.Append(c); break;
            }
        }
        return sb.ToString();
    }

    #endregion
}