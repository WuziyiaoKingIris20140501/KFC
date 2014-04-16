using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.OracleClient;

using System.Data.OleDb;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Text.RegularExpressions;

using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;
using HotelVp.CMS.Domain.DataAccess;
using HotelVp.Common.DBUtility;

public partial class WebUI_Ticket_GrantTicketByPhone : BasePage

{
    public static string selectLabel = string.Empty;
    public static string PromptTicketCodeNoEmpty = string.Empty;
    public static string  PromptPhoneNoInput= string.Empty; 
    public static string PromptPackageHaveFinish = string.Empty;
    public static string PromptLeaveCount = string.Empty;
    public static string PromptActualCreate = string.Empty;
    public static string Count = string.Empty;
    public static string PromptTicketNotUse = string.Empty;
    public static string PromptTicketCountMustOne = string.Empty;
    public static string PromptPhoneFormatError = string.Empty;
    public static string PromptHaveSamePhone = string.Empty;
    public static string PromptCodeHaveFinish = string.Empty;
    public static string PromptTicketSuccess = string.Empty;
    public static string PromptTicketFaild = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        selectLabel = Resources.MyGlobal.SelectText;
        PromptTicketCodeNoEmpty = GetLocalResourceObject("PromptTicketCodeNoEmpty").ToString();
        PromptPhoneNoInput = GetLocalResourceObject("PromptPhoneNoInput").ToString();

