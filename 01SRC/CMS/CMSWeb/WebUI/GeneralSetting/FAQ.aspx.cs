using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Collections;

using HotelVp.Common.DBUtility;
using HotelVp.Common.DataAccess;
using HotelVp.CMS.Domain.Process;
using HotelVp.CMS.Domain.Entity;

public partial class WebUI_GeneralSetting_FAQ : System.Web.UI.Page
{
    private string ParentID = "";
    public int rowIndex = 0;
    public string STR_YES = string.Empty;
    public string STR_NO = string.Empty;

    public static string strClose = Resources.MyGlobal.CloseText;
    public static string strNew = Resources.MyGlobal.NewText;

    FAQEntity _faqEntity = new FAQEntity();
   
    CommonEntity _commonEntity = new CommonEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
        STR_YES = "是";
        STR_NO = "否";

        //多语言的绑定
        if (!IsPostBack)
        {
            BindGridView();          
        }
    }

    private void BindGridView()
    {
        #region FAQGridView 数据绑定

        _faqEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _faqEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _faqEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _faqEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _faqEntity.FAQDBEntity = new List<FAQDBEntity>();
        FAQDBEntity faqDBEntity = new FAQDBEntity();

        _faqEntity.FAQDBEntity.Add(faqDBEntity);
        DataSet dsResult = FAQBP.CommonSelect(_faqEntity).QueryResult;

        DataTable dtUser = dsResult.Tables[0];
        GridviewControl.GridViewDataBind(this.FAQGridView, dtUser);

        #endregion
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strQuestion = this.txtQuestion.Text.Trim();
        string strAnswer = this.txtAnswer.Text.Trim();

        _faqEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _faqEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _faqEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _faqEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _faqEntity.FAQDBEntity = new List<FAQDBEntity>();
        FAQDBEntity faqDBEntity = new FAQDBEntity();
        int id = new CommonFunction().getMaxIDfromSeq("t_lm_qa_seq");

        int SeqMax = DbHelperOra.GetMaxID("SEQ", "t_lm_qa", false);

        faqDBEntity.ID = id;
        faqDBEntity.QUSETION_HEAD = strQuestion;
        faqDBEntity.ANSWER_BODY = strAnswer;
        faqDBEntity.SEQ = SeqMax;      

        
        _faqEntity.FAQDBEntity.Add(faqDBEntity);        
        FAQBP.Insert(_faqEntity);

        clearInputText();//清空已经输入的信息

        BindGridView();

    }

    private void clearInputText()
    {
        this.txtQuestion.Text = "";
        this.txtAnswer.Text = "";       
    }

    protected void FAQGridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        //{          
        //    e.Row.Cells[5].Visible = false; //如果想使第1列不可见,则将它的可见性设为false
        //}
    }

    protected void FAQGridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        FAQGridView.EditIndex = e.NewEditIndex;
        BindGridView();
        btnAdjust.Enabled = false;
        divAdjust.Style.Add("display","block");
        divSaveAdjust.Style.Add("display", "none");
    }

    protected void FAQGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {       
        string ID = FAQGridView.DataKeys[e.RowIndex].Value.ToString();

        _faqEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _faqEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _faqEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _faqEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        string strQuestion = ((TextBox)FAQGridView.Rows[e.RowIndex].FindControl("txtEditQuestion")).Text;
        string strAnswer = ((TextBox)FAQGridView.Rows[e.RowIndex].FindControl("txtEditAnswer")).Text;
        if ((strQuestion.Length > 100) || (string.IsNullOrEmpty(strQuestion)))
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('问题不能为空，且总长度最多只能输入100个字符！');", true);
            return;
        }

        if ((strAnswer.Length > 300) || (string.IsNullOrEmpty(strAnswer)))
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('答案不能为空，且答案的内容的总长度不能大于300个字符！');", true);
            return;
        }

        _faqEntity.FAQDBEntity = new List<FAQDBEntity>();
        FAQDBEntity faqDBEntity = new FAQDBEntity();

        faqDBEntity.ID = Convert.ToInt32(ID);
        faqDBEntity.QUSETION_HEAD = strQuestion;
        faqDBEntity.ANSWER_BODY = strAnswer;
     

        _faqEntity.FAQDBEntity.Add(faqDBEntity);
        int iResult = FAQBP.Update(_faqEntity);

        if (iResult == 1) //修改成功
        {
            string successText = Resources.MyGlobal.UpdateSuccessText;            
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + successText + "');", true);

            FAQGridView.EditIndex = -1;
            BindGridView();//重新绑定显示的页面

            btnAdjust.Enabled = true;
        }
        else//表示修改失败
        {
            //string strFaild = GetLocalResourceObject("UpdateFaildText").ToString();                
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "failed", "alert('修改失败！')", true);
        }


    }

    protected void FAQGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        FAQGridView.EditIndex = -1;
        BindGridView();

        btnAdjust.Enabled = true;
        divAdjust.Style.Add("display", "block");
        divSaveAdjust.Style.Add("display", "none");
    }

    /// <summary>
    /// 点击删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void FAQGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = FAQGridView.DataKeys[e.RowIndex].Value.ToString();
        _faqEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _faqEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _faqEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _faqEntity.LogMessages.IpAddress = UserSession.Current.UserIP;
        _faqEntity.FAQDBEntity = new List<FAQDBEntity>();
        FAQDBEntity faqDBEntity = new FAQDBEntity();

        faqDBEntity.ID = Convert.ToInt32(ID);
        _faqEntity.FAQDBEntity.Add(faqDBEntity);     //增加一条到数据库中

        int iResult = FAQBP.Delete(_faqEntity);

        if (iResult == 1)//删除成功
        {
            string successText = Resources.MyGlobal.DeleteSuccessText;
            //commonDBEntity.Event_Result = successText;           
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('" + successText + "');", true);

            BindGridView();//重新绑定显示的页面          
        }
        else//表示失败
        {
            //string strFaild = GetLocalResourceObject("DeleteFaildText").ToString();               
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "failed", "alert('删除失败！')", true);
        }
    }

    //初始绑定
    protected void FAQGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                string strid = ((Label)e.Row.FindControl("lblID")).Text;
                ((LinkButton)e.Row.Cells[5].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除：\"" + strid + "\"吗?')");
            }
        } 
    }

    
    //输入框设置为可以编辑
    protected void btnAdjust_Click(object sender, EventArgs e)
    {      
        TextBox tbSeq;
        for (int i = 0; i < FAQGridView.Rows.Count; i++)
        {
            tbSeq = (TextBox)FAQGridView.Rows[i].FindControl("txtSeqRead");
            tbSeq.Enabled = true;
        }

        this.divSaveAdjust.Style.Add("display", "block");
        this.divAdjust.Style.Add("display", "none");
    }

    //进行保存
    protected void btnSaveAdjust_Click(object sender, EventArgs e)
    {
        TextBox tbSeq;
        Label lbID;
        List<string> list = new List<string>();
        string sql = string.Empty;
        List<string> listSeq = new List<string>();
        int updateFlag = 0;
        for (int i = 0; i < FAQGridView.Rows.Count; i++)
        {
            lbID = (Label)FAQGridView.Rows[i].FindControl("lblID");
            string id = lbID.Text;
            tbSeq = (TextBox)FAQGridView.Rows[i].FindControl("txtSeqRead");
            string seq = tbSeq.Text;
            try
            {
                int intSeq = int.Parse(seq);
                if (intSeq <= 0)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('排序必须输入大于0的非负整数！');", true);
                    updateFlag = 0;
                    return;

                }
                else
                {
                    int index = listSeq.IndexOf(seq);
                    if (index >= 0)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('排序有重复的数字，请重新输入数字！');", true);
                        updateFlag = 0;
                        return;
                    }
                    else
                    {
                        listSeq.Add(seq);
                        sql = "update t_lm_qa set SEQ =" + seq + " where id=" + id;
                        list.Add(sql);
                        updateFlag = 1;
                    }
                }
            }
            catch
            {
                 
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('排序必须输入大于0的非负整数！');", true);
                updateFlag = 0;
                return; 
            }
        }

        try
        {
            if (updateFlag == 1)
            {
                DbHelperOra.ExecuteSqlTran(list);

                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('修改成功！');", true);

                this.divSaveAdjust.Style.Add("display", "none");
                this.divAdjust.Style.Add("display", "block");
                BindGridView();
            }           

        }
        catch
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "key", "alert('修改失败！');", true);
        }        
      
    }

    //取消
    protected void btnCancelAdjust_Click(object sender, EventArgs e)
    {
        this.divSaveAdjust.Style.Add("display", "none");
        this.divAdjust.Style.Add("display", "block");
        BindGridView();
    }
}