using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data;
using System.Text;
using HotelVp.Common.Utilities;
using HotelVp.Common.DBUtility;
using System.Collections;


public partial class HotelInfoManagerMap : BasePage
{
    public string hotelname = string.Empty;
    public string latitude = string.Empty;
    public string longitude = string.Empty;
    public string arrData = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string latitudetemp = string.Empty;
            string longitudetemp = string.Empty;
            string hotelname = string.Empty;
            if (!String.IsNullOrEmpty(Request.QueryString["latitude"]))
            {
                latitudetemp = Request.QueryString["latitude"].ToString().Trim();
            }

            if (!String.IsNullOrEmpty(Request.QueryString["longitude"]))
            {
                longitudetemp = Request.QueryString["longitude"].ToString().Trim();
            }

            if (!String.IsNullOrEmpty(Request.QueryString["hotelname"]))
            {
                hotelname = HttpUtility.UrlDecode(Request.QueryString["hotelname"], Encoding.GetEncoding("GB2312"));
            }
            arrData = getLatLngList(latitudetemp, longitudetemp, hotelname);
        }
    }

    private string getLatLngList(string latitudetemp, string longitudetemp, string hotelname)
    {
        string temp = string.Empty;
        temp = "['" + hotelname + "'," + latitudetemp + "," + longitudetemp + "," + 0 + "]";
        return "[" + temp + "]";; 
    }   
}