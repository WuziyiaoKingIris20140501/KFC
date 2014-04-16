using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Collections;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;

using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;
using HotelVp.Common.Utilities;

public partial class OrderConfirmInfoImagePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string imgPre = Request.QueryString["ID"] == null ? "" : Request.QueryString["ID"].ToString();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "SetImagePreview('" + imgPre + "');", true);
        }
    }
}