        PromptPackageHaveFinish = GetLocalResourceObject("PromptPackageHaveFinish").ToString();
        PromptLeaveCount = GetLocalResourceObject("PromptLeaveCount").ToString();
        PromptActualCreate = GetLocalResourceObject("PromptActualCreate").ToString();
        Count = GetLocalResourceObject("Count").ToString();
        PromptTicketNotUse = GetLocalResourceObject("PromptTicketNotUse").ToString();
        PromptTicketCountMustOne = GetLocalResourceObject("PromptTicketCountMustOne").ToString();
        PromptPhoneFormatError = GetLocalResourceObject("PromptPhoneFormatError").ToString();
        PromptHaveSamePhone = GetLocalResourceObject("PromptHaveSamePhone").ToString();
        PromptCodeHaveFinish = GetLocalResourceObject("PromptCodeHaveFinish").ToString();
        PromptTicketSuccess = GetLocalResourceObject("PromptTicketSuccess").ToString();
        PromptTicketFaild = GetLocalResourceObject("PromptTicketFaild").ToString();

       
        if (!IsPostBack)
        {
            //为了在前段使用，判断输入的是否超过长度。
            this.txtPhoneNumber.Attributes.Add("onblur", "checkPhoneInputLength();");
            //绑定列表
            BindGridView();
        }
    }

    private bool chkTicketPageDate()
    {
        //string strSql = "select distinct p.id,p.packagecode,p.usercnt,p.startdate,p.enddate,p.packagename,t.ticketrulecode from t_lm_ticket_package p ";
        //strSql += " left join t_lm_ticket t";
        //strSql += " on p.PACKAGECODE = t.PACKAGECODE where 1=1 and p.SINGLE_USERCNT <= 1";
        string strSql = " select distinct p.id,p.packagecode,p.usercnt,p.startdate,p.enddate,p.packagename,t.ticketrulecode from t_lm_ticket_package p inner join (select count(ticketcode) as tCount, packagecode from t_lm_ticket group by packagecode) tcl on p.packagecode = tcl.packagecode and tcl.tCount <= 1 left join t_lm_ticket t on p.PACKAGECODE = t.PACKAGECODE  where 1=1 and (p.SINGLE_USERCNT is null or p.SINGLE_USERCNT <= 1) and trunc(sysdate,'dd') <= to_date(p.enddate,'yyyy-mm-dd')";
        strSql += " and p.packagecode=  '" + CommonFunction.StringFilter(this.txtPackageCode.Value) + "'";
        strSql += " order by p.ID desc";
        DataSet ds = DbHelperOra.Query(strSql, false);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void btnGrantTicket_Click(object sender, EventArgs e)
    {
        try
        {
            if (!chkTicketPageDate())
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('该优惠券已过最迟领用日期，无法领用，请确认！');", true);
                return;
            }

            string strPackageCode = this.txtPackageCode.Value;
            string strRemainCount = this.lblRestCount.Text;//剩余可以生成的张数
            int intRemainCount = 0;

            string fileExt = System.IO.Path.GetExtension(PhoneFlUpload.FileName);
            if (!String.IsNullOrEmpty(fileExt))
            {
                string allowexts = (ConfigurationManager.AppSettings["AllowUploadFileType"] != null) ? ConfigurationManager.AppSettings["AllowUploadFileType"].ToString() : "";    //定义允许上传文件类型
                if (allowexts == "") { allowexts = ".*xls|.*xlsx|.*txt"; }

                Regex allowext = new Regex(allowexts);
                if (!allowext.IsMatch(fileExt)) //检查文件大小及扩展名
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('选择文件类型不正确，请确认！');", true);
                    return;
                }
            }

            string strPhoneNumber = this.txtPhoneNumber.Text.Replace(" ", "");//电话号码
            strPhoneNumber = strPhoneNumber.Trim(',');
            strPhoneNumber = (strPhoneNumber.Length > 0) ? getFileData() + "," + strPhoneNumber : getFileData();
            strPhoneNumber = CommonFunction.StringFilter(strPhoneNumber);
            strPhoneNumber = strPhoneNumber.Trim(',');
            string[] arrPhone = strPhoneNumber.Split(',');//用逗号进行分隔。
            if (String.IsNullOrEmpty(strPhoneNumber) || arrPhone.Length == 0)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('请添加发送手机号码！');", true);
                return;
            }

            if (!string.IsNullOrEmpty(strRemainCount))
            {
                intRemainCount = Convert.ToInt32(strRemainCount);
            }

            if (intRemainCount <= 0)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptPackageHaveFinish + "');", true);
                return;//后面就不执行了        
            }

            if (intRemainCount < arrPhone.Length)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptLeaveCount + "" + intRemainCount.ToString() + "" + PromptActualCreate + "" + arrPhone.Length.ToString() + "" + Count + "');", true);
                return;//后面就不执行了        
            }

            bool bCanUse = CanUse(strPackageCode);
            if (bCanUse == false)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptTicketNotUse + "');", true);
                return;//后面就不执行了
            }

            if (getTicketCount(strPackageCode) == false)
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptTicketCountMustOne + "');", true);
                return;
            }

            //string repeatPhone = string.Empty;
            CommonFunction comFun = new CommonFunction();

            //bool bollMobileNum = true;
            //string strTempPhone = string.Empty;
            //for (int j = 0; j < arrPhone.Length; j++)
            //{
            //    strTempPhone = arrPhone[j];

            //    //bollMobileNum = comFun.IsMobileNumber(strTempPhone);
            //    //if (bollMobileNum == false)
            //    //{
            //    //    break;
            //    //}

            //    int intFlag = 0;
            //    for (int k = 0; k < arrPhone.Length; k++)
            //    {
            //        if (strTempPhone == arrPhone[k])
            //        {
            //            intFlag += 1;
            //        }
            //    }
            //    if (intFlag > 1)
            //    {
            //        repeatPhone = repeatPhone + strTempPhone + ",";
            //    }
            //}
            //repeatPhone = repeatPhone.Trim(',').Trim();

            ////判断有没有不合法的手机号
            //if (bollMobileNum == false)
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptPhoneFormatError + "" + strTempPhone + "！');", true);
            //    return;
            //}

            ////判断有没有相同的号码
            //if (!string.IsNullOrEmpty(repeatPhone))
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptHaveSamePhone + "" + repeatPhone + "！');", true);
            //    return;
            //}

            //根据PackageCode查询ticketcode和该ticketcode的amount.

            string sqlTicket = "select * from T_LM_TICKET where PACKAGECODE=:PACKAGECODE";
            OracleParameter[] parmPack = { new OracleParameter("PACKAGECODE", OracleType.VarChar) };
            parmPack[0].Value = strPackageCode;
            DataTable dt = DbHelperOra.Query(sqlTicket, false, parmPack).Tables[0];

            string ticketCode = dt.Rows[0]["TICKETCODE"].ToString();
            string ticketAmt = dt.Rows[0]["TICKETAMT"].ToString();

            List<CommandInfo> sqlList = new List<CommandInfo>();
            StringBuilder sqlInsert = new StringBuilder();
            //Oracle sql 语法
            sqlInsert.AppendLine("INSERT INTO T_LM_TICKET_USER(ID,TICKETCODE,TICKETUSERCODE,STATUS,TICKETAMT,PACKAGECODE,FLAG,USERID,CREATETIME,UPDATETIME) VALUES ( ");
            sqlInsert.AppendLine("T_LM_TICKET_USER_SEQ.nextval,:TICKETCODE,:TICKETUSERCODE,:STATUS,:TICKETAMT,:PACKAGECODE,:FLAG,:USERID,sysdate,sysdate) ");

            int iCount = 0;
            int MaxLength = (String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxTicketLength"].ToString())) ? 1000 : int.Parse(ConfigurationManager.AppSettings["MaxTicketLength"].ToString());
            for (int i = 0; i < arrPhone.Length; i++)
            {
                string strPhone = arrPhone[i];
                //OracleParameter[] parm ={
                //                            new OracleParameter("ID",OracleType.Int32),
                //                            new OracleParameter("TICKETCODE",OracleType.VarChar),
                //                            new OracleParameter("TICKETUSERCODE",OracleType.VarChar),
                //                            new OracleParameter("STATUS",OracleType.VarChar),
                //                            new OracleParameter("TICKETAMT",OracleType.Double),
                //                            new OracleParameter("PACKAGECODE",OracleType.VarChar),
                //                            new OracleParameter("FLAG",OracleType.Int32),
                //                            new OracleParameter("USERID",OracleType.VarChar)
                //                        };


                ////ComFun comFun = new ComFun();
                //parm[0].Value = comFun.getMaxIDfromSeq("T_LM_TICKET_USER_SEQ");//ID值  

                OracleParameter[] parm ={
                                            new OracleParameter("TICKETCODE",OracleType.VarChar),
                                            new OracleParameter("TICKETUSERCODE",OracleType.VarChar),
                                            new OracleParameter("STATUS",OracleType.VarChar),
                                            new OracleParameter("TICKETAMT",OracleType.Double),
                                            new OracleParameter("PACKAGECODE",OracleType.VarChar),
                                            new OracleParameter("FLAG",OracleType.Int32),
                                            new OracleParameter("USERID",OracleType.VarChar)
                                        };

                parm[0].Value = ticketCode;

                string strTicketUserCode = getTicketCode_New(13);// getTicketCode(13);

                if (strTicketUserCode == "")
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptCodeHaveFinish + "');", true);
                    return;
                }

                parm[1].Value = strTicketUserCode;
                parm[2].Value = "0";
                parm[3].Value = Convert.ToDouble(ticketAmt);
                parm[4].Value = strPackageCode;
                parm[5].Value = 2;//表示手动送券
                parm[6].Value = strPhone;//表示手动送券

                iCount = iCount + 1;
                CommandInfo cminfo = new CommandInfo();
                cminfo.CommandText = sqlInsert.ToString();
                cminfo.Parameters = parm;
                sqlList.Add(cminfo);

                try
                {
                    if (iCount == MaxLength)
                    {
                        DbHelperOra.ExecuteSqlTran(sqlList);
                        sqlList = new List<CommandInfo>();
                        iCount = 0;
                    }
                    //DbHelperOra.ExecuteSql(sqlInsert.ToString(), parm);
                    //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptTicketSuccess + "');", true);
                    //this.txtPhoneNumber.Text = "";
                }
                catch
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptTicketFaild + "');", true);
                }
            }

            try
            {
                if (iCount > 0)
                {
                    DbHelperOra.ExecuteSqlTran(sqlList);
                }
                //DbHelperOra.ExecuteSql(sqlInsert.ToString(), parm);
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptTicketSuccess + "');", true);
                this.txtPhoneNumber.Text = "";

                CommonEntity _commonEntity = new CommonEntity();
                _commonEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
                _commonEntity.LogMessages.Userid = UserSession.Current.UserAccount;
                _commonEntity.LogMessages.Username = UserSession.Current.UserDspName;
                _commonEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

              
                _commonEntity.CommonDBEntity = new List<CommonDBEntity>();
                CommonDBEntity commonDBEntity = new CommonDBEntity();

                commonDBEntity.Event_Type = "发券给指定的用户-保存";
                commonDBEntity.Event_ID = strPackageCode;
                commonDBEntity.Event_Content = String.Format("发券给指定的用户-保存：优惠券包：{0}，发送总数：{1}，操作人：{2}", strPackageCode, arrPhone.Length, _commonEntity.LogMessages.Userid);

                commonDBEntity.Event_Result = PromptTicketSuccess;
                _commonEntity.CommonDBEntity.Add(commonDBEntity);
                CommonBP.InsertEventHistory(_commonEntity);

            }
            catch
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptTicketFaild + "');", true);
            }
        }
        catch
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + PromptTicketFaild + "');", true);
        }

        //执行后需要修改页面中的剩余可以生产券的张数
        btnSearchRest_Click(null, null);
    }

    //private delegate void AsyncSendTicketDel(string userid);

    //public static void AsyncSendTicket(int userid)
    //{
    //    AsyncSendTicketDel del = new AsyncSendTicketDel(SendTicketByUserId);
    //    del.BeginInvoke(userid, new AsyncCallback(CallbackMethod), del);
    //}

    //internal static void CallbackMethod(IAsyncResult ar)
    //{
    //    //得到异步方法的代理 
    //    AsyncSendTicketDel dlgt = (AsyncSendTicketDel)(ar as AsyncResult).AsyncDelegate;
    //    dlgt.EndInvoke(ar);
    //}

    //private static void SendTicketByUserId(int userid)
    //{
    //    System.Threading.Thread.Sleep(5000);

    //}

    private string getFileData()
    {
        if (String.IsNullOrEmpty(PhoneFlUpload.FileName))
        {
            return "";
        }

        StringBuilder sbPhonelist = new StringBuilder();
        string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(PhoneFlUpload.FileName);// OrderFlUpload.FileName.Substring(OrderFlUpload.FileName.IndexOf('.'));          //获取文件名
        string folder = Server.MapPath("../Excel");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        PhoneFlUpload.SaveAs(Server.MapPath("../Excel" + "\\" + fileName));      //上传文件到Excel文件夹下
        string extname = Path.GetExtension(fileName).ToUpper();

        if (".TXT".Equals(extname))
        {
            StreamReader reader = new StreamReader(Server.MapPath("../Excel" + "\\" + fileName), System.Text.Encoding.GetEncoding("GB2312"));
            string oneline = reader.ReadLine();
            reader.Close();
            File.Delete(Server.MapPath("../Excel" + "\\" + fileName));
            return oneline.TrimEnd(',');
        }
        else
        {
            DataTable dtTemp = LoadExcelToDataTable(Server.MapPath("../Excel" + "\\" + fileName));  //读取excel内容，转成DataTable
            File.Delete(Server.MapPath("../Excel" + "\\" + fileName));                                            //最后删除文件

            foreach (DataRow drTemp in dtTemp.Rows)
            {
                sbPhonelist.Append(drTemp[0].ToString() + ",");
            }

            return (sbPhonelist.Length > 0) ? sbPhonelist.ToString().Substring(0, sbPhonelist.ToString().Length - 1) : sbPhonelist.ToString();
        }
       
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


    //判断该券是否在有效的时间和规则内，判断该优惠券包是否可用
    private bool CanUse(string PackageCode)
    {
        string nowDate = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
        string sql = "select * from t_lm_ticket_package where  packagecode =:packagecode and startdate<=:startdate and enddate>=:enddate";
        OracleParameter[] parmPack = {
                                         new OracleParameter("packagecode", OracleType.VarChar),
                                         new OracleParameter("startdate", OracleType.VarChar),
                                         new OracleParameter("enddate", OracleType.VarChar)
                                     };

        parmPack[0].Value = PackageCode;
        parmPack[1].Value = nowDate;
        parmPack[2].Value = nowDate;
        DataTable dt = DbHelperOra.Query(sql, false, parmPack).Tables[0];
        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //判断该Package下有几张ticket券
    //只能内含一张ticket，且只能内含一张券
    private bool getTicketCount(string PackageCode)
    {
        string sql = "select Count(ticketcnt) from t_lm_ticket where  packagecode =:packagecode";
        OracleParameter[] parmPack = {
                                         new OracleParameter("packagecode", OracleType.VarChar)                                       
                                     };

        parmPack[0].Value = PackageCode;
        object objTicketCount = DbHelperOra.GetSingle(sql, false, parmPack);
        if (objTicketCount == null || objTicketCount == DBNull.Value)
        {
            return false;
        }
        else
        {
            int intTicketCount = Convert.ToInt32(objTicketCount);
            if (intTicketCount == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    //用于产生新的一个UserCode
    private string getTicketCode(int length)
    {
        CommonFunction comFun = new CommonFunction();
        string newCode = comFun.GetRandNumString(length);
        bool bExist = ExistCode(newCode);
        if (bExist == false)//表示不存在该Code，则可以加入
        {
            return newCode;
        }
        else
        {
            //循环100次，如果还不存在，则提示不能再生成了。
            int flag = 0; //用于标记，循环100次后，是否有形成可用的ticketcode
            for (int i = 0; i < 100; i++)
            {
                newCode = comFun.GetRandNumString(length);
                if (ExistCode(newCode) == false)
                {
                    flag = 1;
                    break;
                }
            }
            if (flag == 1)
            {
                return newCode;
            }
            else
            {
                return "";
            }
        }
    }

    private string getTicketCode_New(int length)
    {
        string str = "1234567890";
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            builder.Append(str[new Random(Guid.NewGuid().GetHashCode()).Next(0, 9)]);
        }
        return builder.ToString();
    }

    /// <summary>
    /// 判断是否存在
    /// </summary>
    /// <returns></returns>
    private bool ExistCode(string userTicketCode)
    {
        string sqlExist = "select  ticketusercode from T_LM_TICKET_USER where ticketusercode=:ticketusercode";
        OracleParameter[] parmExist = { new OracleParameter("ticketusercode", OracleType.VarChar) };
        parmExist[0].Value = userTicketCode;
        object objCode = DbHelperOra.GetSingle(sqlExist, false, parmExist);
        if (objCode == null || objCode == DBNull.Value)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //剩余可生产张数。
    private int RestCount(string strPackageCode, int UserCNT)
    {
        int restCount = 0;
        string sqlSelCount = "select  count(*) from T_LM_TICKET_USER where PACKAGECODE=:PACKAGECODE";
        OracleParameter[] parmSelCount = { new OracleParameter("PACKAGECODE", OracleType.VarChar) };
        parmSelCount[0].Value = strPackageCode;
        object objTicketUserCount = DbHelperOra.GetSingle(sqlSelCount, false, parmSelCount);
        if (objTicketUserCount == null || objTicketUserCount == DBNull.Value)
        {
            restCount = 0;
        }
        else
        {
            restCount = UserCNT - Convert.ToInt32(objTicketUserCount);
        }
        return restCount;
    }

    //查询剩余可以生产的抵用券张数
    protected void btnSearchRest_Click(object sender, EventArgs e)
    {
        string packageCode = this.txtPackageCode.Value;        
        int userCount = Convert.ToInt32(this.txtTicketCount.Value.Trim());
        int restCount = RestCount(packageCode, userCount);
        this.lblRestCount.Text = restCount.ToString();
    }


    /*add div for select package*/
    #region for searchPachage
    void BindGridView()
    {
        //string strSql = "select distinct p.id,p.packagecode,p.usercnt,p.startdate,p.enddate,p.packagename,t.ticketrulecode from t_lm_ticket_package p ";
        //strSql += " left join t_lm_ticket t";
        //strSql += " on p.PACKAGECODE = t.PACKAGECODE where 1=1 and p.SINGLE_USERCNT <= 1";
        string strSql = " select distinct p.id,p.packagecode,p.usercnt,p.startdate,p.enddate,p.packagename,t.ticketrulecode from t_lm_ticket_package p inner join (select count(ticketcode) as tCount, packagecode from t_lm_ticket group by packagecode) tcl on p.packagecode = tcl.packagecode and tcl.tCount <= 1 left join t_lm_ticket t on p.PACKAGECODE = t.PACKAGECODE  where 1=1 and (p.SINGLE_USERCNT is null or p.SINGLE_USERCNT <= 1) and trunc(sysdate,'dd') <= to_date(p.enddate,'yyyy-mm-dd')";
        if (this.txtPackageNameSearch.Text.Trim() != "")
        {
            strSql += " and p.packagename like '%" + CommonFunction.StringFilter(this.txtPackageNameSearch.Text.Trim()) + "%'";
        }

        if (this.txtPackageCodeSearch.Text.Trim() != "")
        {
            strSql += " and p.packagecode=  '" + CommonFunction.StringFilter(this.txtPackageCodeSearch.Text.Trim()) + "'";
        }

        strSql += " order by p.ID desc";

        DataSet ds = DbHelperOra.Query(strSql, false);
        DataView view = ds.Tables[0].DefaultView;
        this.myGridView.DataSource = view;
        this.myGridView.DataBind();

    }

    protected void myGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        myGridView.PageIndex = e.NewPageIndex;
        BindGridView();

        Page.RegisterStartupScript("ggg", "<script>invokeOpen2(); </script>");

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
        Page.RegisterStartupScript("ggg", "<script>invokeOpen2(); </script>");
    }

   
    protected void myGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selIndex = myGridView.SelectedIndex;
        string strPackageCode = myGridView.Rows[selIndex].Cells[1].Text.ToString();
        string strPackageName = myGridView.Rows[selIndex].Cells[2].Text.ToString();
        string strTicketCount = myGridView.Rows[selIndex].Cells[3].Text.ToString();//总共可以生产的张数
        
        string strTicketRuleCode = myGridView.Rows[selIndex].Cells[6].Text.ToString().Trim();//使用规则

        if (string.IsNullOrEmpty(strTicketRuleCode)|| strTicketRuleCode=="&nbsp;" )
        {
            Page.RegisterStartupScript("key", "<script>alert('该选中的礼包券没有设置使用规则，不能使用！');invokeOpen2(); </script>");          
        }
        else
        {

            int userCount = Convert.ToInt32(strTicketCount);
            int restCount = RestCount(strPackageCode, userCount);

            this.lblRestCount.Text = restCount.ToString();
            this.txtPackageCode.Value = strPackageCode;
            this.txtTicketCount.Value = strTicketCount;
            Page.RegisterStartupScript("ggg", "<script>invokeClose2(); </script>");
        }
    }
    #endregion


   
    
}