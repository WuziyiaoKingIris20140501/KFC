using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HotelVp.Common.Json;
using HotelVp.Common.Json.Linq;
using HotelVp.Common.Json.Serialization;

public partial class WebUI_UserGroup_SearchSign : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strMobile = this.txtMobile.Text;      
        string getResult = getSignByPhoneForCC(strMobile);
        if (getResult == "")
        {
            this.lblSign.Text = "查询失败!";
        }
        else
        {
            this.lblSign.Text = getResult;
        }

    }

    //用于CC根据电话号码，查询生产新的验证码.
    public string getSignByPhoneForCC(string userPhoneNumber)
    {
        bool boolResult = false;
        try
        {
            string url = JsonRequestURLBuilder.QueryUserSign();

            string postDataStr = "{\"LmLoginRQ\":{\"loginMobile\":\"" + userPhoneNumber + "\"}}";
            CallWebPage callWebPage = new CallWebPage();
            string strJson = callWebPage.CallWebByURL(url, postDataStr);


            //解析json数据
            JObject o = JObject.Parse(strJson);
            JObject oLoginMember = (JObject)o.SelectToken("LoginLmRS[0]");

            string strSign = string.Empty;
            if (oLoginMember.SelectToken("result.success") != null)
            {
                boolResult = (bool)oLoginMember.SelectToken("result.success");
                if (boolResult == true)
                {
                    if (oLoginMember.SelectToken("signKey") != null)
                    {
                        strSign = oLoginMember.SelectToken("signKey").ToString().Trim('"');
                    }
                }
                else
                {
                    if (oLoginMember.SelectToken("result.errormsg") != null)
                    {
                        strSign = oLoginMember.SelectToken("result.errormsg").ToString().Trim('"');
                    }
                }
            }
            return strSign;
        }
        catch
        {
            return "";
        }

    } 
}