using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GoogleMapIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["lng"]) && !String.IsNullOrEmpty(Request.QueryString["lat"]))
            {
                string strlng = Request.QueryString["lng"].ToString();
                string strlat = Request.QueryString["lat"].ToString();
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetInitialData('" + strlng + "','" + strlat + "')", true);
            }
        }
    }
}