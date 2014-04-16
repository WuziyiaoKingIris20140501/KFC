using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OracleClient;
using System.IO;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.OleDb;

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

public partial class PushManagerPage : BasePage
{
    PushEntity _pushEntity = new PushEntity();
    CommonEntity _commonEntity = new CommonEntity();
    string _strFilePath = string.Empty;
    string _strPushProtoType = string.Empty;
    int _iAllCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidMsg.Value = GetLocalResourceObject("LoadMsg").ToString(); //"数据加载中，请稍等...";
            hidPushType.Value = "0";
            BindReviewLmListGrid();
            /*king*/
            //BindReviewLmSystemLogListGrid(1, gridViewCSReviewLmSystemLogList.PageSize);

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "ClearClickEvent();", true);
        }
        //messageContent.InnerHtml = "";
    }

    private string GetDateTimeQuery(string param)
    {
        if (String.IsNullOrEmpty(param))
        {
            return "";
        }

        try
        {
            DateTime dtTime = DateTime.Parse(param);
            return dtTime.AddDays(1).ToShortDateString() + " 04:00:00";
        }
        catch
        {
            return "";
        }
    }

    //protected void AspNetPager1_PageChanged(object src, EventArgs e)
    //{
    //    BindReviewLmSystemLogListGrid();
    //}

    //private int CountLmSystemLog()
    //{
    //    _pushEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
    //    _pushEntity.LogMessages.Userid = UserSession.Current.UserAccount;
    //    _pushEntity.LogMessages.Username = UserSession.Current.UserDspName;
    //    _pushEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
    //    _pushEntity.FogOrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();
    //    _pushEntity.StartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["StartDTime"].ToString())) ? null : ViewState["StartDTime"].ToString();
    //    _pushEntity.EndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["EndDTime"].ToString())) ? null : ViewState["EndDTime"].ToString();
    //    _pushEntity.HotelID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelID"].ToString())) ? null : ViewState["HotelID"].ToString();
    //    _pushEntity.CityID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["CityID"].ToString())) ? null : ViewState["CityID"].ToString();
    //    _pushEntity.PayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayCode"].ToString())) ? null : ViewState["PayCode"].ToString();
    //    _pushEntity.BookStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["BookStatus"].ToString())) ? null : ViewState["BookStatus"].ToString();
    //    _pushEntity.PayStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PayStatus"].ToString())) ? null : ViewState["PayStatus"].ToString();
    //    _pushEntity.Aprove = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Aprove"].ToString())) ? null : ViewState["Aprove"].ToString();
    //    _pushEntity.HotelComfirm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["HotelComfirm"].ToString())) ? null : ViewState["HotelComfirm"].ToString();
    //    _pushEntity.Ticket = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Ticket"].ToString())) ? null : ViewState["Ticket"].ToString();
    //    _pushEntity.Mobile = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Mobile"].ToString())) ? null : ViewState["Mobile"].ToString();
    //    _pushEntity.InStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InStart"].ToString())) ? null : ViewState["InStart"].ToString();
    //    _pushEntity.InEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["InEnd"].ToString())) ? null : ViewState["InEnd"].ToString();
    //    _pushEntity.PlatForm = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["PlatForm"].ToString())) ? null : ViewState["PlatForm"].ToString();
    //    _pushEntity.Sales = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["Sales"].ToString())) ? null : ViewState["Sales"].ToString();
    //    _pushEntity.OutTest = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OutTest"].ToString())) ? null : ViewState["OutTest"].ToString(); 

    //    _pushEntity.TicketType = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketType"].ToString())) ? null : ViewState["TicketType"].ToString();
    //    _pushEntity.TicketData = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketData"].ToString())) ? null : ViewState["TicketData"].ToString();
    //    _pushEntity.TicketPayCode = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["TicketPcode"].ToString())) ? null : ViewState["TicketPcode"].ToString();

    //    _pushEntity.DashPopStatus = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashPopStatus"].ToString())) ? null : ViewState["DashPopStatus"].ToString();
    //    _pushEntity.DashInStart = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashInStart"].ToString())) ? null : ViewState["DashInStart"].ToString();
    //    _pushEntity.DashInEnd = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashInEnd"].ToString())) ? null : ViewState["DashInEnd"].ToString();
    //    _pushEntity.DashStartDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashStartDTime"].ToString())) ? null : ViewState["DashStartDTime"].ToString();
    //    _pushEntity.DashEndDTime = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["DashEndDTime"].ToString())) ? null : ViewState["DashEndDTime"].ToString();
    
    //    DataSet dsResult = PushBP.ReviewLmOrderLogSelectCount(_pushEntity).QueryResult;

    //    if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0][0].ToString()))
    //    {
    //        return int.Parse(dsResult.Tables[0].Rows[0][0].ToString());
    //    }

    //    return 0;
    //}

    private void BindReviewLmListGrid()
    {
        //messageContent.InnerHtml = "";

        _pushEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _pushEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _pushEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _pushEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        //_pushEntity.FogOrderID = (ViewState.Count == 0 || String.IsNullOrEmpty(ViewState["OrderID"].ToString())) ? null : ViewState["OrderID"].ToString();

        //_pushEntity.PageCurrent = AspNetPager1.CurrentPageIndex;
        _pushEntity.PageSize = gridViewCSReviewList.PageSize;
        //_pushEntity.SortField = gridViewCSReviewList.Attributes["SortExpression"].ToString();
        //_pushEntity.SortType = gridViewCSReviewList.Attributes["SortDirection"].ToString();
        DataSet dsResult = PushBP.PushHistorySelect(_pushEntity).QueryResult;

        gridViewCSReviewList.DataSource = dsResult.Tables[0].DefaultView;
        gridViewCSReviewList.DataKeyNames = new string[] { "TID" };//主键
        gridViewCSReviewList.DataBind();

        //AspNetPager1.PageSize = gridViewCSReviewList.PageSize;
        //AspNetPager1.RecordCount = CountLmSystemLog();
        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
    }

    protected void gridViewCSReviewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    //    //this.gridViewRegion.PageIndex = e.NewPageIndex;
    //    //BindGridView();

    //    //执行循环，保证每条数据都可以更新
    //    for (int i = 0; i <= gridViewCSChannelList.Rows.Count; i++)
    //    {
    //        //首先判断是否是数据行
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            //当鼠标停留时更改背景色
    //            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'");
    //            //当鼠标移开时还原背景色
    //            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
    //        }
    //    }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        //getFileData();
        //BindReviewLmListGrid();
        //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnLoadStyle();", true);

        //System.Diagnostics.Process process = new System.Diagnostics.Process();
        //try
        //{
        //    process.StartInfo.FileName = @"D:\TFS\HotelVPBackend\CMS\01SRC\JobConsole\HotelVp.JobConsole.DailyOrderSum_Top\HotelVp.JobConsole.DailyOrderSumTop\bin\Debug\HotelVp.JobConsole.DailyOrderSumTop.exe";//文件名必须加后缀。 
        //    process.StartInfo.Arguments = "1";
        //    process.Start();
        //}
        //catch (Exception exU)
        //{
        //    //exU.Message
        //}
        //finally
        //{
        //    if (!process.HasExited)
        //    {
        //        process.Close();
        //        process.Dispose();
        //    }
        //}
        //lblRemainSecond.Text = "5";

        _pushEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _pushEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _pushEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _pushEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _pushEntity.PushDBEntity = new List<PushDBEntity>();
        PushDBEntity pushEntity = new PushDBEntity();
        pushEntity.ID = hidTaskID.Value.Trim();
        pushEntity.Status = "1";
        _pushEntity.PushDBEntity.Add(pushEntity);
        _pushEntity = PushBP.SendPushMsg(_pushEntity);
        int iResult = _pushEntity.Result;

        _commonEntity.LogMessages = _pushEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "发送Push信息-发送";
        commonDBEntity.Event_ID = txtPushTitle.Text.Trim();
        string conTent = GetLocalResourceObject("EventSendMessage").ToString();

        string strParams = string.Empty;
        if ("1".Equals(hidPushType.Value))
        {
            strParams = hidUserGroupList.Value.Trim();
        }
        else if ("2".Equals(hidPushType.Value))
        {
            strParams = _strPushProtoType.Trim();
        }
        conTent = string.Format(conTent, hidTaskID.Value, txtPushTitle.Text.Trim(), txtPushContent.Text.Trim(), hidPushType.Value.Trim(), strParams);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            //hidTaskID.Value = _pushEntity.PushDBEntity[0].ID;
            commonDBEntity.Event_Result = GetLocalResourceObject("SendSuccess").ToString();
            //messageContent.InnerHtml = GetLocalResourceObject("SendSuccess").ToString();
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
            //BindReviewLmListGrid();
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "DvHidChangeEvent();", true);
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error9").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error9").ToString();
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "DvHidChangeEvent();", true);
            return;
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error10").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error10").ToString();
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "DvHidChangeEvent();", true);
            return;
        }

        hidMsg.Value = String.Format(GetLocalResourceObject("PushMsg").ToString(), lbPushAllNum.Text); //"Push发送中...0/100";
        hidRemainSecond.Value = (ConfigurationManager.AppSettings["PushRemainSecond"] != null) ? ConfigurationManager.AppSettings["PushRemainSecond"].ToString() : "20";
        this.Page.RegisterStartupScript("remaintimebtn0", "<script>DvHidChangeEvent();setInterval('RemainTimeBtn()',1000);</script>"); //执行定时执行
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        messageContent.InnerHtml = "";

        if (String.IsNullOrEmpty(txtPushTitle.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error1").ToString(); //"Push任务标题不能为空，请确认！";
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "DvShChangeEvent();", true);
            return;
        }

        if (String.IsNullOrEmpty(txtPushContent.Text.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error2").ToString(); //"Push信息正文不能为空，请确认！";
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "DvShChangeEvent();", true);
            return;
        }

        if (StringUtility.Text_Length(txtPushContent.Text.Trim()) > 450)
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error6").ToString(); //"Push信息正文限制输入150个中文字符，请确认！";
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "DvShChangeEvent();", true);
            return;
        }

        if ("1".Equals(hidPushType.Value.Trim()) && String.IsNullOrEmpty(hidUserGroupList.Value.Trim()))
        {
            messageContent.InnerHtml = GetLocalResourceObject("Error3").ToString(); //"指定用户组不能为空，请确认！";
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "DvShChangeEvent();", true);
            return;
        }
        else if ("2".Equals(hidPushType.Value.Trim()))
        {
            string strResult = getFileData();
            if (!String.IsNullOrEmpty(strResult))
            {
                messageContent.InnerHtml = strResult;
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "DvShChangeEvent();", true);
                return;
            }
        }

        lbPushTitle.Text = txtPushTitle.Text;
        txtRePushContent.Text = txtPushContent.Text;

        _pushEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _pushEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _pushEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _pushEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _pushEntity.PushDBEntity = new List<PushDBEntity>();
        PushDBEntity pushEntity = new PushDBEntity();
        pushEntity.ID = hidTaskID.Value.Trim();
        pushEntity.Title = txtPushTitle.Text.Trim();
        pushEntity.Content = txtPushContent.Text.Trim();
        pushEntity.Type = hidPushType.Value.Trim();
        pushEntity.UserGroupList = hidUserGroupList.Value.Trim();
        _strPushProtoType = ("2".Equals(hidPushType.Value)) ? _strPushProtoType : hidUserGroupList.Value.Trim();
        pushEntity.PushProtoType = _strPushProtoType;
        pushEntity.FilePath = _strFilePath.Trim();
        pushEntity.AllCount = GetPushAllCount();
        lbPushAllNum.Text = pushEntity.AllCount;
        _pushEntity.PushDBEntity.Add(pushEntity);

        if (String.IsNullOrEmpty(hidTaskID.Value.Trim()))
        {
            _pushEntity = PushBP.Insert(_pushEntity);
        }
        else
        {
            _pushEntity = PushBP.Update(_pushEntity);
        }

        int iResult = _pushEntity.Result;

        _commonEntity.LogMessages = _pushEntity.LogMessages;
        _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
        CommonDBEntity commonDBEntity = new CommonDBEntity();

        commonDBEntity.Event_Type = "发送Push信息-保存";
        commonDBEntity.Event_ID = txtPushTitle.Text.Trim();
        string conTent = GetLocalResourceObject("EventInsertMessage").ToString();
        
        string strParams = string.Empty;
        if ("1".Equals(hidPushType.Value))
        {
            strParams = hidUserGroupList.Value.Trim();
        }
        else if ("2".Equals(hidPushType.Value))
        {
            strParams = _strPushProtoType.Trim();
        }
        conTent = string.Format(conTent, txtPushTitle.Text.Trim(), txtPushContent.Text.Trim(), hidPushType.Value.Trim(), strParams);
        commonDBEntity.Event_Content = conTent;

        if (iResult == 1)
        {
            hidTaskID.Value = _pushEntity.PushDBEntity[0].ID;
            commonDBEntity.Event_Result = GetLocalResourceObject("InsertSuccess").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("InsertSuccess").ToString();
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);
            BindReviewLmListGrid();
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "DoSaveEvent();", true);
        }
        else if (iResult == 2)
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error8").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error8").ToString();
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "DoSaveEvent();", true);
        }
        else
        {
            commonDBEntity.Event_Result = GetLocalResourceObject("Error7").ToString();
            messageContent.InnerHtml = GetLocalResourceObject("Error7").ToString();
            _commonEntity.CommonDBEntity.Add(commonDBEntity);
            CommonBP.InsertEventHistory(_commonEntity);

            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "DvShChangeEvent();", true);
        }
       
        //UpdatePanel1.Update();
        //UpdatePanel4.Update();
    }

    private string GetPushAllCount()
    {
        PushEntity pushEntityMaster = new PushEntity();
        pushEntityMaster.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        pushEntityMaster.LogMessages.Userid = UserSession.Current.UserAccount;
        pushEntityMaster.LogMessages.Username = UserSession.Current.UserDspName;
        pushEntityMaster.LogMessages.IpAddress = UserSession.Current.UserIP;

        pushEntityMaster.PushDBEntity = new List<PushDBEntity>();
        PushDBEntity pushEntity = new PushDBEntity();
        pushEntity.Type = hidPushType.Value.Trim();
        pushEntity.UserGroupList = hidUserGroupList.Value.Trim();
        pushEntityMaster.PushDBEntity.Add(pushEntity);
        
        if ("0".Equals(hidPushType.Value))
        {
            pushEntityMaster = PushBP.SelectPushAllUsersCount(pushEntityMaster);
            if (pushEntityMaster.QueryResult.Tables.Count > 0 && pushEntityMaster.QueryResult.Tables[0].Rows.Count > 0)
            {
                return pushEntityMaster.QueryResult.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "0";
            }
        }
        else if ("1".Equals(hidPushType.Value))
        {
            pushEntityMaster = PushBP.SelectPushUserGroupCount(pushEntityMaster);
            if (pushEntityMaster.QueryResult.Tables.Count > 0 && pushEntityMaster.QueryResult.Tables[0].Rows.Count > 0)
            {
                return pushEntityMaster.QueryResult.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "0";
            }
        }
        else
        {
            return _iAllCount.ToString();
        }
    }

    protected void btnRefush_Click(object sender, EventArgs e)
    {
        _pushEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _pushEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _pushEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _pushEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _pushEntity.PushDBEntity = new List<PushDBEntity>();
        PushDBEntity pushEntity = new PushDBEntity();
        pushEntity.ID = hidTaskID.Value.Trim();
        _pushEntity.PushDBEntity.Add(pushEntity);
        DataSet dsResult = PushBP.SelectPushSuccCount(_pushEntity).QueryResult;

        decimal decPushSucNum = 0;
        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsResult.Tables[0].Rows[0]["Suc_Count"].ToString()))
        {
            decPushSucNum = decimal.Parse(dsResult.Tables[0].Rows[0]["Suc_Count"].ToString().Trim());
        }
        decimal decPushAllNum = String.IsNullOrEmpty(lbPushAllNum.Text) ? 0 : decimal.Parse(lbPushAllNum.Text);

        string strStatus = dsResult.Tables[0].Rows[0]["Status"].ToString();
        //if (decPushSucNum < decPushAllNum)
        if (!"3".Equals(strStatus))
        {
            hidMsg.Value = "Push发送中..." + decPushSucNum.ToString() + "/" + lbPushAllNum.Text;
            //lblRemainSecond.Text = "5";
            hidRemainSecond.Value = (ConfigurationManager.AppSettings["PushRemainSecond"] != null) ? ConfigurationManager.AppSettings["PushRemainSecond"].ToString() : "20";
            //this.Page.RegisterStartupScript("remaintimebtn0", "<script>setInterval('RemainTimeBtn()',1000);</script>"); //执行定时执行
        }
        else
        {
            //hidMsg.Value = "数据加载中，请稍等...";
            //hidRemainSecond.Value = "0";
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "BtnCompleteStyle();", true);
            //UpdatePanel2.Update();
            //BindReviewLmListGrid();
            Response.Redirect(Request.Url.ToString());
        }
    }

    private string getFileData()
    {
        if (String.IsNullOrEmpty(TokenFlUpload.FileName))
        {
            return GetLocalResourceObject("Error4").ToString(); //"选择文件类型不能为空，请确认！"; ;
        }

        string fileExt = System.IO.Path.GetExtension(TokenFlUpload.FileName);
        if (!String.IsNullOrEmpty(fileExt))
        {
            string allowexts = (ConfigurationManager.AppSettings["AllowUploadFileType"] != null) ? ConfigurationManager.AppSettings["AllowUploadFileType"].ToString() : "";    //定义允许上传文件类型
            if (allowexts == "") { allowexts = ".*xls|.*xlsx"; }

            Regex allowext = new Regex(allowexts);
            if (!allowext.IsMatch(fileExt)) //检查文件大小及扩展名
            {
                return GetLocalResourceObject("Error4").ToString(); //"选择文件类型不正确，请确认！";
            }
        }
        else
        {
            return GetLocalResourceObject("Error4").ToString(); //"选择文件类型不正确，请确认！";
        }

        _strPushProtoType = TokenFlUpload.FileName;
        //StringBuilder sbPhonelist = new StringBuilder();
        string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(TokenFlUpload.FileName);// OrderFlUpload.FileName.Substring(OrderFlUpload.FileName.IndexOf('.'));          //获取文件名
        string folder = Server.MapPath("../PushExcel");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        TokenFlUpload.SaveAs(Server.MapPath("../PushExcel" + "\\" + fileName));      //上传文件到Excel文件夹下
        _strFilePath = fileName;
        DataTable dtTemp = LoadExcelToDataTable(Server.MapPath("../PushExcel" + "\\" + fileName));  //读取excel内容，转成DataTable
        _iAllCount = dtTemp.Rows.Count;

        //File.Delete(Server.MapPath("../Excel" + "\\" + fileName));                                            //最后删除文件

        //foreach (DataRow drTemp in dtTemp.Rows)
        //{
        //    sbPhonelist.Append(drTemp[0].ToString() + ",");
        //}

        ///return (sbPhonelist.Length > 0) ? sbPhonelist.ToString().Substring(0, sbPhonelist.ToString().Length - 1) : sbPhonelist.ToString();
        return "";
    }

    public static DataTable LoadExcelToDataTable(string filename)
    {
        DataTable dtResult = new DataTable();
        //连接字符串  
        string sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties=Excel 12.0;";

        //string sConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + @";Extended Properties=""Excel 12.0;HDR=YES;""";

        //String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filename + ";" + "Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
        OleDbConnection myConn = new OleDbConnection(sConnectionString);
        myConn.Open();
        DataTable sheetNames = myConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
        string sheetName = (sheetNames.Rows.Count > 0) ? sheetNames.Rows[0][2].ToString() : "";

        if (String.IsNullOrEmpty(sheetName))
        {
            return dtResult;
        }
        string strCom = " SELECT * FROM [" + sheetName + "]";

        OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn);
        myCommand.Fill(dtResult);
        myConn.Close();
        return dtResult;
    } 

    protected void gridViewCSReviewList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gridViewCSReviewList.PageIndex = e.NewPageIndex;
        BindReviewLmListGrid();
    } 

     //<summary>
     //GridView排序事件
     //</summary>
    //protected void gridViewCSReviewLmSystemLogList_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    //// 从事件参数获取排序数据列
    //    //string sortExpression = e.SortExpression.ToString();

    //    //// 假定为排序方向为“顺序”
    //    //string sortDirection = "ASC";

    //    //// “ASC”与事件参数获取到的排序方向进行比较，进行GridView排序方向参数的修改
    //    //if (sortExpression == gridViewCSReviewList.Attributes["SortExpression"])
    //    //{
    //    //    //获得下一次的排序状态
    //    //    sortDirection = (gridViewCSReviewList.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
    //    //}

    //    //// 重新设定GridView排序数据列及排序方向
    //    //gridViewCSReviewList.Attributes["SortExpression"] = sortExpression;
    //    //gridViewCSReviewList.Attributes["SortDirection"] = sortDirection;

    //    BindReviewLmListGrid();
    //    //BindReviewLmSystemLogListGrid(AspNetPager1.CurrentPageIndex , gridViewCSReviewLmSystemLogList.PageSize);
    //}
}