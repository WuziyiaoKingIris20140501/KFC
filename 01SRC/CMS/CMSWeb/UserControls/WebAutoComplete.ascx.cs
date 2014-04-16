using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;
using System.Data.OracleClient;

using HotelVp.CMS.Domain.Process;

public partial class WebAutoComplete : System.Web.UI.UserControl
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string valueString = CheckName(Request["q"]);

        string name = _cityName;

        _autoType = String.IsNullOrEmpty(Request["type"]) ? _autoType : Request["type"].ToString();

        if (!String.IsNullOrEmpty(valueString))
        {
            Response.Clear();
            Response.Charset = "utf-8";
            Response.Buffer = true;
            this.EnableViewState = false;
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "text/plain";
            Response.Write(GetLikeUserName(valueString.Trim()));
            Response.Flush();
            //Response.Close();
            Response.End();
        }
    }

    private string GetLikeUserName(string valueString)
    {
        if (String.IsNullOrEmpty(valueString) || String.IsNullOrEmpty(_autoType.Trim()))
        {
            return "";
        }

        StringBuilder sb = new StringBuilder();

        WebAutoCompleteBP webBP = new WebAutoCompleteBP();
        //if (Session["cityName"] != null)
        //{
        //    CityName = Session["cityName"].ToString();
        //}
        DataSet dsResult = webBP.GetWebCompleteList(_autoType, valueString, _cityName);

        if (dsResult.Tables[0].Rows.Count == 0)
        {
            return "";
        }

        foreach (DataRow dr in dsResult.Tables[0].Rows)
        {
            sb.Append(dr["REVALUE_ALL"].ToString()).Append("\n");
        }
        return sb.ToString();
    }

    private string CheckName(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return string.Empty;
        }
        return str;
    }

    private string _autoType;
    public string AutoType
    {
        get
        {
            return _autoType;
        }
        set
        {
            _autoType = value;
        }
    }

    private string _autoParent;
    public string AutoParent
    {
        get
        {
            return _autoParent;
        }
        set
        {
            _autoParent = value;
        }
    }

    string _autoResult;
    public string AutoResult
    {
        get
        {
            return hiddenValue.Value;//return _autoResult;
        }
        set
        {
            hiddenValue.Value = value;//_autoResult = value;
        }
    }

    public string CTLID
    {
        get
        {
            return hiddenCtlID.Value;//return _autoResult;
        }
        set
        {
            hiddenCtlID.Value = value;//_autoResult = value;
        }
    }

    //private string _autoCtlDisplay = "";
    public string CTLDISPLAY
    {
        get
        {
            return HiddenStyle.Value;//return _autoResult;
        }
        set
        {
            HiddenStyle.Value = value;//_autoResult = value;
        }
    }

    string _cityName;
    public string CityName
    {
        get { return _cityName; }
        set { _cityName = value; }
    }
    //public string CityName
    //{
    //    get
    //    {
    //        return HiddenCityName.Value;//return _autoResult;
    //    }
    //    set
    //    {
    //        HiddenCityName.Value = value;//_autoResult = value;
    //    }
    //}
}