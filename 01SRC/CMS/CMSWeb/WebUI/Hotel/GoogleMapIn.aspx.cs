using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GoogleMapIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strlng = "";
            string strlat = "";
            string strsrcadd = "";

            if (!String.IsNullOrEmpty(Request.QueryString["lng"]))
            {
                strlng = Request.QueryString["lng"].ToString();
            }

            if (!String.IsNullOrEmpty(Request.QueryString["lat"]))
            {
                strlat = Request.QueryString["lat"].ToString();
            }

            if (!String.IsNullOrEmpty(Request.QueryString["srcadd"]))
            {
                strsrcadd = HttpUtility.UrlDecode(Request.QueryString["srcadd"].ToString(), Encoding.GetEncoding("GB2312")); ;
            }

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetInitialData('" + strlng + "','" + strlat + "','" + strsrcadd + "')", true);
        }
    }
}